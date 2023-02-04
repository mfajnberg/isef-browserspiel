namespace web_api.GameModel.Sites
{
    /// <summary>
    /// Different Types of Sites
    /// </summary>
    public enum SiteType
    {
        /// <summary>
        /// default, represents a empty HexTile
        /// </summary>
        Empty = 0,

        /// <summary>
        /// represents a HexTile with Obstacle
        /// </summary>
        Obstacle = 100,

        /// <summary>
        /// represents a HexTile with Cluster of Trees as Obstacle
        /// </summary>
        TreeCluster1 = 101,

        /// <summary>
        /// represens a interactive HexTile
        /// </summary>
        Interactive = 200,

        /// <summary>
        /// 
        /// </summary>
        FieldCamp1 = 201
    }
}
