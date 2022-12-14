namespace web_api.GameModel
{
    public class WorldmapSlice
    {
        public int RootSize { get; set; }
        public List<HexTile> TileGrid { get; set; }

        internal void LoadTiles() 
        {

        }

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

        public int Distance(HexTile neighbor, HexTile destination)
        {
            throw new NotImplementedException();
        }
    }
}

