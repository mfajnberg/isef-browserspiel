using Microsoft.EntityFrameworkCore;
using web_api.GameModel.Worldmap;

namespace web_api.GameModel.OGIs
{
    /// <summary>
    ///     Represents a specific ongoing traveling interaction
    /// </summary>
    public class TravelOGI : OngoingGameplayInteraction
    {
        /// <summary>
        ///     Gets the <c>InteractionType</c> as InteractionType.Travel
        /// </summary>
        public new InteractionType Type { get { return InteractionType.Travel; } }


        public int TargetAxialQ { get; set; }
        public int TargetAxialR { get; set; }

        /// <summary>
        ///     Executes this Travel OGI immediately and removes it from the Database.
        ///     Mutates the position of the traveling party, updating their current location and triggering the appropriate events based on the target hex.
        /// </summary>
        /// 
        /// <param name="context">
        ///     <c>DataContext</c> for Database interactions
        /// </param>
        public override async Task ExecuteSelf(DataContext context)
        {
            Party party = context.Parties.Where (x => x.Id == PartyId).FirstOrDefault();
            HexTile hexTile = context.HexTiles.Where(h => h.AxialR == TargetAxialR && h.AxialQ == TargetAxialQ).Include(h => h.Site).FirstOrDefault();

            party.UpdateLocation(hexTile);
            context.TravelOGIs.Remove(this);

            if (hexTile.Site?.Type == Sites.SiteType.Chest
                || hexTile.Site?.Type == Sites.SiteType.Crystal)
            {
                var chest = context.SitesChest.Where(c => c.Id == hexTile.Site.Id).FirstOrDefault();
                context.SitesChest.Remove(chest);

                hexTile.Site = null;
            }
            
            await context.SaveChangesAsync();
        }
    }
}
