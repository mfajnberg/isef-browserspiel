using Microsoft.AspNetCore.Mvc;

namespace web_api.Controllers
{
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly string _assetPath;
        public AssetController(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _assetPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Assets");
        }

        // authorize?
        [HttpGet("names")]
        public async Task<ActionResult> GetNamesOfPlacable()
        {
            List<string> paths = new List<string>();
            foreach (string path in Directory.GetFiles(_assetPath))
            {
                FileInfo file = new FileInfo(path);
                paths.Add(file.Name);
            }
            return Ok(paths);
        }

        // authorize?
        [HttpGet("model")]
        public async Task<FileContentResult> GetTestGlb(string name)
        {
            var myfile = System.IO.File.ReadAllBytes(
                Path.Combine(_assetPath, "Hex_meter.glb"));

            return new FileContentResult(myfile, "application/octet-stream");
        }
    }
}
