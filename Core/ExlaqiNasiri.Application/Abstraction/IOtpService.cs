using ExlaqiNasiri.Application.Enums;
using ExlaqiNasiri.Application.ResultPattern;

namespace ExlaqiNasiri.Application.Abstraction
{
    public interface IOtpService
    {
        Task<Result<bool>> SendOtpAsync(string email, OtpPurpose purpose);
        Result<bool> VerifyOtpAsync(string email, string code, OtpPurpose purpose);
    }
}
