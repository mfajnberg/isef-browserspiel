﻿namespace web_api.GameModel.Sites
{
    public class Banner : SiteBase
    {
        public SiteType SiteType { get { return SiteType.Banner; } }

        public override void Visit(Party party)
        {

        }
    }
}