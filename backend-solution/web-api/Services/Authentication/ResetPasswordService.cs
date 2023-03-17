using Microsoft.AspNetCore.Identity;
using System.Reflection;
using System;
using System.Text;
using web_api.Controllers;

namespace web_api.Services.Authentication
{
    public class ResetPasswordService
    {
        private string Url { get; set; }    
        public ResetPasswordService(string url)
        {

            Url = url;

        }

        public string GetForgotPasswordSubject()
        {
            return "Passwort zurücksetzen";
        }

        public string CreateForgotPasswortMessage(UserResetToken userResetToken)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Hallo Spieler,");
            sb.AppendLine();
            sb.AppendLine("wenn du dein Passwort zurücksetzen möchtest, verwende den nachfolgende Link:");
            sb.AppendLine(getResetPasswordLink(userResetToken));
            sb.AppendLine();
            sb.AppendLine("Hast du keine Passwortänderung angefordert, ignoriere diese Nachricht einfach.");
            sb.AppendLine();

            return sb.ToString();

        }

        private string? getResetPasswordLink(UserResetToken userResetToken)
        {
            var methods = typeof(AuthenticationController).GetMethods(BindingFlags.Public | BindingFlags.Instance);
            var confirmMethod = methods.Where(m => m.Name == "ResetPassword").FirstOrDefault();

            var attribute = confirmMethod.GetCustomAttributes(typeof(Microsoft.AspNetCore.Mvc.Routing.HttpMethodAttribute)).FirstOrDefault();
            string api = ((Microsoft.AspNetCore.Mvc.Routing.HttpMethodAttribute)attribute).Template.Replace("/{resetId}", "");

            return $"{Url}?action=resetPassword&token={userResetToken.Token}&user={userResetToken.Email}";
        }
    }
}
