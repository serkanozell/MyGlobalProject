using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using MyGlobalProject.Application.Dto.EmailDtos;
using MyGlobalProject.Application.ServiceInterfaces.Notification;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Serilog;
using System.Text;

namespace MyGlobalProject.Infrastructure.Notification
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(EmailDTO emailDTO)
        {
            var uri = "https://localhost:7152/api/";
            var client = new HttpClient() { BaseAddress = new Uri(uri) };
            var json = JsonConvert.SerializeObject(emailDTO);
            var content = new StringContent(json, encoding: Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("publisher", content);

            var response = await responseMessage.Content.ReadAsStringAsync();
        }
    }
}
