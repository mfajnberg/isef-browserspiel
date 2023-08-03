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
    ///     Controller for users to register, manage and authenticate themselves with their accounts.
    /// </summary>
    [ApiController]
    [Route("api/user")]
    public class AuthenticationController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly INotification _notification;

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
        ///     Registers a new user account. 
        /// </summary>
        /// 
        /// <response code="409">
        ///     An account with the input email address already exists in the database.
        /// </response>
        /// 
        /// <param name="request">
        ///     <c>UserDTO</c>, with an email and a password.
        /// </param>
        /// 
        /// <returns>
        ///     A message for the client.
        /// </returns>
        /// 
        /// <remarks>
        ///     Sample request:
        /// 
        ///     POST
        ///  
        ///     {
        ///         "email": "name@example.com",
        ///         "password" : "$uper$ecret"
        ///     }
        /// </remarks>
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register([FromBody] UserDTO request)
        {

            // email-addresses are not case sensitive, so the test must ignore case sensitivity
            var user = _context.Users.Where(u => u.Email.ToLower() == request.Email.ToLower()).FirstOrDefault();
            if (user != null)
            {
                return Conflict("Ein Konto mit dieser E-Mail-Adresse existiert bereits.");
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

            return Ok("Erfolgreich registriert als " + request.Email + ".");
        }

        // todo: Ensure additionally that clients only send hashed values to begin with.
        /// <summary>
        ///     LogIn for a existing user
        /// </summary>
        /// 
        /// <param name="request">
        ///     <c>UserDTO</c>, with an email and a password.
        /// </param>
        /// 
        /// <response code="400">
        ///     The input user data does not match any accounts in the database.
        /// </response>
        /// 
        /// <response code="422">
        ///     The account is currently not set to active.
        /// </response>
        /// 
        /// <returns>
        ///     A message for the client.
        /// </returns>
        /// 
        /// <remarks>
        ///     Sample request:
        /// 
        ///     POST
        ///     
        ///     {
        ///         "email": "name@example.com",
        ///         "password" : "$uper$ecret",
        ///         "token" : ""
        ///     }
        /// </remarks>
        [HttpPost("login")]
        public async Task<ActionResult<string>> LogIn([FromBody] UserDTO request)
        {
            var user = _context.Users.Where(u => u.Email.ToLower() == request.Email.ToLower())
                .Include(u => u.Avatar) //.ThenInclude(a => a.Party) // etc.
                    .FirstOrDefault();
            if (user == null || !AuthenticationService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                return BadRequest("Wrong username or password.");

            if (!user.IsActive)
                return UnprocessableEntity("User isn't confirmed yet.");

            string accessToken = AuthenticationService.CreateAccessToken(user, _configuration);
            var newRefreshToken = AuthenticationService.GenerateRefreshToken();
            await UpdateRefreshTokenInDbAsync(newRefreshToken, user);

            var response = AuthResponseDTO.CreateResponse(_context, user, accessToken);
            SetRefreshTokenToCookieData(newRefreshToken);

            return Ok(response);
        }

        /// <summary>
        ///     Enables automated login for authenticated clients.
        /// </summary>
        /// 
        /// <response code="401">
        ///     The input refresh token is unknown or expired.
        /// </response>
        /// 
        /// <returns>
        ///     A new refresh token.
        /// </returns>
        [HttpPost("token-refresh")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var user = _context.Users.Where(u => u.RefreshToken == refreshToken)
                .Include(u => u.Avatar)
                    .FirstOrDefault();

            if (user == null || user.TokenExpires < DateTime.Now)
                return Unauthorized("Invalid refresh token.");

            string accessToken = AuthenticationService.CreateAccessToken(user, _configuration);
            var newRefreshToken = AuthenticationService.GenerateRefreshToken();
            await UpdateRefreshTokenInDbAsync(newRefreshToken, user);

            var response = AuthResponseDTO.CreateResponse(_context, user, accessToken);
            SetRefreshTokenToCookieData(newRefreshToken);

            return Ok(response);
        }

        /// <summary>
        ///     Updates a user's refresh token entry in the database.
        /// </summary>
        /// 
        /// <param name="newRefreshToken">
        ///     New refresh token data.
        /// </param>
        /// 
        /// <param name="user">
        ///     The user who is logging in.
        /// </param>
        private async Task UpdateRefreshTokenInDbAsync(RefreshToken newRefreshToken, User user)
        {
            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        ///     Attatches a given refresh token to the cookies of the current request data.
        /// </summary>
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

        // todo: Reroute the client to a special form based on the reset link!
        /// <summary>
        /// Function for users who have forgotten their passwords
        /// sends an email to the user. email includes a reset link
        /// </summary>
        /// <response code="200">everything is fine, email was sent</response>
        /// <response code="404">if the email address isn't located in the DB</response>
        /// <param name="emailAddress">email from a registred user</param>
        /// <returns></returns>
        [HttpGet("forgot")]
        public async Task<ActionResult> ForgotPassword(string emailAddress)
        {
            // validate if the user is known
            User user = _context.Users.FirstOrDefault(u => u.Email.ToLower() == emailAddress.ToLower());
            if (user == null)
                return BadRequest();

            // create ResetToken and add it to DB
            UserResetToken userResetToken = new UserResetToken();
            userResetToken.Email = emailAddress;
            userResetToken.Token = Guid.NewGuid().ToString();
            userResetToken.ExpiresAt = DateTime.Now.AddHours(1);

            _context.UserResetTokens.Add(userResetToken);
            await _context.SaveChangesAsync();

            // Create and send reset mail
            ResetPasswordService resetPasswordService = new ResetPasswordService($"{Request.Scheme}://{Request.Host.Value}");

            // Not awaited due to a possible delay because of external factors
            _notification.SendToAsync(resetPasswordService.CreateForgotPasswortMessage(userResetToken), 
                resetPasswordService.GetForgotPasswordSubject(), emailAddress);

            return Ok();
        }

        /// <summary>
        ///     Changes the password from the given user
        /// </summary>
        /// 
        /// <param name="request">
        /// </param>
        /// 
        /// <response code="404">
        ///     if user or userResetToken isn't located in the DB
        /// </response>
        /// 
        /// <remarks>
        ///     Sample request:
        /// 
        ///     POST
        ///  
        ///     {
        ///         "email": "name@example.com",
        ///         "password" : "$uper$ecret",
        ///         "token" : "365546E7-1921-4349-BA05-375385759769"
        ///     }
        /// </remarks>
        [HttpPost("reset")]
        public async Task<ActionResult> ResetPassword([FromBody] UserDTO request)
        {
            // validate if the resetToken is known
            UserResetToken resetToken = _context.UserResetTokens.FirstOrDefault(r => r.Token == request.Token);
            if (resetToken == null)
                return BadRequest();

            _context.UserResetTokens.Remove(resetToken);

            // validate if the user is known 
            User user = _context.Users.FirstOrDefault(u => u.Email.ToLower() == request.Email);
            if (user == null)
                return BadRequest();

            // Change password
            AuthenticationService.CreatePasswordHash(request.Password, out byte[] pwdHash, out byte[] pwdSalt);
            user.PasswordSalt = pwdSalt;
            user.PasswordHash = pwdHash;
            user.RefreshToken = null;
            user.TokenExpires = null;
            user.TokenCreated = null;

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
