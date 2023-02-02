using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace web_api.GameModel.Worldmap
{
    [NotMapped]
    public class HexVector
    {
        public int Q { get; set; }
        public int R { get; set; }
        public HexVector(int Q, int R)
        {
            this.Q = Q;
            this.R = R;
        }

        public static List<HexVector> makeGridVectors(int radius)
        {
            List<HexVector> result = new List<HexVector>{new HexVector(0, 0)};
            for (int currentRing = 1; currentRing <= radius; currentRing++) 
            {
                HexVector vectorNE = new HexVector(1 * currentRing, -1 * currentRing);
                for (int i = 0; i < currentRing; i++)
                    result.Add(new HexVector(0*i + vectorNE.Q, 1*i + vectorNE.R));

                HexVector vectorE = new HexVector(1 * currentRing, 0 * currentRing);
                for (int i = 0; i < currentRing; i++)
                    result.Add(new HexVector(-1 * i + vectorE.Q, 1 * i + vectorE.R));

                HexVector vectorSE = new HexVector(0 * currentRing, 1 * currentRing);
                for (int i = 0; i < currentRing; i++)
                    result.Add(new HexVector(-1 * i + vectorSE.Q, 0 * i + vectorSE.R));

                HexVector vectorSW = new HexVector(-1 * currentRing, 1 * currentRing);
                for (int i = 0; i < currentRing; i++)
                    result.Add(new HexVector(0 * i + vectorSW.Q, -1 * i + vectorSW.R));
                
                HexVector vectorW = new HexVector(-1 * currentRing, 0 * currentRing);
                for (int i = 0; i < currentRing; i++)
                    result.Add(new HexVector(1 * i + vectorW.Q, -1 * i + vectorW.R));
                
                HexVector vectorNW = new HexVector(0 * currentRing, -1 * currentRing);
                for (int i = 0; i < currentRing; i++)
                    result.Add(new HexVector(1 * i + vectorNW.Q, 0 * i + vectorNW.R));
            }
            return result;
        }
    }
}
