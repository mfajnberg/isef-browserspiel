using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        [Route("api/admin/world/init")]
        public async Task<ActionResult> InitWorld(List<HexTileDto> worldGenData)
        {
            bool success = await _worldManager.InitWorld(_context, worldGenData);
            if (!success)
            {
                return BadRequest("no");
            }
            return Ok("yes");
        }

        [HttpDelete]
        [Route("api/admin/user/delete")]
        public async Task<ActionResult> DeleteUser()
        {
            return Ok();
        }
    }
}
