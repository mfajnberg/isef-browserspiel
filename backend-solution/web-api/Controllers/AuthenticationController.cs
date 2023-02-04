using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Text;
using web_api.DTOs;
using web_api.GameModel;
using web_api.Services.Authentication;

namespace web_api.Controllers
{
    /// <summary>
    /// Authentication Endpoint
    /// </summary>
    [ApiController]
    [Route("api/user")]
    public class AuthenticationController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly INotification _notification;
        /// <summary>
        /// Constructor for AuthenticationController
        /// </summary>
        /// <param name="context"></param>
        /// <param name="configuration"></param>
        /// <param name="notification"></param>
        public AuthenticationController(DataContext context, IConfiguration configuration, INotification notification)
        {
            _context = context;
            _configuration = configuration;
            _notification = notification;
        }

        /// <summary>
        /// Registers a new user 
        /// </summary>
        /// <response code="200">when the new user has been successfully stored in the database</response>
        /// <response code="409">if the Emailaddress is already stored in the database</response>
        /// <param name="request"><c>UserDTO</c>, with Email and Password</param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///  POST
        ///  {
        ///    "email": "name@example.com",
        ///    "password" : "$uper$ecret"
        ///  }
        /// </remarks>
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register([FromBody] UserDTO request)
        {

            // email-addresses are not case sensitive, so the test must ignore case sensitivity
            var user = _context.Users.Where(u => u.Email.ToLower() == request.Email.ToLower()).FirstOrDefault();
            if (user != null)
            {
                return Conflict("Ein Konto mit dieser E-Mail-Adresse existiert bereits");
            }

            AuthenticationService.CreatePasswordHash(request.Password, out byte[] pwdHash, out byte[] pwdSalt);

            User newUser = new User(request.Email, pwdHash, pwdSalt);
            newUser.RegistrationTime = DateTime.UtcNow;

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            // after saving the dataContext, the User-Id is automaticly set in the newUser-object
            UserConfirmation userConfirmation = new UserConfirmation();
            userConfirmation.UserId = newUser.Id;
            _context.Confirmations.Add(userConfirmation);

            await _context.SaveChangesAsync();

            ConfirmationService confirmation = new ConfirmationService();
            confirmation.Url = $"{Request.Scheme}://{Request.Host.Value}";
            string messageText = confirmation.CreateNotificationMessage(userConfirmation);

            await _notification.SendToAsync(messageText, "Account bestätigen", newUser.Email);

            return Ok("Erfolgreich registriert als " + request.Email);
        }

        /// <summary>
        /// LogIn for a existing user
        /// </summary>
        /// <response code="200">when the user exist, the password is equal and the refresh token is updated </response>
        /// <response code="400">when the user <b>not</b> exists in the database or the given password is unequal to the stored password</response>
        /// <param name="request">UserDTO, with Email and Password</param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///  POST
        ///  {
        ///    "email": "name@example.com",
        ///    "password" : "$uper$ecret"
        ///  }
        /// </remarks>
        [HttpPost("login")]
        public async Task<ActionResult<string>> LogIn([FromBody] UserDTO request)
        {
            var user = _context.Users.Where(u => u.Email.ToLower() == request.Email.ToLower())
                .Include(u => u.Avatar) //.ThenInclude(a => a.Party) // etc.
                    .FirstOrDefault();
            if (user == null || !AuthenticationService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                return BadRequest("Wrong username or password");

            if (!user.IsActive)
                return UnprocessableEntity("User isn't confirmed yet");

            string accessToken = AuthenticationService.CreateAccessToken(user, _configuration);
            var newRefreshToken = AuthenticationService.GenerateRefreshToken();
            await UpdateRefreshTokenInDbAsync(newRefreshToken, user);

            var response = AuthResponseDTO.CreateResponse(_context, user, accessToken);
            SetRefreshTokenToCookieData(newRefreshToken);

            return Ok(response);
        }

        /// <summary>
        /// creates a new reftresh token item 
        /// </summary>
        /// <response code="200"></response>
        /// <response code="401">when the user is unknown, or the refreshToken is expired</response>
        /// <returns></returns>
        [HttpPost("token-refresh")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var user = _context.Users.Where(u => u.RefreshToken == refreshToken).FirstOrDefault();
            if (user == null || user.TokenExpires < DateTime.Now)
                return Unauthorized("Invalid Refresh Token.");

            string accessToken = AuthenticationService.CreateAccessToken(user, _configuration);
            var newRefreshToken = AuthenticationService.GenerateRefreshToken();
            await UpdateRefreshTokenInDbAsync(newRefreshToken, user);

            var response = AuthResponseDTO.CreateResponse(_context, user, accessToken);
            SetRefreshTokenToCookieData(newRefreshToken);

            return Ok(response);
        }

        /// <summary>
        /// Updates the users refresh token data 
        /// </summary>
        /// <param name="newRefreshToken">the new refresh token for the user</param>
        /// <param name="user">user hows logged in</param>
        /// <returns></returns>
        private async Task UpdateRefreshTokenInDbAsync(RefreshToken newRefreshToken, User user)
        {
            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// sets the refresh token in the cookie data 
        /// </summary>
        /// <param name="newRefreshToken"></param>
        private void SetRefreshTokenToCookieData(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires,
                SameSite = SameSiteMode.None,
                Secure = true
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
        }
    }
}
