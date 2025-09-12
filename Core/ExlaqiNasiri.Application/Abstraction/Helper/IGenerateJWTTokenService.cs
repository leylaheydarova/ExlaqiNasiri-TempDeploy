using ExlaqiNasiri.Application.ResultPattern;
using ExlaqiNasiri.Domain.Entities.BaseEntities;

namespace ExlaqiNasiri.Application.Abstraction.Helper
{
    public interface IGenerateJWTTokenService
    {
        Result<string> GenerateJwtToken(BaseUser user);
        Result<string> GenerateRefreshToken();
    }
}
