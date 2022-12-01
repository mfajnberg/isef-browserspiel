﻿namespace web_api.GameModel
{
    public class User
    {
        public User(
            string email, byte[] passwordHash, byte[] passwordSalt, 
            int id = 0, bool isActive = false, bool receiveNewsletter = false, 
            DateTime? registrationTime = null, DateTime? lastLoginTime = null)
        {
            Id = id;
            Email = email;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            IsActive = isActive;
            ReceiveNewsletter = receiveNewsletter;
            RegistrationTime = registrationTime;
            LastLoginTime = lastLoginTime;
        }

        public int Id { get; set; }
        
        public string Email { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
        public bool IsActive { get; set; }
        public bool ReceiveNewsletter { get; set; }
        public DateTime? RegistrationTime{ get; set; }
        public DateTime? LastLoginTime{ get; set; }

    }
}
