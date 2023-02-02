namespace web_api.GameModel.OGIs
{
    public class TravelOGI : OngoingGameplayInteraction
    {
        public Party? Party { get; set; }
        public InteractionType Type { get { return InteractionType.Travel; } }

        public override async Task ExecuteSelf(DataContext context)
        {
            //...
            context.TravelOGIs.Remove(this);
            //...
        }
    }
}
