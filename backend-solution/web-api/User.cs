using web_api.GameModel.Creatures;

namespace web_api
{
    public class User
    {
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

        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime? RegistrationTime { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? TokenCreated { get; set; }
        public DateTime? TokenExpires { get; set; }
        public Avatar? Avatar { get; set; }
    }
}
