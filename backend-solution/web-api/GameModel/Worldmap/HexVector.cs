using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace web_api.GameModel.Worldmap
{
    /// <summary>
    /// contains the axial Q and R coordinates 
    /// </summary>
    [NotMapped]
    public class HexVector
    {
        /// <summary>
        /// constructor of <c>HexVector</c>
        /// </summary>
        /// <param name="Q">the axial Q coordinate</param>
        /// <param name="R">the axial R coordinate</param>
        public HexVector(int Q, int R)
        {
            this.Q = Q;
            this.R = R;
        }

        /// <summary>
        /// gets or sets the axial Q coordinate
        /// </summary>
        public int Q { get; set; }

        /// <summary>
        /// gets or sets the axial R coordinate
        /// </summary>
        public int R { get; set; }
        
        /// <summary>
        /// creates a List of <c>HexVector</c>s in the given radius
        /// </summary>
        /// <param name="radius">how many rings must be created</param>
        /// <returns>a <c>List</c> of <c>HexVectors</c> that represents a slice of a map</returns>
        public static List<HexVector> MakeGridVectors(int radius)
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
