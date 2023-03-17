using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using web_api.Controllers;

namespace web_api.Services.Authentication
{
    /// <summary>
    /// Email Notification
    /// </summary>
    public class MailNotification : INotification
    {
        readonly string mailServer;
        readonly int mailServerPort;
        readonly string mailSender;
        readonly string mailSenderPassword;

        /// <summary>
        /// Constructor for MailNotification
        /// </summary>
        /// <param name="config">application settings</param>
        public MailNotification(IConfiguration config)
        {
            mailServer = config.GetSection("Mail:Server").Value;
            if (!int.TryParse(config.GetSection("Mail:Port").Value, out mailServerPort))
                mailServerPort = 587;
            mailSender = config.GetSection("Mail:Sender").Value;
            mailSenderPassword = config.GetSection("Mail:Password").Value;
        }

        /// <summary>
        /// sends a email to the user 
        /// </summary>
        /// <param name="message">email body</param>
        /// <param name="subject">email subject</param>
        /// <param name="to">email recipient</param>
        /// <returns></returns>
        public async Task<bool> SendToAsync(string message, string subject, string to)
        {
            bool result = false;
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

                MailMessage mailMessage = new MailMessage();
                //mailMessage.IsBodyHtml = true;
                mailMessage.From = new MailAddress(mailSender, "ISEF Browserspiel");

                mailMessage.Subject = subject;
                mailMessage.Body = message;
                mailMessage.To.Add(to);

                try
                {
                    ConsoleLogger.LogInfo($"trying to Send Mail to {to} ...");

                    await client.SendMailAsync(mailMessage);
                    result = true;

                    ConsoleLogger.LogInfo("Mail successfully sent...");
                }
                catch (Exception ex)
                {
                    ConsoleLogger.LogError("Error while sending mail ");
                    ConsoleLogger.LogError(ex.Message);
                    result = false;
                }

            }
            return result;
        }

        /// <summary>
        /// checks weather the email is configured
        /// </summary>
        /// <returns><b>true</b> if, and <b>false</b> if not</returns>
        private bool isConfigured()
        {
            bool result = true;

            if (string.IsNullOrEmpty(mailSender))
                result = false;

            if (string.IsNullOrEmpty(mailSenderPassword))
                result = false;

            if (string.IsNullOrEmpty(mailServer))
                result = false;

            return result;
        }

        /// <summary>
        /// checks, if the mail server is pingable
        /// </summary>
        /// <param name="mailServer"></param>
        /// <returns></returns>
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
