using BackEngin.Services.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BackEngin.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _sendGridApiKey;
        private readonly string _fromEmail;
        private readonly string _fromName;

        public EmailService(IConfiguration configuration)
        {
            _sendGridApiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            _fromEmail = Environment.GetEnvironmentVariable("SENDGRID_FROM_EMAIL");
            _fromName = Environment.GetEnvironmentVariable("SENDGRID_FROM_NAME");
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var client = new SendGridClient(_sendGridApiKey);
            var from = new EmailAddress(_fromEmail, _fromName);
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);

            var response = await client.SendEmailAsync(msg);

            // Optionally, you can check the response status and log it
            // if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
            // {
            //     // Handle the error accordingly
            // }
        }
    }
}
