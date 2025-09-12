using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.Abstraction.Helper;

namespace ExlaqiNasiri.Persistence.Services.Helper
{
    public class SendMailService : ISendMailService
    {
        readonly IMailService _mailService;

        public SendMailService(IMailService mailService)
        {
            _mailService = mailService;
        }

        public async Task SendMailAsync(string email, string subject, string body)
        {
            await _mailService.SendMailAsync(email, subject, body);
        }
    }
}
