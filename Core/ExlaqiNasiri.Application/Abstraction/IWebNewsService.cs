using ExlaqiNasiri.Application.DTOs.WebNews;
using ExlaqiNasiri.Application.ResultPattern;

namespace ExlaqiNasiri.Application.Abstraction
{
    public interface IWebNewsService
    {
        Task<Result<bool>> CreateAsync(WebNewsCommandDto dto);
        Task<Result<bool>> DeleteAsync(Guid id);
        Task<Result<bool>> UpdateAsync(Guid id, WebNewsPatchDto dto);
        Task<Result<List<WebNewsGetAllDto>>> GetAllAsync(WebNewsFilterDto? dto);
        Task<Result<WebNewsGetSingleDto>> GetSingleAsync(Guid id);

    }
}
