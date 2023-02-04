using System.Diagnostics;
using web_api.GameModel.Creatures;

namespace web_api.DTOs
{
    /// <summary>
    /// Data Transfer Object for Users
    /// </summary>
    [DebuggerDisplay("Email: {Email}, Password: {Password}")]
    public class UserDTO
    {
        /// <summary>
        /// gets or sets the email address of the user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// gets or sets the password of the user
        /// </summary>
        public string Password { get; set; }
    }
}
