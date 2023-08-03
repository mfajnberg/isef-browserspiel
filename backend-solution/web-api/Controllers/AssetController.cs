using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace web_api.Controllers
{
    // todo: cache assets in the client to drastically reduce server load

    /// <summary>
    ///     Controller for downloading various in-game assets
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/asset")]
    public class AssetController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _assetPath;

        /// <param name="webHostEnvironment">
        ///     Used for easy retreival of the full path of the local asset directory
        /// </param>
        public AssetController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _assetPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Assets");
        }

        /// <summary>
        ///     Returns a List containting the file names of all currently placeable 3D objects.
        /// </summary>
        /// 
        /// <response code="400">
        ///     when the asset directory does not exists
        /// </response>
        /// 
        /// <returns>
        ///     a List of filenames, stored in the asset directory
        /// </returns>
        // authorize?
        [HttpGet("names")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<ActionResult> GetNamesOfPlacable()
        {
            if (!Directory.Exists(_assetPath)) 
                return NotFound();

            List<string> paths = new List<string>();
            foreach (string path in Directory.GetFiles(_assetPath))
            {
                FileInfo file = new FileInfo(path);
                paths.Add(file.Name);
            }
            return Ok(paths);
        }

        /// <summary>
        ///     Retreives vertex data of a placeable 3d object from the file system, based on a given input file name
        /// </summary>
        /// 
        /// <response code="404">
        ///     The asset with the given name could not be read
        /// </response>
        /// 
        /// <returns>
        ///     The binary data from asset file encoded as a byte array
        /// </returns>
        // authorize?
        [HttpGet("get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/octet-stream")]
        public async Task<IActionResult> Get3dAssetByName(string name)
        {
            try
            {
                var asset = System.IO.File.ReadAllBytes(Path.Combine(_assetPath, name));
                return new FileContentResult(asset, "application/octet-stream");
            }
            catch (Exception ex) 
            {
                return NotFound(ex.Message);
            }
        }
    }
}
