using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using web_api.DTOs;
using web_api.GameModel.Worldmap;

namespace web_api.Controllers
{
    /// <summary>
    /// Admin Endpoint
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        DataContext _context;
        WorldManager _worldManager;

        /// <summary>
        /// Constructor for Admin-Controller
        /// </summary>
        /// <param name="context">type of <c>DataContext</c> for Database interactions</param>
        public AdminController(DataContext context)
        {
            _context = context;
            _worldManager = new WorldManager();
        }

        /// <summary>
        /// saves the given list of hextiles in the database
        /// </summary>
        /// <returns><b>yes</b>, when succuessfull, <b>no</b>, if not</returns>
        ///  <remarks>
        /// Sample request:
        /// 
        ///  POST
        ///  [
        ///    {
        ///      "axialCoordinates": {
        ///        "q": -1,
        ///        "r": 2
        ///      },
        ///      "siteType": 100
        ///   }
        ///  ]
        /// </remarks>
        /// <response code="200">when the hextiles has been successfully stored in the database</response>
        /// <response code="400">if the Emailaddress is already stored in the database</response>
        /// <response code="401">if the user is not authorized</response>
        /// <response code="403">if the user has no admin permissions</response>
        /// <param name="worldGenData">List of <c>HexTileDTO</c> which should be stored in the Database</param>

        [HttpPost]
        [Route("world/save")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Produces("text/plain")]
        public async Task<ActionResult> SaveSliceLayout(List<HexTileDTO> worldGenData)
        {
            if (!isAdminFromClaim())
                return Forbid();
            
            bool success = await _worldManager.updateSliceLayout(_context, worldGenData);
            if (!success)
            {
                return BadRequest("Layout update failed!");
            }
            return Ok("World Layout was successfully updated...");
        }


        /// <summary>
        /// get's a list of hextiles from the database, based on a relative zero point
        /// </summary>
        /// <response code="200">when the user has admin rights</response>
        /// <response code="401">if the user has no admin permissions</response>
        /// <param name="RelativeZero">the relative zero point of the worlsmap slice</param>
        /// <returns>a List of <c>HexTile</c> </returns>
        [HttpPost]
        [Route("world/get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
        public async Task<ActionResult> GetSliceToEdit(HexTileDTO RelativeZero)
        {
            if (!isAdminFromClaim())
                return Unauthorized();

            List<HexTile> result = await WorldManager.GetSliceAsync(_context, RelativeZero, 5);
            return Ok(result);
        }


        /// <summary>
        /// deletes an user from the database
        /// </summary>
        /// <response code="200">when the current user has admin rights and the user is deleted</response>
        /// <response code="401">if the user has no admin permissions</response>
        [HttpDelete]
        [Route("user/delete")]
        public async Task<ActionResult> DeleteUser()
        {
            if (!isAdminFromClaim())
                return Unauthorized();

            throw new NotImplementedException("Delete user feature isn't implemented, jet");
        }


        /// <summary>
        /// checks weather the current user has admin permissions or not
        /// </summary>
        /// <returns></returns>
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
