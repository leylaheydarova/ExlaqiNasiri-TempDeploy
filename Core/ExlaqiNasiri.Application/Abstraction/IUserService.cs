using ExlaqiNasiri.Application.DTOs.UserDto;
using ExlaqiNasiri.Application.Enums;
using ExlaqiNasiri.Application.ResultPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Application.Abstraction
{
    public interface IUserService
    {
        Task<Result<bool>> VerifyEmailAsync(string email, string otpCode, OtpPurpose purpose);
        Task<Result<bool>> ChangePasswordAsync(PasswordUpdateDto dto);
        Task<Result<bool>> RemoveAccountAsync(string UserIdOrEmail, UserType type);
    }
}
