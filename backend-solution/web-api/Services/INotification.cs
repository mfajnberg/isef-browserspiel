namespace web_api.Services
{
    public interface INotification
    {
        Task<bool> SendToAsync(string message, string subject, string to);
    }
}
