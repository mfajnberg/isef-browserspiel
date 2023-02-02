using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using web_api.GameModel.Sites;

namespace web_api.GameModel.Worldmap
{
    public class HexTile
    {
        public HexVector GetAxialCoordinates() { return new HexVector(AxialQ, AxialR); }
        [Key]
        [JsonPropertyName("Q")]
        public int AxialQ { get; set; }
        [Key]
        [JsonPropertyName("R")]
        public int AxialR { get; set; }

        public virtual SiteBase? Site { get; set; }
        public bool isBlocked { get; set; } = false;

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

        internal IEnumerable<HexTile> GetNeighbors()
        {
            throw new NotImplementedException();
        }
    }
}
