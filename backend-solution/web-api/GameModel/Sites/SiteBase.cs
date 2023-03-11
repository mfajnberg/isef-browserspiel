using System.Diagnostics;

namespace web_api.GameModel.Sites
{
    /// <summary>
    /// represents a Site in the worldmap
    /// </summary>
    [DebuggerDisplay("Id:{Id}, Type:{Type}")]
    public abstract class SiteBase
    {
        /// <summary>
        /// gets or sets the id of the site
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// gets or sets the <c>SiteType</c>  of the site
        /// </summary>
        public SiteType Type { get; set; }


        public abstract void Visit(Party party);

        
    }

   
}
