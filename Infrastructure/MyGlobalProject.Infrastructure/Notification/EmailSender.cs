using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using MyGlobalProject.Application.Dto.EmailDtos;
using MyGlobalProject.Application.ServiceInterfaces.Notification;
using Serilog;

namespace MyGlobalProject.Infrastructure.Notification
{
    public class EmailSender : IEmailSender
    {
        IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task SendEmailAsync(EmailDTO emailDTO)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("Email:FromMail").Value!));
            email.To.Add(MailboxAddress.Parse(emailDTO.To));
            email.Subject = emailDTO.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = emailDTO.Body };

            using var smtpClient = new SmtpClient();
            smtpClient.Connect(_configuration.GetSection("Email:SmtpHost").Value, 587, SecureSocketOptions.StartTls);
            smtpClient.Authenticate(_configuration.GetSection("Email:FromMail").Value, _configuration.GetSection("Email:Password").Value);

            smtpClient.Send(email);
            smtpClient.Disconnect(true);

            Log.Information($"Message sended to: {emailDTO.To} - From: {email.From} at {DateTime.Now}");

            return Task.CompletedTask;
        }
    }
}
