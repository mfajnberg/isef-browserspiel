using Microsoft.AspNetCore.Mvc;
using web_api.GameModel;
using web_api.Services;

namespace web_api.Controllers
{
    public class AuthenticationController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public AuthenticationController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserDto request)
        {
            Authentication.CreatePasswordHash(request.Password, out byte[] pwdHash, out byte[] pwdSalt);

            User user = new User(request.Email, pwdHash, pwdSalt);
            user.RegistrationTime = DateTime.UtcNow;
            user.ReceiveNewsletter = request.ReceiveNewsletter;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> LogIn(UserDto request)
        {
            
            var user = _context.Users.Where(u => u.Email == request.Email).FirstOrDefault();
            if (user == null || !Authentication.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong username or password");
            }

            return Ok(Authentication.CreateToken(user, _configuration));

        }
    }
}
