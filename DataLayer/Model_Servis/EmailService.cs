using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DataLayer.Model_Servis
{
    public class EmailService:IEmailSender //internetten buldum bunuda mail atıyor işte
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var smtpClient = new SmtpClient(_configuration["Email:Host"])
            {
                Port = int.Parse(_configuration["Email:Port"]),
                Credentials = new NetworkCredential(_configuration["Email:Username"], _configuration["Email:Password"]),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["Email:From"]),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(email);


            try
            {
                await smtpClient.SendMailAsync(mailMessage);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email.");
            }
        }
    }
}
