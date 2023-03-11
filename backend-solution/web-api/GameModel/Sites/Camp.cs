namespace web_api.GameModel.Sites
{
    public class Camp : SiteBase
    {
        public SiteType SiteType { get { return SiteType.Camp; } }

        public override void Visit(Party party)
        {

        }
    }
}
