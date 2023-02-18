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
        Forest = 101,

        /// <summary>
        /// represens a interactive HexTile
        /// </summary>
        Interactive = 200,

        Banner = 201,
        Camp = 202,
        House = 203,
        Chest = 204,
        Crystal = 205,
        AncientTree = 206,

        /// <summary>
        /// 
        /// </summary>
        FieldCamp1 = 201
    }
}
