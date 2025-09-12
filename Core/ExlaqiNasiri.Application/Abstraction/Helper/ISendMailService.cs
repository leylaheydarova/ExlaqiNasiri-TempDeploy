namespace ExlaqiNasiri.Application.Abstraction.Helper
{
    public interface ISendMailService
    {
        //Task SendMailAsync(BaseUser user);
        Task SendMailAsync(string email, string subject, string body);
    }
}
