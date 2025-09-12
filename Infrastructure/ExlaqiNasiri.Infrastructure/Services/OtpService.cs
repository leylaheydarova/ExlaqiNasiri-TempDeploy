using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.Abstraction.Helper;
using ExlaqiNasiri.Application.Enums;
using ExlaqiNasiri.Application.ResultPattern;
using ExlaqiNasiri.Application.Utils;
using Microsoft.Extensions.Caching.Memory;

namespace ExlaqiNasiri.Infrastructure.Services
{
    public class OtpService : IOtpService
    {
        readonly IMemoryCache _cache;
        readonly ISendMailService _sendMailService;

        public OtpService(IMemoryCache cache, ISendMailService sendMailService)
        {
            _cache = cache;
            _sendMailService = sendMailService;
        }

        private string GetCacheKey(string email, OtpPurpose purpose)
       => $"otp:{purpose}:{email.ToLower()}";

        public async Task<Result<bool>> SendOtpAsync(string email, OtpPurpose purpose)
        {
            var otp = GenerateSequence.GenerateOtpCode();
            var key = GetCacheKey(email, purpose);

            _cache.Set(key, otp, TimeSpan.FromMinutes(5));//bu kod emaile geden kodun 5deqiqelik available oldugunu gorsedir

            var subject = purpose switch
            {
                OtpPurpose.EmailVerification => "Əxlaqi-Nasiri platformasına xoş gəlmisiniz! Qeydiyyatınızı tamamlamaq üçün kodunuz burada",
                OtpPurpose.PasswordReset => "Əxlaqi-Nasiri şifrənizi yeniləyin",
                OtpPurpose.ChangeEmail => "Email hesabınızı yeniləyin"
            };
            var body = purpose switch
            {
                OtpPurpose.EmailVerification => $"<h3>Salam!\r\nSizi Əxlaqi-Nasiri platformasında görməkdən məmnunuq. \U0001F54C✨\r\nQeydiyyatınızı tamamlamaq üçün aşağıdakı təsdiq kodunu tətbiqdə müvafiq yerə daxil edin:\r\n</h3><h1>{otp}</h1><p>Bu kod 3 dəqiqə ərzində keçərlidir.</p>",
                OtpPurpose.PasswordReset => $"<h3>Salam!\r\n Əxlaqi-Nasiri şifrənizi yeniləmək üçün aşağıdakı təsdiq kodunu tətbiqdə müvafiq yerə daxil edin:\r\n</h3><h1>{otp}</h1><p>Bu kod 3 dəqiqə ərzində keçərlidir.</p>",
                OtpPurpose.ChangeEmail => $"<h3>Salam!\r\nEmail hesabınızı yeniləmək üçün aşağıdakı təsdiq kodunu tətbiqdə müvafiq yerə daxil edin:\r\n</h3><h1>{otp}</h1><p>Bu kod 3 dəqiqə ərzində keçərlidir.</p>"

            };

            await _sendMailService.SendMailAsync(email, subject, body);
            return Result<bool>.Success(true);
        }

        public Result<bool> VerifyOtpAsync(string email, string code, OtpPurpose purpose)
        {
            var key = GetCacheKey(email, purpose);
            if (_cache.TryGetValue(key, out string cachedOtp))
            {
                if (cachedOtp == code)
                {
                    _cache.Remove(key);
                    return Result<bool>.Success(true);
                }
            }

            return Result<bool>.Failure(Error.InvalidInputError("OTP code"));
        }
    }
}
