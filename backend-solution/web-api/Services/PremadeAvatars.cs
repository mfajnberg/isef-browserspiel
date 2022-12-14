using Microsoft.AspNetCore.Mvc;
using web_api.GameModel;
using web_api.GameModel.AvatarModel;

namespace web_api.Services
{
    public static class PremadeAvatars
    {
        static List<CreatureBase> PremadeOptions = new List<CreatureBase>() {
            new CreatureBase() {
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
                Fellowship = new Party(),
            },
            new CreatureBase() {
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
                Fellowship = new Party(),
            },
            new CreatureBase() {
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
                Fellowship = new Party(),
            }
        };

        public static List<CreatureBase> GetAvatarList()
        {
            return PremadeOptions;
        }

        public static CreatureBase SelectFromAvatarList(string name)
        {
            CreatureBase? result = PremadeOptions.Find(x => x.Name == name);
            if (result == null)
            {
                if (String.IsNullOrEmpty(name))
                {
                    throw new Exception("Bad Request");
                }
                throw new Exception("Fehler im Server");
            }

            return result;
        }
    }
}