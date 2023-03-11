namespace web_api.GameModel.Sites
{
    public class House : SiteBase
    {
        public SiteType SiteType { get { return SiteType.House; } }

        public override void Visit(Party party)
        {

        }
    }
}
