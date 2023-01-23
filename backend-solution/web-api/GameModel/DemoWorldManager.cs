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
                HexTile hexTile = new HexTile()
                {
                    AxialQ = item.AxialCoordinates.AxialQ,
                    AxialR = item.AxialCoordinates.AxialR,
                    Site = CreateSite(item.SiteType)
                };

                context.HexTiles.Add(hexTile);
            }

            await context.SaveChangesAsync();

            // write placement to context
            return true;
        }

        private SiteBase? CreateSite(SiteType siteType)
        {
            switch (siteType)
            {
                case SiteType.Empty:
                    return null;

                case SiteType.Obstacle:
                    return new SiteObstacle();

                case SiteType.Interactive:
                    return new SiteInteractive();

            }


            throw new NotImplementedException();
        }
    }
}
