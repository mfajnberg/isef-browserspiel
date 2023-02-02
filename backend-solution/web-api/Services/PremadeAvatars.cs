using Microsoft.AspNetCore.Mvc;
using web_api.GameModel;
using web_api.GameModel.Creatures;

namespace web_api.Services
{
    public static class PremadeAvatars
    {
        static List<Avatar> PremadeOptions = new List<Avatar>() {
            new Avatar() {
                Name = "Eliana Dawnbreak",

                Intellect = 13,
                Discipline = 17,
                Power = 11,
                Agility = 19,
                Lucidity = 9,
                Charisma = 8,

                Alignment = 5,
                Temperament = 5,
                Morale = 0,
                Party = new Party(),
            },
            new Avatar() {
                Name = "Leito Froste",

                Intellect = 10,
                Discipline = 8,
                Power = 10,
                Agility = 15,
                Lucidity = 14,
                Charisma = 20,

                Alignment = 0,
                Temperament = 5,
                Morale = 10,
                Party = new Party(),
            },
            new Avatar() {
                Name = "Marsilio Mirandola",

                Intellect = 17,
                Discipline = 12,
                Power = 17,
                Agility = 9,
                Lucidity = 12,
                Charisma = 10,

                Alignment = -5,
                Temperament = -10,
                Morale = 5,
                Party = new Party(),
            }
        };

        public static List<Avatar> GetAvatarList()
        {
            return PremadeOptions;
        }

        public static Avatar SelectFromAvatarList(string name)
        {
            Avatar? result = PremadeOptions.Find(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            if (result == null)
            {
                if (string.IsNullOrEmpty(name))
                {
                    throw new Exception("Bad Request");
                }
                throw new Exception("Fehler im Server");
            }

            return result;
        }
    }
}