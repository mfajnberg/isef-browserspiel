using System.Text.Json.Serialization;

namespace web_api.GameModel
{
    public class HexVector
    {
        [JsonPropertyName("Q")]
        public int AxialQ { get; set; }
        [JsonPropertyName("R")]
        public int AxialR { get; set; }
        public HexVector(int axialQ, int axialR)
        {
            AxialQ = axialQ;
            AxialR = axialR;
        }
    }
}
