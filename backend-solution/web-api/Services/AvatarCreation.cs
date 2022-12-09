using Microsoft.AspNetCore.Mvc;
using web_api.GameModel.AvatarModel;

namespace web_api.Services
{
    public static class AvatarCreation
    {
        static List<Avatar> AvatarOptions = new List<Avatar>() {
            new Avatar() {
                Name = "Marsilio",
                Body = new AvatarBody(16, 13),
                Senses = new AvatarSenses(12, 10),
                Mind = new AvatarMind(17, 15)
            },
            new Avatar() {
                Name = "Leito",
                Body = new AvatarBody(11, 16),
                Senses = new AvatarSenses(12, 17),
                Mind = new AvatarMind(13, 10)
            },
            new Avatar() {
                Name = "Struppi",
                Body = new AvatarBody(10, 10),
                Senses = new AvatarSenses(10, 10),
                Mind = new AvatarMind(10, 10)
            }
        };

        public static List<Avatar> GetAvatarList()
        {
            return AvatarOptions;
        }

        public static Avatar SelectFromAvatarList(string name)
        {
            Avatar? result = AvatarOptions.Find(x => x.Name == name);
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