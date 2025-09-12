using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.Abstraction.Helper;
using ExlaqiNasiri.Application.DTOs.UserDto;
using ExlaqiNasiri.Application.Enums;
using ExlaqiNasiri.Application.ResultPattern;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Domain.Entities.BaseEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace ExlaqiNasiri.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<BaseUser> _userManager;
        private readonly IOtpService _otpService;
        private readonly ILogger<UserService> _logger;
        private readonly IGetEntityService _entityService;

        public UserService(UserManager<BaseUser> userManager, IOtpService otpService, ILogger<UserService> logger, IGetEntityService entityService)
        {
            _userManager = userManager;
            _otpService = otpService;
            _logger = logger;
            _entityService = entityService;
        }
        Result<bool> CheckUserType(BaseUser user, UserType type)
        {
            if (user is WebUser && type != UserType.User) return Result<bool>.Failure(Error.UserTypeError("user"));
            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> VerifyEmailAsync(string mail, string otpcode, OtpPurpose purpose)
        {
            // 1. E-poçtun boş və ya null olmadığını yoxla
            if (string.IsNullOrWhiteSpace(mail))
            {
                _logger.LogWarning("Email is null or empty.");
                return Result<bool>.Failure(Error.InvalidInputError("email"));
            }

            // 2. OTP kodunun boş və ya null olmadığını yoxla
            if (string.IsNullOrWhiteSpace(otpcode))
            {
                _logger.LogWarning("OTP code is null or empty.");
                return Result<bool>.Failure(Error.InvalidInputError("OTP code"));
            }

            // 3. İstifadəçini e-poçt ilə tap
            var user = await _userManager.FindByEmailAsync(mail);
            if (user == null)
            {
                _logger.LogWarning($"User with email {mail} not found.");
                return Result<bool>.Failure(Error.NotFoundError("user"));
            }

            // 4. E-poçtun artıq təsdiqlənib-təsdiqlənmədiyini yoxla
            if (user.EmailConfirmed)
            {
                _logger.LogInformation($"Email {mail} is already verified.");
                return Result<bool>.Success(true);
            }

            // 5. OTP kodunu yoxla
            var otpResult = _otpService.VerifyOtpAsync(mail, otpcode, purpose);
            if (!otpResult.IsSuccess)
            {
                _logger.LogWarning($"Invalid OTP code for email {mail} with purpose {purpose}.");
                return Result<bool>.Failure(otpResult.Error);
            }

            // 6. E-poçtu təsdiqlə
            user.EmailConfirmed = true;
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                _logger.LogError($"Failed to verify email for {mail}. Errors: {string.Join(", ", updateResult.Errors.Select(e => e.Description))}");
                return Result<bool>.Failure(Error.UnexpectedError($"Failed to verify email for {mail}."));
            }

            _logger.LogInformation($"Email {mail} verified successfully for purpose {purpose}.");
            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> RemoveAccountAsync(string UserIdOrEmail, UserType type)
        {
            var user = await _entityService.GetUserAsync(UserIdOrEmail);
            if (user.IsFailure) return Result<bool>.Failure(user.Error);
            var check = CheckUserType(user.Value, type);
            if (check.IsFailure) return Result<bool>.Failure(check.Error);
            var roles = await _userManager.GetRolesAsync(user.Value);
            var result = await _userManager.RemoveFromRolesAsync(user.Value, roles);
            if (!result.Succeeded) return Result<bool>.Failure(Error.OperationFailError("remove roles"));
            result = await _userManager.DeleteAsync(user.Value);
            if (!result.Succeeded) return Result<bool>.Failure(Error.OperationFailError("remove user"));
            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> ChangePasswordAsync(PasswordUpdateDto dto)
        {
            if (dto.NewPassword != dto.ConfirmNewPassword) return Result<bool>.Failure(Error.PasswordConfirmError());
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) return Result<bool>.Failure(Error.NotFoundError("user"));
            if (!user.EmailConfirmed) return Result<bool>.Failure(Error.EmailConfirmError());
            var result = await _userManager.CheckPasswordAsync(user, dto.CurrentPassword);
            if (!result) return Result<bool>.Failure(Error.InvalidInputError("password"));
            var change = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
            if (!change.Succeeded) return Result<bool>.Failure(Error.FailError("change password"));
            return Result<bool>.Success(true);
        }
    }
}