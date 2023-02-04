namespace web_api.GameModel.OGIs
{
    /// <summary>
    /// represents a specific Traveling OngoingGamplayInteraction
    /// </summary>
    public class TravelOGI : OngoingGameplayInteraction
    {
        /// <summary>
        /// gets the <c>InteractionType</c> as InteractionType.Travel
        /// </summary>
        public new InteractionType Type { get { return InteractionType.Travel; } }

        /// <summary>
        /// Executes this Travel OGI immediately and removes it from the Database
        /// </summary>
        /// <param name="context"><c>DataContext</c> for Database interactions</param>
        /// <returns></returns>
        public override async Task ExecuteSelf(DataContext context)
        {
            //...
            context.TravelOGIs.Remove(this);
            //...
        }
    }
}
