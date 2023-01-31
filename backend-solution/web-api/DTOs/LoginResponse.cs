using web_api.GameModel.Creatures;

namespace web_api.DTOs
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public Avatar? Avatar { get; set; }
        public bool IsAdmin { get; set; }
    }
}
