using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace web_api.Controllers
{
    /// <summary>
    /// Asset Endpoint
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/asset")]
    public class AssetController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _assetPath;

        /// <summary>
        /// constructor of AssetController
        /// </summary>
        /// <param name="webHostEnvironment"></param>
        /// <param name="configuration"></param>
        public AssetController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _assetPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Assets");
        }

        /// <summary>
        /// gets a List of placeable 3D objects
        /// </summary>
        /// <response code="200">when the asset directory exists</response>
        /// <response code="400">when the asset directory not exists</response>
        /// <returns>a List of filenames, stored in the asset directory </returns>
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
        /// gets a binary data from a requested placeable object
        /// </summary>
        /// <response code="200">when the asset exists</response>
        /// <response code="404">when the asset could not be readed</response>
        /// <returns>binary data from asset file</returns>
        // authorize?
        [HttpGet("glb")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/octet-stream")]
        public async Task<IActionResult> GetGlbByName(string name)
        {
            try
            {
                var glb = System.IO.File.ReadAllBytes(Path.Combine(_assetPath, name));
                return new FileContentResult(glb, "application/octet-stream");
            }
            catch (Exception ex) 
            {
                return NotFound(ex.Message);
            }
        }
    }
}
