using System.Text.Json.Serialization;
using web_api.GameModel.Sites;
using web_api.GameModel.Worldmap;

namespace web_api.DTOs
{
    public class HexTileDTO
    {
        public HexVector AxialCoordinates { get; set; }
        public SiteType SiteType { get; set; }
    }
}
