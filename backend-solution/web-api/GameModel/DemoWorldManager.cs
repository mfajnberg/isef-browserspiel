using web_api.GameModel.Sites;

namespace web_api.GameModel
{
    public class DemoWorldManager
    {
        private List<SiteBase> _obstacles { get; set; } = new List<SiteBase>();
        private List<SiteBase> _settlements { get; set; } = new List<SiteBase>();
        private List<SiteBase> _perils { get; set; } = new List<SiteBase>();

        public void InitObstacles(DataContext context) { }
        public void InitSettlements(DataContext context) { }
        public void InitPerils(DataContext context) { }

    }
}
