namespace web_api.GameModel.AvatarModel
{
    public class Avatar
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AvatarBody Body { get; set; }
        public AvatarMind Mind { get; set; }
        public AvatarSenses Senses { get; set; }
    }
}