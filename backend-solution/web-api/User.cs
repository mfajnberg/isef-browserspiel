using System.Diagnostics;
using web_api.GameModel.Creatures;

namespace web_api
{
    /// <summary>
    /// represents the current user
    /// </summary>
    [DebuggerDisplay("Id: {Id}, Email: {Email}, active: {IsActive}, admin: {IsAdmin}")]
    public class User
    {
        /// <summary>
        /// Constructor of User class
        /// </summary>
        /// <param name="email">emailadress of the user</param>
        /// <param name="passwordHash">password hash of the users</param>
        /// <param name="passwordSalt">password salt of the user</param>
        /// <param name="id">user id</param>
        /// <param name="isActive">proptery which dertermines if the user is active</param>
        /// <param name="registrationTime">registration time</param>
        /// <param name="lastLoginTime">last login occured when</param>
        public User(string email, byte[] passwordHash, byte[] passwordSalt, int id = 0,
            bool isActive = false, DateTime? registrationTime = null, DateTime? lastLoginTime = null)
        {
            Id = id;
            Email = email;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            IsActive = isActive;
            RegistrationTime = registrationTime;
            LastLoginTime = lastLoginTime;
        }

        /// <summary>
        /// gets or sets the unique id of the user
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// gets or sets the email-adress of the user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// gets or sets the users password hash
        /// </summary>
        public byte[] PasswordHash { get; set; }

        /// <summary>
        /// gets or sets the users password salt
        /// </summary>
        public byte[] PasswordSalt { get; set; }

        /// <summary>
        /// gets or sets the porterty if the user account is already confirmed
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// gets or sets the property if the current user is an admin
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// gets or sets the DataTime value when the user was registerd
        /// </summary>
        public DateTime? RegistrationTime { get; set; }

        /// <summary>
        /// gets or sets the DateTime value when the user the last time was logged in 
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// gets or sets the current refresh token
        /// </summary>
        public string? RefreshToken { get; set; }

        /// <summary>
        /// gets or sets the DateTime value, when the refresh token was created
        /// </summary>
        public DateTime? TokenCreated { get; set; }

        /// <summary>
        /// gets or sets the DateTime value, when the refresh token will expires
        /// </summary>
        public DateTime? TokenExpires { get; set; }

        /// <summary>
        /// gets or sets the avater of the user
        /// </summary>
        public Avatar? Avatar { get; set; }
    }
}
