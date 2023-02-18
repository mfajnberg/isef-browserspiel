using Microsoft.EntityFrameworkCore;
using web_api.GameModel;
using web_api.GameModel.Creatures;

namespace web_api.DTOs
{
    /// <summary>
    /// Data Transport Object for authentication
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("AvatarId={Avatar.Id}, PartyId={Party.Id}, isAdmin={IsAdmin}")]
    public class AuthResponseDTO
    {
        /// <summary>
        /// gets or sets the access token
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// gets or sets the avatar
        /// </summary>
        public Avatar? Avatar { get; set; }

        /// <summary>
        /// gets or sets the party
        /// </summary>
        public Party? Party { get; set; }

        /// <summary>
        /// gets or sets the attribute if the user is admin
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// constructor of AuthResponseDTO
        /// </summary>
        /// <param name="context"><c>DataContext</c> for Database interactions</param>
        /// <param name="user">the user who needs authentication</param>
        /// <param name="accessToken">the new created accesstoken</param>
        /// <returns></returns>
        public static AuthResponseDTO CreateResponse(DataContext context, User user, string accessToken)
        {
            AuthResponseDTO response = new AuthResponseDTO();
            response.AccessToken = accessToken;
            response.Avatar = user.Avatar;
            Party? userParty;
            if (user.Avatar != null)
            {
                userParty = context.Parties.Include(p => p.Location).Where(p => p.Id == user.Avatar.PartyId).FirstOrDefault();
                response.Party = userParty;
            }
            response.IsAdmin = user.IsAdmin;

            return response;
        }
    }

}
