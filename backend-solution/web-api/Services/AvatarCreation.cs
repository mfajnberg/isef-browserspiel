using Microsoft.AspNetCore.Mvc;
using web_api.GameModel;
using web_api.GameModel.AvatarModel;

namespace web_api.Services
{
    public static class AvatarCreation
    {
        static List<CreatureBase> PremadeOptions = new List<CreatureBase>() {
            new CreatureBase() {
                Name = "Marsilio",
                Intellect = 17,
                Discipline = 15,
                Power = 16,
                Agility = 13,
                Lucidity = 12,
                Charisma = 10,

                Alignment = -10,
                Temperament = -10,
                Morale = 5,
                Fellowship = new Party(),
            },
            new CreatureBase() {
                Name = "Leito",
                Intellect = 13,
                Discipline = 10,
                Power = 11,
                Agility = 16,
                Lucidity = 12,
                Charisma = 17,

                Alignment = 0,
                Temperament = 5,
                Morale = 10,
                Fellowship = new Party(),
            },
            new CreatureBase() {
                Name = "Standard-Struppi",
                Intellect = 10,
                Discipline = 10,
                Power = 10,
                Agility = 10,
                Lucidity = 10,
                Charisma = 10,

                Alignment = 0,
                Temperament = 0,
                Morale = 0,
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