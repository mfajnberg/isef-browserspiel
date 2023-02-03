using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_api.DTOs;
using web_api.GameModel.Sites;

namespace web_api.GameModel.Worldmap
{
    public class WorldManager
    {
        public async Task<bool> updateSliceLayout(DataContext context, List<HexTileDTO> worldGenData)
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
                if ((int)item.SiteType > 100 && (int)item.SiteType < 200)
                    hexTile.IsBlocked = true;

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

        public static async Task<List<HexTile>> GetSliceAsync(DataContext context, HexTileDTO RelativeZero, int radius = 2)
        {
            List<HexTile> result = new List<HexTile>();
            List<HexVector> gridVectors = HexVector.MakeGridVectors(radius);
            //bool createdHex = false;

            foreach (var vec in gridVectors)
            {
                vec.Q += RelativeZero.AxialCoordinates.Q;
                vec.R += RelativeZero.AxialCoordinates.R;
                HexTile? hex = context.HexTiles.Include(h => h.Site).Where(h => h.AxialQ == vec.Q && h.AxialR == vec.R).FirstOrDefault();

                /*  // The following is to be replaced with more sophisticated procedural "on the fly" generation.
                    // It's also broken due to race conditions. Instantiate a seperate context for the write operations.
                if (hex == null)
                {
                    hex = new HexTile() { AxialQ = vec.Q, AxialR = vec.R };
                    context.HexTiles.Add(hex);
                    createdHex = true;
                }
                */

                if (hex != null)
                    result.Add(hex);
            }
            //if (createdHex)
            //    await context.SaveChangesAsync();

            return result;
        }


    }
}
