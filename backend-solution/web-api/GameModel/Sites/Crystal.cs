﻿namespace web_api.GameModel.Sites
{
    public class Crystal : SiteBase
    {
        public SiteType SiteType { get { return SiteType.Crystal; } }

        public override void Visit(Party party)
        {

        }
    }
}