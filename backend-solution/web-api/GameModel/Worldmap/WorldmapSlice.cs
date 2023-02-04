namespace web_api.GameModel.Worldmap
{
    /// <summary>
    /// represents a slice of the worldmap
    /// </summary>
    public class WorldmapSlice
    {
        /// <summary>
        /// gets or sets the max size of the worldmap
        /// </summary>
        public int RootSize { get; set; }

        /// <summary>
        /// gets or sets the list of HexTiles which represents the slice
        /// </summary>
        public List<HexTile> HexGrid { get; set; }

        internal void LoadTiles()
        {

        }

        /// <summary>
        /// AStar algorythm for finding a path between to hextiles
        /// </summary>
        /// <param name="start"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public List<HexTile> FindPathAStar(HexTile start, HexTile destination)
        {
            List<HexTile> toSearch = new List<HexTile>();
            List<HexTile> processed = new List<HexTile>();

            toSearch.Add(start);

            while (toSearch.Count != 0)
            {
                HexTile candidate = toSearch[0];

                if (candidate == destination) { break; }

                foreach (HexTile hex in toSearch)
                {
                    if (hex.FCost < candidate.FCost ||
                        hex.FCost == candidate.FCost && hex.HCost < candidate.HCost)
                    {
                        candidate = hex;
                    }
                }

                toSearch.Remove(candidate);
                processed.Add(candidate);

                foreach (HexTile neighbor in candidate.GetNeighbors())
                {
                    if (processed.Contains(neighbor)) { continue; }

                    bool inToSearch = toSearch.Contains(neighbor);
                    int newGCost = candidate.GCost + 1; // 1 is the "move cost"

                    if (newGCost < neighbor.GCost || !inToSearch)
                    {
                        neighbor.GCost = newGCost;
                        neighbor.HCost = Distance(neighbor, destination);
                        neighbor.FCost = neighbor.GCost + neighbor.HCost;
                        neighbor.Previous = candidate;
                        if (!inToSearch)
                        {
                            toSearch.Add(neighbor);
                        }
                    }
                }
            }
            List<HexTile> path = new List<HexTile>();
            HexTile node = destination;
            while (node != start)
            {
                path.Add(node);
                node = node.Previous;
            }
            return path; // reverse first
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="neighbor"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int Distance(HexTile neighbor, HexTile destination)
        {
            throw new NotImplementedException();
        }
    }
}

