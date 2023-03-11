using Microsoft.EntityFrameworkCore.Diagnostics;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using web_api.GameModel.Creatures;
using web_api.GameModel.Items;
using web_api.GameModel.OGIs;
using web_api.GameModel.Worldmap;

namespace web_api.GameModel
{
    /// <summary>
    /// represents a party of the current user
    /// </summary>
    public class Party 
    {
        /// <summary>
        /// the constructor of a Party. Initializes the Inventory and the member list
        /// </summary>
        public Party()
        {
            Inventory = new List<Item>();
            Members = new List<Creature>();
        }

        /// <summary>
        /// get or sets the id of a party
        /// </summary>
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        /// <summary>
        /// gets or sets the avatar Id from the leader
        /// </summary>
        [ForeignKey("Creature")]
        public int? LeaderId { get; set; }

        /// <summary>
        /// gets or sets the party members
        /// </summary>
        public virtual List<Creature> Members {get ; set; } // possibly hidden

        /// <summary>
        /// gets or sets the item List the party owns
        /// </summary>
        public virtual List<Item> Inventory {get ; set; } // possibly hidden

        /// <summary>
        /// gets or sets the current location 
        /// </summary>
        public HexTile Location { get; set; }

        /// <summary>
        /// gets or sets the destination the party would travel to
        /// </summary>
        public HexTile? Destination { get; set; }  // possibly hidden

        /// <summary>
        /// gets or sets the amount of electrum (coin)
        /// </summary>
        public int Electrum { get; set; }

        /// <summary>
        /// gets or sets the current OGI
        /// </summary>
        [ForeignKey("OGI")]
        public int? ActionId { get; set; }

        /// <summary>
        /// set the given destination to the new destination of the party
        /// </summary>
        /// <param name="_destination">the new Destination</param>
        /// <param name="dataContext"><c>DataContex</c> for Database interaction</param>
        /// <returns></returns>
        public async Task StartTravelingAsync(HexTile _destination, DataContext dataContext)
        {
            Destination = _destination;
            await SaveChangesAsync(dataContext);
        }

        /// <summary>
        /// Saves the current party to the Database
        /// </summary>
        /// <param name="dataContext"></param>
        /// <returns></returns>
        private async Task SaveChangesAsync(DataContext dataContext)
        {
            await dataContext.SaveChangesAsync();
        }

        /// <summary>
        /// determines if the party ist currently traveling
        /// </summary>
        /// <returns><b><c>true</c></b> when the party travels to a destination, <c><b>false</b></c> if not</returns>
        public bool IsTraveling()
        {
            return Destination != null;
        }

        /// <summary>
        /// set the current location 
        /// </summary>
        /// <param name="location">new current location</param>
        public void UpdateLocation(HexTile location)
        {
            Location = location;

            Location.Touch(this);

            if (location == Destination)
                Destination = null;
        }


    }
}
