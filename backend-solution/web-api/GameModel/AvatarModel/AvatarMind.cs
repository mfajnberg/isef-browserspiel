namespace web_api.GameModel.AvatarModel
{
    public class AvatarMind : AvatarTrait
    {
        public int Intellect { get; set; }
        public int Discipline { get; set; }

        public AvatarMind(int intellect, int discipline)
        {
            Label = "Mind";
            Discipline = discipline;
            Intellect = intellect;
        }
    }
}
