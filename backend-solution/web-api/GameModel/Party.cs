using Microsoft.EntityFrameworkCore.Diagnostics;
using System.ComponentModel;
using web_api.GameModel.Creatures;
using web_api.GameModel.Items;

namespace web_api.GameModel
{
    public class Party 
    {
        public Party()
        {
            Items = new List<Item>();
            Members = new List<Creature>();
        }

        public int Id { get; set; }

        public Creature Leader { get; set; }

        public List<Creature> Members {get ; set;}

        public HexTile Location { get; set; }

        public HexTile? Destination { get; set; } 

        public List<Item> Items { get; set; }

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
            {
                Destination = null;
            }
        }


    }
}
