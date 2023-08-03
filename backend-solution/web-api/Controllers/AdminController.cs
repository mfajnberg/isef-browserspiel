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
    ///     Controller for secure issuing of administrative tasks concerning the world, player accounts etc.
    /// </summary>
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


        // todo: Inform the client whether or not new hex tiles would be generated at previously empty coordinates prior to communication with this endpoint.
        /// <summary>
        ///     Saves the given input list of hex tiles to the world's database, replacing current adventure-site data if needed.
        ///     Generates new hex tiles for whatever pairs of previously void coordinates are given.
        ///     Makes them appear as part of the explorable game world.
        /// </summary>
        /// 
        /// <param name="worldGenData">
        ///     List of <c>HexTileDTO</c> to be saved in the database as new hex tiles.
        /// </param>
        /// 
        /// <response code="403">
        ///     The user lacks admin permissions
        /// </response>
        /// 
        /// <response code="422">
        ///     Updating the world slice with the given input layout failed due to some internal logic issue.
        /// </response>
        /// 
        /// <remarks>
        ///  
        ///     Sample request:
        /// 
        ///     POST
        ///     
        ///     [
        ///         {
        ///             "axialCoordinates": {
        ///                 "q": -1,
        ///                 "r": 2
        ///             },
        ///             "siteType": 100
        ///         }, 
        ///         ...
        ///     ]
        ///  
        /// </remarks>
        [HttpPost]
        [Route("world/save")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [Produces("text/plain")]
        public async Task<ActionResult> SaveSliceLayout(List<HexTileDTO> worldGenData)
        {
            if (!IsAdminFromClaim())
                return Forbid();
            
            bool success = await _worldManager.updateSliceLayout(_context, worldGenData);
            if (!success)
            {
                return UnprocessableEntity("Layout update failed!");
            }

            return Ok("World Layout was successfully updated...");
        }


        /// <summary>
        ///     Returns a list of the hextiles around the given input coordinates.
        ///     Only used by admins to edit the game world. 
        /// </summary>
        /// 
        /// <param name="RelativeZero">
        ///     the relative zero point on the world map around which to load any existing hex tiles
        /// </param>
        /// 
        /// <response code="403">
        ///     The requesting client lacks admin permissions
        /// </response>
        /// 
        /// <returns>
        ///     a List of <c>HexTile</c> 
        /// </returns>
        [HttpPost]
        [Route("world/get")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Produces("application/json")]
        public async Task<ActionResult> GetSliceToEdit(HexTileDTO RelativeZero)
        {
            if (!IsAdminFromClaim())
                return Forbid();

            List<HexTile> result = await WorldManager.GetSliceAsync(_context, RelativeZero, 4);
            return Ok(result);
        }


        /// <summary>
        ///     Deletes a given input user from the database
        /// </summary>
        /// 
        /// <response code="400">
        ///     The given input user is invalid
        /// </response>
        /// 
        /// <response code="403">
        ///     The requesting client lacks admin permissions
        /// </response>
        /// 
        /// <param name="userData">
        ///     The data of user to be deleted 
        /// </param>
        [HttpDelete]
        [Route("user/delete")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Produces("text/plain")]
        public async Task<ActionResult> DeleteUser(UserDTO userData)
        {
            if (!IsAdminFromClaim())
                return Forbid();

            var user = _context.Users.Where(u => u.Email.ToLower() == userData.Email.ToLower()).FirstOrDefault();

            if (user == null)
                return BadRequest();

            _context.Users.Remove(user);
            return Ok();
        }


        /// <summary>
        ///     Checks the requesting client's admin permissions
        /// </summary>
        private bool IsAdminFromClaim()
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
