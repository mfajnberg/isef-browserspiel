using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_api.GameModel;

namespace web_api.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<ActionResult> InitWorld()
        {
            _worldManager.InitObstacles(_context);
            _worldManager.InitSettlements(_context);
            _worldManager.InitPerils(_context);

            return Ok();
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> DeleteUser()
        {

            return Ok();
        }
    }
}
