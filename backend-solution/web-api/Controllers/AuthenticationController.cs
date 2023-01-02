using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using System.Text;
using web_api.Services;

namespace web_api.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly INotification _notification;
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
        /// <param name="request">UserDto, with Email and Password</param>
        /// <returns></returns>
        [HttpPost("api/user/register")]
        public async Task<ActionResult<string>> Register([FromBody] UserDto request)
        {
            // email-addresses are not case sensitive, so the test must ignore case sensitivity
            var user = _context.Users.Where(u => u.Email.ToLower() == request.Email.ToLower()).FirstOrDefault();
            if (user != null)
                return Conflict("Ein Konto mit dieser E-Mail-Adresse existiert bereits");

            Authentication.CreatePasswordHash(request.Password, out byte[] pwdHash, out byte[] pwdSalt);

            User newUser = new User(request.Email, pwdHash, pwdSalt);
            newUser.RegistrationTime = DateTime.UtcNow;

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            // after saving the dataContext, the User-Id is automaticly set in the newUser-object
            UserConfirmation userConfirmation = new UserConfirmation();
            userConfirmation.UserId = newUser.Id;
            _context.Confirmation.Add(userConfirmation);

            await _context.SaveChangesAsync();

            ConfirmationService confirmation = new ConfirmationService();
            confirmation.Url = $"{Request.Scheme}://{Request.Host.Value}";
            string messageText = confirmation.CreateNotificationMessage(userConfirmation);

            _notification.SendTo(messageText, "Account bestätigen", newUser.Email);

            return Ok("Erfolgreich registriert als " + request.Email);
        }

        /// <summary>
        /// LogIn for a existing user
        /// </summary>
        /// <response code="200">when the user exist, the password is equal and the refresh token is updated </response>
        /// <response code="400">when the user <b>not</b> exists in the database or the given password is unequal to the stored password</response>
        /// <param name="request">UserDto, with Email and Password</param>
        /// <returns></returns>
        [HttpPost("api/user/login")]
        public async Task<ActionResult<string>> LogIn([FromBody] UserDto request)
        {
            var user = _context.Users.Where(u => u.Email.ToLower() == request.Email.ToLower()).FirstOrDefault();
            if (user == null || !Authentication.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                return BadRequest("Wrong username or password");

            if (!user.IsActive)
                return BadRequest("User isn't confirmed yet");

            string accessToken = Authentication.CreateAccessToken(user, _configuration);
            var newRefreshToken = Authentication.GenerateRefreshToken(user);

            await UpdateRefreshTokenInDbAsync(newRefreshToken, user);

            SetRefreshTokenToCookieData(newRefreshToken);

            return Ok(accessToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <response code="200"></response>
        /// <response code="401">when the user is unknown, or the refreshToken is expired</response>
        /// <returns></returns>
        [HttpPost("api/user/token-refresh")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var user = _context.Users.Where(u => u.RefreshToken == refreshToken).FirstOrDefault();
            if (user == null || user.TokenExpires < DateTime.Now)
                return Unauthorized("Invalid Refresh Token.");

            string accessToken = Authentication.CreateAccessToken(user, _configuration);
            var newRefreshToken = Authentication.GenerateRefreshToken(user);

            await UpdateRefreshTokenInDbAsync(newRefreshToken, user);

            SetRefreshTokenToCookieData(newRefreshToken);

            return Ok(accessToken);
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
