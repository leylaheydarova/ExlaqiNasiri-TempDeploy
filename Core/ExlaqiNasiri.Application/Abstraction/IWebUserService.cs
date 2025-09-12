using ExlaqiNasiri.Application.Abstraction.Base;
using ExlaqiNasiri.Application.DTOs.WebUser;

namespace ExlaqiNasiri.Application.Abstraction
{
    public interface IWebUserService : IUserDtoService<WebUserRegisterDto, WebUserPatchDto, WebUserGetDto, WebUserGetDto>
    {
    }
}
