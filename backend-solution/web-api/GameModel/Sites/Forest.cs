﻿namespace web_api.GameModel.Sites
{
    public class Forest : SiteBase
    {
        public SiteType SiteType { get { return SiteType.Forest; } }

        public override void Visit(Party party)
        {

        }
    }
}