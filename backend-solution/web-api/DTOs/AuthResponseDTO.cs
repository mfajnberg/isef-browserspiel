using Microsoft.EntityFrameworkCore;
using web_api.GameModel;
using web_api.GameModel.Creatures;

namespace web_api.DTOs
{
    [System.Diagnostics.DebuggerDisplay("AvatarId={Avatar.Id}, PartyId={Party.Id}, isAdmin={IsAdmin}")]
    public class AuthResponseDTO
    {
        public string AccessToken { get; set; }
        public Avatar? Avatar { get; set; }
        public Party? Party { get; set; }
        public bool IsAdmin { get; set; }

        public static AuthResponseDTO CreateResponse(DataContext context, User user, string accessToken)
        {
            AuthResponseDTO response = new AuthResponseDTO();
            response.AccessToken = accessToken;
            response.Avatar = user.Avatar;
            Party? userParty;
            if (user.Avatar != null)
                userParty = context.Parties.Where(p => p.Id == user.Avatar.PartyId).FirstOrDefault();
            response.IsAdmin = user.IsAdmin;

            return response;
        }
    }

}
