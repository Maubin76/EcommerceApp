using SendGrid.Helpers.Mail;
using SendGrid;
using Microsoft.Extensions.Options;

namespace Application.Services
{
    public class EmailSender
    {
        private readonly string apiKey;

        public EmailSender(IConfiguration config)
        {
            // Retrieve Sendgrid's API key from appsettings.json
            apiKey = config["Sendgrid:APIKey"] ?? throw new InvalidOperationException("API key not found.");
        }

        public async Task SendEmail(string toEmail, string username, string subject, string htmlContent)
        {
            var client = new SendGridClient(apiKey); // connection to the API
            var from = new EmailAddress("no-reply-metaarts@outlook.com", "Meta Arts");
            var to = new EmailAddress(toEmail, username); // receiver
            var plainTextContent = "This is a confirmation email.";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent); // email creation
            var response = await client.SendEmailAsync(msg); // send email
        }
    }
}
