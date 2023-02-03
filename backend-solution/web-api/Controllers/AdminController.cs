using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using web_api.DTOs;
using web_api.GameModel.Worldmap;

namespace web_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        DataContext _context;
        WorldManager _worldManager;

        public AdminController(DataContext context)
        {
            _context = context;
            _worldManager = new WorldManager();
        }

        [HttpPost]
        [Route("world/save")]
        public async Task<ActionResult> SaveSliceLayout(List<HexTileDTO> worldGenData)
        {
            if (!isAdminFromClaim())
                return Forbid();
            
            bool success = await _worldManager.updateSliceLayout(_context, worldGenData);
            if (!success)
            {
                return BadRequest("no");
            }
            return Ok("yes");
        }

        [HttpPost]
        [Route("world/get")]
        public async Task<ActionResult> GetSliceToEdit(HexTileDTO RelativeZero)
        {
            if (!isAdminFromClaim())
                return Unauthorized();

            List<HexTile> result = await WorldManager.GetSliceAsync(_context, RelativeZero);
            return Ok(result);
        }

        [HttpDelete]
        [Route("user/delete")]
        public async Task<ActionResult> DeleteUser()
        {
            if (!isAdminFromClaim())
                return Unauthorized();

            return Ok();
        }


        private bool isAdminFromClaim()
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
