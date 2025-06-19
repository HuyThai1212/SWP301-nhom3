using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {

            if (string.IsNullOrWhiteSpace(toEmail))
                throw new ArgumentException("Email cannot be empty");

            if (!new EmailAddressAttribute().IsValid(toEmail))
                throw new ArgumentException("Invalid email format");

            try
            {
                // Get config with fallback values
                var smtpHost = _config["SmtpSettings:Host"] ?? "smtp.gmail.com";
                var smtpPort = _config.GetValue<int>("SmtpSettings:Port", 587);
                var smtpUser = _config["SmtpSettings:Email"];
                var smtpPass = _config["SmtpSettings:Password"];

                using var mail = new MailMessage
                {
                    From = new MailAddress(smtpUser, "Hospital System"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false
                };
                mail.To.Add(toEmail);

                using var smtp = new SmtpClient(smtpHost, smtpPort)
                {
                    Credentials = new NetworkCredential(smtpUser, smtpPass),
                    EnableSsl = _config.GetValue<bool>("SmtpSettings:EnableSsl", true),
                    Timeout = _config.GetValue<int>("SmtpSettings:Timeout", 30000)
                };

                await smtp.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SMTP Error: {ex}");
                throw;
            }
        }
    }
}
