using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.Abstraction.Helper;
using ExlaqiNasiri.Application.DTOs.WebUser;
using ExlaqiNasiri.Application.Enums;
using ExlaqiNasiri.Application.ResultPattern;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Domain.Entities.BaseEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace ExlaqiNasiri.Persistence.Services
{
    public class WebUserService : IWebUserService
    {
        readonly UserManager<BaseUser> _userManager;
        readonly ISendMailService _mailService;
        readonly IOtpService _otpservice;
        readonly IGetEntityService _entityService;
        public WebUserService(UserManager<BaseUser> userManager, ISendMailService mailService, IOtpService otpservice, IGetEntityService entityService)
        {
            _userManager = userManager;
            _mailService = mailService;
            _otpservice = otpservice;
            _entityService = entityService;
        }

        public async Task<Result<List<WebUserGetDto>>> GetAllUserAsync(DateTime? lastCreadetAt, int size)
        {
            var query = _userManager.Users.OfType<WebUser>().AsQueryable();
            if (lastCreadetAt.HasValue)
            {
                query = query.Where(u => u.CreadetAt < lastCreadetAt.Value);
            }
            var webUsers = await query.OrderByDescending(u => u.CreadetAt)
                .Take(size)
                .Select(webUser => new WebUserGetDto
                {
                    Id = webUser.Id,
                    Email = webUser.Email,
                    FirstName = webUser.FirstName,
                    LastName = webUser.LastName,
                    MiddleName = webUser.MiddleName,
                    CreadetAt = webUser.CreadetAt
                }).ToListAsync();
            var result = new WebUserGetDto
            {
                CreadetAt = webUsers.LastOrDefault()?.CreadetAt
            };
            return Result<List<WebUserGetDto>>.Success(webUsers);
        }

        public async Task<Result<WebUserGetDto>> GetSingleUserAsync(string UserIdOrEmail)
        {
            var webUser = await _entityService.GetUserAsync(UserIdOrEmail);
            var dto = new WebUserGetDto
            {
                Id = webUser.Value.Id,
                FirstName = webUser.Value.FirstName,
                LastName = webUser.Value.LastName,
                MiddleName = webUser.Value.MiddleName,
                Email = webUser.Value.Email,
                CreadetAt = webUser.Value.CreadetAt
            };
            return Result<WebUserGetDto>.Success(dto);
        }

        public async Task<Result<bool>> RegisterAsync(WebUserRegisterDto registerDto)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var user = await _userManager.FindByEmailAsync(registerDto.Email);
                if (user != null) return Result<bool>.Failure(Error.AlreadyExistError("user"));

                var webUser = new WebUser
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    MiddleName = registerDto.MiddleName,
                    Email = registerDto.Email,
                    NormalizedEmail = registerDto.Email.ToUpper(),
                    UserName = registerDto.Email
                };
                if (registerDto.Password != registerDto.ConfirmPassword) return Result<bool>.Failure(Error.ConfirmPasswordError());

                var result = await _userManager.CreateAsync(webUser, registerDto.Password);
                if (!result.Succeeded) return Result<bool>.Failure(Error.OperationFailError("register user"));
                result = await _userManager.AddToRoleAsync(webUser, "User");
                if (!result.Succeeded) return Result<bool>.Failure(Error.OperationFailError("add role"));

                await _otpservice.SendOtpAsync(webUser.Email, OtpPurpose.EmailVerification);

                scope.Complete();
                return Result<bool>.Success(true);
            }
        }

        public async Task<Result<bool>> UpdateUserAsync(string UserIdOrEmail, WebUserPatchDto dto)
        {
            var webUser = await _entityService.GetUserAsync(UserIdOrEmail);
            webUser.Value.FirstName = dto.FirstName != null ? dto.FirstName : webUser.Value.FirstName;
            webUser.Value.LastName = dto.LastName != null ? dto.LastName : webUser.Value.LastName;
            webUser.Value.MiddleName = dto.MiddleName != null ? dto.MiddleName : webUser.Value.MiddleName;

            var result = await _userManager.UpdateAsync(webUser.Value);
            if (!result.Succeeded) return Result<bool>.Failure(Error.OperationFailError("update user"));
            return Result<bool>.Success(true);
        }
    }
}
