using System.Text.Json.Serialization;
using web_api.GameModel;
using web_api.GameModel.Sites;

namespace web_api
{
    public class HexTileDto
    {
        public HexVector AxialCoordinates { get; set; }
        public SiteType SiteType { get; set; }
        //public HexTileDto(int Q, int R, SiteType siteType)
        //{
        //    AxialCoordinates = new HexVector(Q, R);
        //    SiteType = siteType;
        //}

        //public HexTileDto()
        //{

        //}
    }
}
