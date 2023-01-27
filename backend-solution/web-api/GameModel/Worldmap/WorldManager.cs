using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_api.DTOs;
using web_api.GameModel.Sites;

namespace web_api.GameModel.Worldmap
{
    public class WorldManager
    {
        private List<SiteBase> _sites { get; set; } = new List<SiteBase>();

        public async Task<bool> InitWorld(DataContext context, List<HexTileDTO> worldGenData)
        {
            foreach (var item in worldGenData)
            {
                // validate type-id
                // validate validate coordinates
                // validate placement
                HexTile hexTile = new HexTile()
                {
                    AxialQ = item.AxialCoordinates.Q,
                    AxialR = item.AxialCoordinates.R,
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

        public static async Task<List<HexTile>> GetSliceAsync(DataContext context, HexTileDTO RelativeZero, int radius = 5)
        {
            List<HexTile> result = new List<HexTile>();
            List<HexVector> gridVectors = HexVector.makeGridVectors(radius);
            bool createdHex = false;

            foreach (var vec in gridVectors)
            {
                vec.Q += RelativeZero.AxialCoordinates.Q;
                vec.R += RelativeZero.AxialCoordinates.R;
                HexTile? hex = context.HexTiles.Where(h => h.AxialQ == vec.Q && h.AxialR == vec.R).FirstOrDefault();

                // make special
                if (hex == null)
                {
                    hex = new HexTile() { AxialQ = vec.Q, AxialR = vec.R };
                    context.HexTiles.Add(hex);
                    createdHex = true;
                }
                result.Add(hex);
            }
            if (createdHex)
                await context.SaveChangesAsync();

            return result;
        }
    }
}
