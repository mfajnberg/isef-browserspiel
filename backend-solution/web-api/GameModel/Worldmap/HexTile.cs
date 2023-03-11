using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using web_api.GameModel.Sites;

namespace web_api.GameModel.Worldmap
{
    /// <summary>
    /// Represents a <c>HexTile</c> of the worldmap, which can contain a site
    /// </summary>
    public class HexTile
    {
        /// <summary>
        /// gets the current coordinates as <c>HexVector</c>
        /// </summary>
        /// <returns>a <c>HexVector</c> of the Coordinates <c>AxialQ</c> and <c>AxialR</c></returns>
        public HexVector GetAxialCoordinates() { return new HexVector(AxialQ, AxialR); }

        /// <summary>
        /// gets or sets the Q Coordinate 
        /// </summary>
        [Key]
        [JsonPropertyName("Q")]
        public int AxialQ { get; set; }

        /// <summary>
        /// gets or sets the R Coordinate
        /// </summary>
        [Key]
        [JsonPropertyName("R")]
        public int AxialR { get; set; }

        /// <summary>
        /// gets or sets the site which is placed on the HexTile
        /// </summary>
        public virtual SiteBase? Site { get; set; }

        /// <summary>
        /// gets or sets a property, if an avatar can access the HexTile
        /// </summary>
        public bool IsBlocked { get; set; } = false;


        // The following properties help with pathfinding and are thus not persisted

        [JsonIgnore]
        [NotMapped]
        public int GCost { get; set; }
        [JsonIgnore]
        [NotMapped]
        public int HCost { get; set; }
        [JsonIgnore]
        [NotMapped]
        public int FCost { get; set; }
        [JsonIgnore]
        [NotMapped]
        public HexTile Previous { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public int TravelTime { get; internal set; } = 7;

        internal IEnumerable<HexTile> GetNeighbors()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal bool CanBeTraveled()
        {
            return !IsBlocked;
            // ToDo: implement other traveling restrictions
        }

        internal void Touch(Party party)
        {
            if (Site != null)
                Site.Visit(party);
            else
                ConsoleLogger.LogWarning(" > Site is empty");

        }
    }
}
