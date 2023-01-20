using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using web_api.GameModel;

namespace web_api.Controllers
{
    [ApiController]
    public class AdminWorldController : ControllerBase
    {
        DataContext _context;
        DemoWorldManager _worldManager;

        public AdminWorldController(DataContext context)
        {
            _context = context;
            _worldManager = new DemoWorldManager();
        }

        [Authorize]
        [HttpPost]
        [Route("api/admin/world/init")]
        public async Task<ActionResult> InitWorld(List<HexTileDto> worldGenData)
        {
            if (!isAuthorized())
                return Unauthorized();
            
            bool success = await _worldManager.InitWorld(_context, worldGenData);
            if (!success)
            {
                return BadRequest("no");
            }
            return Ok("yes");
        }

        [Authorize]
        [HttpDelete]
        [Route("api/admin/user/delete")]
        public async Task<ActionResult> DeleteUser()
        {
            if (!isAuthorized())
                return Unauthorized();


            return Ok();
        }

        private bool isAuthorized()
        {
            var mailFromClaim = User.Claims.Where(c => c.Type == ClaimTypes.Email).FirstOrDefault().Value.ToLower();
            var user = _context.Users.Where(u => u.Email.ToLower() == mailFromClaim).FirstOrDefault();
            if (user != null)
            {
                if (user.IsAdmin)
                    return true;
            }

            return false;
        }
    }
}
