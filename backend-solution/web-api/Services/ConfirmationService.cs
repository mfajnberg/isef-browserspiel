using System.Reflection;
using System.Text;
using web_api.Controllers;

namespace web_api.Services
{
    public class ConfirmationService
    {
        public string Url { get; set; }

        public string CreateNotificationMessage(UserConfirmation userConfirmation)
        {
            StringBuilder @string = new StringBuilder();
            @string.AppendLine("Hallo Spieler,");
            @string.AppendLine();
            @string.AppendLine("du hast dich soeben erfolgreich auf isef.ranger8.de registiert.");
            @string.AppendLine("Bitte verwende den Link, um deinen Spieler zu aktivieren.");
            @string.AppendLine();
            @string.AppendLine(GetConfimationLink(userConfirmation));

            return @string.ToString();
        }

        private string? GetConfimationLink(UserConfirmation userConfirmation)
        {
            var methods = typeof(ConfirmationController).GetMethods(BindingFlags.Public|BindingFlags.Instance);
            var confirmMethod = methods.Where(m => m.Name == "Confirm").FirstOrDefault();

            var attribute = confirmMethod.GetCustomAttributes(typeof(Microsoft.AspNetCore.Mvc.Routing.HttpMethodAttribute)).FirstOrDefault();
            string api = ((Microsoft.AspNetCore.Mvc.Routing.HttpMethodAttribute)attribute).Template.Replace("/{confirmationId}", "");

            return $"{Url}/{api}/{userConfirmation.ConfirmationId}";

        }
    }
}
