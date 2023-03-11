namespace web_api.GameModel.Sites
{
    public class Chest : SiteBase
    {
        /// <summary>
        /// 
        /// </summary>
        public SiteType SiteType { get { return SiteType.Chest; } }

        public int ElectrumAmount { get; set; } = 100;

        public override void Visit(Party party)
        {
            party.Electrum += ElectrumAmount;
        }
    }
}
