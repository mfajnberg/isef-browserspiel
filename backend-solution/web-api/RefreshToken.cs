using System.Diagnostics;

namespace web_api
{
    /// <summary>
    /// a refresh token which is stored in the users cookies
    /// </summary>
    [DebuggerDisplay("Token: {Token}, Created: {Created}, Expires:{Expires}")]
    public class RefreshToken
    {
        /// <summary>
        /// the token 
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// gets or sets the DateTime value when the token was created
        /// </summary>
        public DateTime Created { get; set; } = DateTime.Now;

        /// <summary>
        /// gets or sets the DateTime value when the token will be outdated
        /// </summary>
        public DateTime Expires { get; set; }
    }
}
