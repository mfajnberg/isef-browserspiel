using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_api.DTOs;
using web_api.GameModel.Sites;

namespace web_api.GameModel.Worldmap
{
    /// <summary>
    /// auxiliery class to manage the world
    /// </summary>
    public class WorldManager
    {
        /// <summary>
        /// saves a set of <c>HexTiles</c> to the database
        /// </summary>
        /// <param name="context"><c>DataContext</c> for Database interactions</param>
        /// <param name="worldGenData">the <c>HexTile</c> elements </param>
        /// <returns></returns>
        public async Task<bool> updateSliceLayout(DataContext context, List<HexTileDTO> worldGenData)
        {
            foreach (var item in worldGenData)
            {
                HexTile hexTile = new HexTile()
                {
                    AxialQ = item.AxialCoordinates.Q,
                    AxialR = item.AxialCoordinates.R,
                    Site = CreateSite(item.SiteType)
                    
                };
                if ((int)item.SiteType > 100)
                //if ((int)item.SiteType > 100 && (int)item.SiteType < 200)
                    hexTile.IsBlocked = true;

                var dbHexTile = context.HexTiles.Where(h => h.AxialQ == hexTile.AxialQ && h.AxialR == hexTile.AxialR).FirstOrDefault();
                if (dbHexTile == null) 
                {
                    context.HexTiles.Add(hexTile);
                }
                else
                {
                    // Todo: replace empty hextiles
                    dbHexTile.Site = hexTile.Site;
                    dbHexTile.IsBlocked = hexTile.IsBlocked;
                }
 
            }

            await context.SaveChangesAsync();

            // write placement to context
            return true;
        }

        /// <summary>
        /// creates a new <c>SiteBase</c> from the given <c>SiteType</c>
        /// </summary>
        /// <param name="siteType"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
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

                case SiteType.Forest:
                    return new Forest();

                case SiteType.Banner:
                    return new Banner();

                case SiteType.Camp:
                    return new Camp();

                case SiteType.House:
                    return new House();

                case SiteType.Chest:
                    return new Chest();

                case SiteType.Crystal:
                    return new Crystal();

                case SiteType.AncientTree:
                    return new AncientTree();

            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a slice of the worldmap, at the given relativ zero, 
        /// </summary>
        /// <param name="context">DataContext for Database interactions</param>
        /// <param name="RelativeZero">a <c>HexTileDTO</c> which represents the middle of the slice</param>
        /// <param name="radius">amount of rings that should be calculated</param>
        /// <returns></returns>
        public static async Task<List<HexTile>> GetSliceAsync(DataContext context, HexTileDTO RelativeZero, int radius = 3)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        internal static List<HexTile> ValidatePath(DataContext context, List<HexTileDTO> path)
        {
            List<HexTile> result = new();
            foreach (var tile in path)
            {
                var dbTile = context.HexTiles.Where(h => h.AxialR == tile.AxialCoordinates.R
                                        && h.AxialQ == tile.AxialCoordinates.Q).First();

                if (dbTile == null)
                    throw new Exception("ups, that was wrong");

                if (!dbTile.CanBeTraveled())
                    throw new Exception("tile cannot be reached");

                result.Add(dbTile);
            }
            return result;
        }
    }
}
