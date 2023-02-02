﻿using Microsoft.EntityFrameworkCore.Diagnostics;
using System.ComponentModel;
using System.Text.Json.Serialization;
using web_api.GameModel.Creatures;
using web_api.GameModel.Items;
using web_api.GameModel.Worldmap;

namespace web_api.GameModel
{
    public class Party 
    {
        [JsonIgnore]
        public int Id { get; set; }
        public HexTile Location { get; set; }
        public HexTile? Destination { get; set; }  // possibly hidden
        public Creature? Leader { get; set; } // possibly hidden
        public List<Creature> Members {get ; set; } // possibly hidden
        public List<Item> Items { get; set; }  // possibly hidden

        public Party()
        {
            Items = new List<Item>();
            Members = new List<Creature>();
        }

        public async Task StartTravelingAsync(HexTile _destination, DataContext dataContext)
        {
            Destination = _destination;
            await SaveChangesAsync(dataContext);
        }

        private async Task SaveChangesAsync(DataContext dataContext)
        {
            await dataContext.SaveChangesAsync();
        }

        public bool IsTraveling()
        {
            return Destination != null;
        }

        public void UpdateLocation(HexTile location)
        {
            Location = location;

            if (location == Destination)
                Destination = null;
        }


    }
}
