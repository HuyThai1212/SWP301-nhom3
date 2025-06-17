using Microsoft.Extensions.Configuration;
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
            var smtpHost = _config["SmtpSettings:Host"];
            var smtpPort = int.Parse(_config["SmtpSettings:Port"]);
            var smtpUser = _config["SmtpSettings:Email"];
            var smtpPass = _config["SmtpSettings:Password"];

            var mail = new MailMessage
            {
                From = new MailAddress(smtpUser),
                Subject = subject,
                Body = body
            };
            mail.To.Add(toEmail);

            var smtp = new SmtpClient(smtpHost)
            {
                Port = smtpPort,
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                EnableSsl = true,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            await smtp.SendMailAsync(mail);
        }
    }
}
