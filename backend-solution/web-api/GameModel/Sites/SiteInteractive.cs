namespace web_api.GameModel.Sites
{
    /// <summary>
    /// Represents a interactive site in the worldmap
    /// </summary>
    public class SiteInteractive : SiteBase
    {
        /// <summary>
        /// get the <c>SiteType</c> as SiteType.Interactive
        /// </summary>
        public SiteType SiteType { get { return SiteType.Interactive; } }
    }
}
