using web_api.GameModel;
using web_api.GameModel.Sites;

namespace web_api.Services
{
    public class DemoWorldManager
    {
        private int Id { get; set; }
        private List<HexTile> _worldMap { get; set; } = new List<HexTile>();
        private List<SiteBase> _sites { get; set; } = new List<SiteBase>();
        private bool _isInitialized { get; set; }

        public DemoWorldManager() { }

        public void LoadWorldMap() { }
        public void InitObstacles() { }
        public void InitSettlements() { }
        public void InitPerils() { }
        public void PlaceTreasures() { }
        public void PlaceGuards() { }

    }
}
