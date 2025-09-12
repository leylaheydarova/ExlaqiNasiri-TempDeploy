using ExlaqiNasiri.Application.DTOs.Authentication;
using ExlaqiNasiri.Application.ResultPattern;

namespace ExlaqiNasiri.Application.Abstraction
{
    public interface IAuthenticationService
    {
        Task<Result<TokenDto>> LoginUserAsync(LoginDto dto);
        Task<Result<TokenDto>> UpdateAccessToken(string encryptedRefreshToken);
        Task<Result<bool>> LogoutAsync(string encryptedRefreshToken);
        Task<Result<bool>> ForgotPasswordAsync(string email);
        Task<Result<bool>> ResetPasswordAsync(ResetPasswordDto dto);

        Task<Result<bool>> ResendOtpCodeAsync(string userIdOrEmail);
    }
}
