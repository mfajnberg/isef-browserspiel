using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using web_api.GameModel.Sites;

namespace web_api.GameModel
{
    public class HexTile
    {
        public int Id { get; set; }
        public HexVector GetAxialCoordinates() { return new HexVector(AxialQ, AxialR); }

        [Key]
        [JsonPropertyName("Q")]
        public int AxialQ { get; set; }
        [Key]
        [JsonPropertyName("R")]
        public int AxialR { get; set; }
        public SiteBase? Site { get; set; }
        public bool isBlocked { get; set; } = false;
        public List<HexVector> BlocksVectors { get; set; }

        [NotMapped]
        public int GCost { get; set; }
        [NotMapped]
        public int HCost { get; set; }
        [NotMapped]
        public int FCost { get; set; }
        [NotMapped]
        public HexTile Previous { get; internal set; }

        internal IEnumerable<HexTile> GetNeighbors()
        {
            throw new NotImplementedException();
        }
    }
}
