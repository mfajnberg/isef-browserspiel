namespace web_api.Services
{
    public interface INotification
    {
        void SendTo(string message, string subject, string to);
    }
}
