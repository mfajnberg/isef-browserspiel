namespace web_api.GameModel.AvatarModel
{
    public class AvatarTrait
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string IconUri { get; set; }
        public int Score { get; set; }
        public AvatarTrait() { }
    }
}