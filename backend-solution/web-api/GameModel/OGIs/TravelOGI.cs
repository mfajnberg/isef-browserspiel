﻿using web_api.GameModel.Worldmap;

namespace web_api.GameModel.OGIs
{
    /// <summary>
    /// represents a specific Traveling OngoingGamplayInteraction
    /// </summary>
    public class TravelOGI : OngoingGameplayInteraction
    {
        /// <summary>
        /// gets the <c>InteractionType</c> as InteractionType.Travel
        /// </summary>
        public new InteractionType Type { get { return InteractionType.Travel; } }


        public int AxialQ { get; set; }

        public int AxialR { get; set; }

        /// <summary>
        /// Executes this Travel OGI immediately and removes it from the Database
        /// </summary>
        /// <param name="context"><c>DataContext</c> for Database interactions</param>
        /// <returns></returns>
        public override async Task ExecuteSelf(DataContext context)
        {
            Party party = context.Parties.Where (x => x.Id == PartyId).FirstOrDefault();
            HexTile hexTile = context.HexTiles.Where(h => h.AxialR == AxialR && h.AxialQ == AxialQ).FirstOrDefault();

            party.UpdateLocation(hexTile);
            context.TravelOGIs.Remove(this);
            
            await context.SaveChangesAsync();
        }
    }
}
