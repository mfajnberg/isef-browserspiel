namespace web_api.GameModel.AvatarModel
{
    public class AvatarSenses : AvatarTrait
    {
        public int Focus { get; set; }
        public int Charisma { get; set; }

        public AvatarSenses(int focus, int charisma)
        {
            Label = "Senses";
            Focus = focus;
            Charisma = charisma;
        }
    }
}
