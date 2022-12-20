using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_api.Services;

namespace web_api.Controllers
{
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        public AssetController(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        [HttpGet("api/assets/hex-tile-glb")]
        public async Task<FileContentResult> GetHexTileGlb()
        {
            var myfile = System.IO.File.ReadAllBytes(
                Path.Combine(_webHostEnvironment.ContentRootPath, "Assets", "Hex_meter.glb"));

            return new FileContentResult(myfile, "application/octet-stream");
        }
    }
}
