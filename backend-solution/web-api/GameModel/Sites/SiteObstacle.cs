namespace web_api.GameModel.Sites
{
    public class SiteObstacle : SiteBase
    {
        public new SiteType SiteType { get { return SiteType.Obstacle; } }

        public int testProperty { get; set; } = 12;
    }
}
