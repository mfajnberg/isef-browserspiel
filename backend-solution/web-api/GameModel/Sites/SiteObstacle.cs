namespace web_api.GameModel.Sites
{
    /// <summary>
    /// represents aobstacle site in the worldmap
    /// </summary>
    public class SiteObstacle : SiteBase
    {
        /// <summary>
        /// gets the <c>SiteType</c> as SiteType.Obstacle
        /// </summary>
        public SiteType SiteType { get { return SiteType.Obstacle; } }

        public override void Visit(Party party)
        {

        }


        // ToDo: remove the unneeded testProperty
        public int testProperty { get; set; } = 12;
    }
}
