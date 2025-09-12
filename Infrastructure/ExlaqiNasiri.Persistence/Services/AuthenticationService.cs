using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.Abstraction.Helper;
using ExlaqiNasiri.Application.DTOs.Authentication;
using ExlaqiNasiri.Application.Enums;
using ExlaqiNasiri.Application.ResultPattern;
using ExlaqiNasiri.Domain.Entities.BaseEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ExlaqiNasiri.Persistence.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        readonly UserManager<BaseUser> _userManager;
        readonly SignInManager<BaseUser> _signInManager;
        readonly IGenerateJWTTokenService _jwtService;
        readonly IEncryptionService _encryptionService;
        readonly IOtpService _otpService;
        readonly IGetEntityService _entityService;
        readonly IMemoryCache _cache;
        public AuthenticationService(UserManager<BaseUser> userManager, SignInManager<BaseUser> signInManager, IGenerateJWTTokenService jwtService, IEncryptionService encryptionService, IOtpService otpService, IGetEntityService entityService, IMemoryCache cache)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _encryptionService = encryptionService;
            _otpService = otpService;
            _entityService = entityService;
            _cache = cache;
        }

        async Task<Result<bool>> UpdateUserRefreshToken(BaseUser user, string refreshToken)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpireDate = DateTime.UtcNow.AddHours(4).AddDays(7);

            var identityResult = await _userManager.UpdateAsync(user);
            if (!identityResult.Succeeded) return Result<bool>.Failure(Error.OperationFailError("update user"));
            return Result<bool>.Success(true);
        }

        public async Task<Result<TokenDto>> LoginUserAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) return Result<TokenDto>.Failure(Error.NotFoundError("user"));

            if (!user.EmailConfirmed) return Result<TokenDto>.Failure(Error.EmailConfirmError());

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, true);
            if (!signInResult.Succeeded) return Result<TokenDto>.Failure(Error.InvalidInputError("email or password"));

            var token = _jwtService.GenerateJwtToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();

            await UpdateUserRefreshToken(user, refreshToken.Value);

            return Result<TokenDto>.Success(new TokenDto
            {
                ExpireDate = DateTime.UtcNow.AddHours(4).AddHours(1),
                RefreshToken = _encryptionService.EncryptText(refreshToken.Value),
                AccessToken = token.Value
            });
        }

        public async Task<Result<TokenDto>> UpdateAccessToken(string encryptedRefreshToken)
        {
            var refreshToken = _encryptionService.DecryptText(encryptedRefreshToken);
            var azerbaijaniTime = DateTime.UtcNow.AddHours(4);

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken && u.RefreshTokenExpireDate > azerbaijaniTime);

            if (user == null) return Result<TokenDto>.Failure(Error.UnauthorizedError());
            var accessToken = _jwtService.GenerateJwtToken(user);

            var newRefreshToken = _jwtService.GenerateRefreshToken();
            await UpdateUserRefreshToken(user, newRefreshToken.Value);
            return Result<TokenDto>.Success(new TokenDto
            {
                AccessToken = accessToken.Value,
                ExpireDate = DateTime.UtcNow.AddHours(4).AddHours(1),
                RefreshToken = _encryptionService.EncryptText(newRefreshToken.Value)
            });
        }

        public async Task<Result<bool>> LogoutAsync(string encryptedRefreshToken)
        {
            var refreshToken = _encryptionService.DecryptText(encryptedRefreshToken);

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user == null) return Result<bool>.Failure(Error.UnauthorizedError());
            user.RefreshToken = null;
            user.RefreshTokenExpireDate = DateTime.MinValue;
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded) return Result<bool>.Failure(Error.OperationFailError("update user"));
            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> ForgotPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return Result<bool>.Failure(Error.NotFoundError("user"));

            await _otpService.SendOtpAsync(user.Email, OtpPurpose.PasswordReset);
            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> ResetPasswordAsync(ResetPasswordDto dto)
        {
            var result = _otpService.VerifyOtpAsync(dto.Email, dto.OtpCode, OtpPurpose.PasswordReset);
            if (!result.IsSuccess)
                return result;
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user is null) return Result<bool>.Failure(Error.NotFoundError("user"));

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetResult = await _userManager.ResetPasswordAsync(user, token, dto.NewPassword);

            if (!resetResult.Succeeded)
            {
                var errors = resetResult.Errors.Select(e => e.Description);
                return Result<bool>.Failure(Error.InvalidInputError(string.Join(", ", errors)));
            }
            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> ResendOtpCodeAsync(string userIdOrEmail)
        {
            var user = await _entityService.GetUserAsync(userIdOrEmail);
            string cacheKey = $"VerificationCode_{userIdOrEmail}";

            if (_cache.TryGetValue<(int code, DateTime ExpiryDate)>(cacheKey, out var cacheEntry))
            {
                if (cacheEntry.ExpiryDate > DateTime.UtcNow.AddHours(4)) return Result<bool>.Failure(Error.InvalidInputError("verification code"));
            }
            string newCode = new Random().Next(100000, 999999).ToString();

            var expiryTime = DateTime.UtcNow.AddHours(4).AddMinutes(3);//burda yazilan kod ile tezden gondereceyimiz otp-ye 3deq vaxt veririk.
            _cache.Set(cacheKey, (newCode, expiryTime), expiryTime);

            await _otpService.SendOtpAsync(userIdOrEmail, OtpPurpose.EmailVerification);
            return Result<bool>.Success(true);
        }
    }
}
