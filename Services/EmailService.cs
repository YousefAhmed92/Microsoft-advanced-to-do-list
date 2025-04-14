using MyTasks01.Interfaces;
using System.Net.Mail;
using System.Net;

namespace MyTasks01.Services
{
    public class EmailService : IEmailInterface
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailService(string toEmail, string subject, string body)
        {
            var smtpClient = new SmtpClient(_configuration["EmailConfiguration:SmtpServer"])
            {
                Port = int.Parse(_configuration["EmailConfiguration:Port"] ?? "hello"),
                Credentials = new NetworkCredential(
               _configuration["EmailConfiguration:Username"],
               _configuration["EmailConfiguration:Password"]),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["EmailConfiguration:SenderEmail"]??"hello", _configuration["EmailConfiguration:SenderName"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(toEmail);
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
