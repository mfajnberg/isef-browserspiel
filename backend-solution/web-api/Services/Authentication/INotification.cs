namespace web_api.Services.Authentication
{
    public interface INotification
    {
        Task<bool> SendToAsync(string message, string subject, string to);
    }
}
