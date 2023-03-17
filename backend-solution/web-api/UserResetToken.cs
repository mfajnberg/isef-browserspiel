namespace web_api
{
  /// <summary>
  /// Reset Tokens 
  /// </summary>
    public class UserResetToken
    {

        public int Id { get; set; } 

        public string Email { get; set; }

        public string Token { get; set; }

        public DateTime ExpiresAt { get; set; } 

    }
}
