using ExlaqiNasiri.Application.DTOs.HadithCategory;
using ExlaqiNasiri.Application.ResultPattern;

namespace ExlaqiNasiri.Application.Abstraction
{
    public interface IHadithCategoryService
    {
        Task<Result<bool>> CreateAsync(HadithCategoryCommandDTO dto);
        Task<Result<bool>> UpdateAsync(Guid id, HadithCategoryCommandDTO dto);
        Task<Result<bool>> DeleteAsync(Guid id);
        Task<Result<bool>> ToggleAsync(Guid id);

        Task<Result<List<HadithCategoryGetDto>>> GetAllAsync(HadithCategoryFilterDto? dto);
        Task<Result<HadithCategoryGetDto>> GetSingleAsync(Guid id);
    }
}
