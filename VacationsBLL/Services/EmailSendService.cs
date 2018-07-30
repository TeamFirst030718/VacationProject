using System.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SendGrid;
using SendGrid.Helpers.Mail;
using VacationsBLL.Interfaces;

namespace VacationsBLL.Services
{
    public class EmailSendService: IEmailSendService
    {
        private readonly string SendGridApiKeyName = "SendGridApiKey";

        public async Task SendAsync(string address, string name, string title, string plainTextContent, string message)
        {
            var apiKey = ConfigurationManager.AppSettings[SendGridApiKeyName];

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("test@example.com", "Softheme Vacations");
            var subject = title;
            var to = new EmailAddress(address, name);
            var htmlContent = "<strong>" + message + "</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
