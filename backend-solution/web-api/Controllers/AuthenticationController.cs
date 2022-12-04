using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text.Json;
using web_api.GameModel;
using web_api.Services;

namespace web_api.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public AuthenticationController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("api/user/register")]
        public async Task<ActionResult> Register([FromBody] UserDto request)
        {
            Authentication.CreatePasswordHash(request.Password, out byte[] pwdHash, out byte[] pwdSalt);

            User user = new User(request.Email, pwdHash, pwdSalt);
            user.RegistrationTime = DateTime.UtcNow;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Erfolgreich registriert als " + request.Email);
        }

        [HttpPost("api/user/login")]
        public async Task<ActionResult<string>> LogIn([FromBody] UserDto request)
        {
            
            var user = _context.Users.Where(u => u.Email == request.Email).FirstOrDefault();
            if (user == null || !Authentication.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong username or password");
            }

            string accessToken = Authentication.CreateAccessToken(user, _configuration);
            var newRefreshToken = Authentication.GenerateRefreshToken(user);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires,
                SameSite = SameSiteMode.None,
                Secure = true
            };

            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;
            await _context.SaveChangesAsync();

            return Ok(accessToken);
        }

        [HttpPost("api/user/token-refresh")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var user = _context.Users.Where(u => u.RefreshToken == refreshToken).FirstOrDefault();
            if (user == null || user.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Invalid Refresh Token.");
            }

            string accessToken = Authentication.CreateAccessToken(user, _configuration);
            var newRefreshToken = Authentication.GenerateRefreshToken(user);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;
            _context.SaveChanges();

            return Ok(accessToken);
        }
    }
}
