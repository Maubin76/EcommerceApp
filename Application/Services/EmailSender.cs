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
            apiKey = config["Sendgrid:APIKey"];
        }

        public async Task SendEmail(string toEmail, string username, string subject, string htmlContent)
        {
            //var apiKey = _config["Sendgrid:APIKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("no-reply-metaarts@outlook.com", "Meta Arts");
            var to = new EmailAddress(toEmail, username);
            var plainTextContent = "This is a confirmation email.";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            //Console.WriteLine(response.StatusCode);
            //Console.WriteLine(await response.Body.ReadAsStringAsync());
        }
    }
}
