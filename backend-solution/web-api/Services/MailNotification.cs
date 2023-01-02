using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;

namespace web_api.Services
{
    public class MailNotification : INotification
    {
        readonly string mailServer;
        readonly int mailServerPort;
        readonly string mailSender;
        readonly string mailSenderPassword;
        public MailNotification(IConfiguration config)
        {
            mailServer = config.GetSection("Mail:Server").Value;
            if (!int.TryParse(config.GetSection("Mail:Port").Value, out mailServerPort))
                mailServerPort = 587;
            mailSender = config.GetSection("Mail:Sender").Value;
            mailSenderPassword = config.GetSection("Mail:Password").Value;
        }


        public void SendTo(string message, string subject, string to)
        {
           if (isConfigured())
            {
                SmtpClient client = new SmtpClient();
                client.Host = mailServer;
                client.Port = mailServerPort;
                client.EnableSsl = mailServerPort == 587;
                client.DeliveryFormat = SmtpDeliveryFormat.SevenBit;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(mailSender, mailSenderPassword);

                MailMessage mailMessage= new MailMessage();
                mailMessage.From = new MailAddress(mailSender, "ISEF Browserspiel");

                mailMessage.Subject = subject;
                mailMessage.Body = message;
                mailMessage.To.Add(to);

                client.Send(mailMessage);

            }
        }

        private bool isConfigured()
        {
            bool result = true;

            if (string.IsNullOrEmpty(mailSender))
                result = false;

            if (string.IsNullOrEmpty(mailSenderPassword))
                result = false;

            if (string.IsNullOrEmpty(mailServer))
                result = false;

            if (mailServer != null)
                if (!isPingable(mailServer))
                    result = false;
   
            return result;
        }

        private bool isPingable(string mailServer)
        {
            Ping ping = new Ping();
            PingReply reply = ping.Send(mailServer, 1000);

            if (reply != null)
                if (reply.Status == IPStatus.Success)
                    return true;

            return false;
        }
    }
}
