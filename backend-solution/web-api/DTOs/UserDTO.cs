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
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
