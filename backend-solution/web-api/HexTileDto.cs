using System.Text.Json.Serialization;
using web_api.GameModel;

namespace web_api
{
    public class HexTileDto
    {
        public HexVector Vector { get; set; }
        public int SiteBaseId { get; set; }
        public HexTileDto(int axialQ, int axialR, int siteBaseId)
        {
            Vector = new HexVector(axialQ, axialR);
            SiteBaseId = siteBaseId;
        }
    }
}
