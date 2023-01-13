using web_api.GameModel.Sites;

namespace web_api.GameModel
{
    public class DemoWorldManager
    {
        private List<SiteBase> _sites { get; set; } = new List<SiteBase>();

        public async Task<bool> InitWorld(DataContext context, List<HexTileDto> worldGenData) {
            foreach (var item in worldGenData)
            {
                // validate type-id
                // validate validate coordinates
                // validate placement
                if (false)
                {
                    return false;
                }
            }
            // write placement to context
            return true;
        }
    }
}
