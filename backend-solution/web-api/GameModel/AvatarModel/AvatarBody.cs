namespace web_api.GameModel.AvatarModel
{
    public class AvatarBody : AvatarTrait
    {
        public int Power { get; set; }
        public int Agility { get; set; }

        public AvatarBody(int power, int agility)
        {
            Label = "Body";
            Power = power;
            Agility = agility;
        }
    }
}
