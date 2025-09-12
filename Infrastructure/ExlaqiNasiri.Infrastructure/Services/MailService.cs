using ExlaqiNasiri.Application.Abstraction;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace ExlaqiNasiri.Infrastructure.Services
{
    public class MailService : IMailService
    {
        readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMailAsync(string to, string subject, string body)
        {
            string from = _configuration["Mail:From"];


            MailMessage mail = new MailMessage();
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.From = new MailAddress(_configuration["Mail:From"]);
            mail.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = _configuration["Smtp:Host"];
            smtpClient.Port = Convert.ToInt32(_configuration["Smtp:Port"]);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(from, _configuration["Smtp:Password"]);
            smtpClient.EnableSsl = true;

            await smtpClient.SendMailAsync(mail);
        }
    }
}
