using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace web_api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WorldController : ControllerBase
    {
        DataContext _context;
        public WorldController(DataContext context)
        {
            _context= context;
        }
        [HttpGet]
        public async Task<ActionResult> GetVisibleHexTiles()
        {
            var mailFromClaim = User.Claims.Where(c => c.Type == ClaimTypes.Email).FirstOrDefault().Value.ToLower();
            var user = _context.Users.Where(u => u.Email.ToLower() == mailFromClaim);
            if (user == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
