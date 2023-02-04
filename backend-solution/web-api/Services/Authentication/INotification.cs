namespace web_api.Services.Authentication
{
    /// <summary>
    /// Interface for Notifications like email 
    /// </summary>
    public interface INotification
    {
        /// <summary>
        /// sends a message to a user 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="subject"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        Task<bool> SendToAsync(string message, string subject, string to);
    }
}
