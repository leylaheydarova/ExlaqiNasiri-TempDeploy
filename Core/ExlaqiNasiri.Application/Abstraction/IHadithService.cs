using ExlaqiNasiri.Application.DTOs.Hadith;
using ExlaqiNasiri.Application.ResultPattern;

namespace ExlaqiNasiri.Application.Abstraction
{
    public interface IHadithService
    {
        Task<Result<bool>> CreateAsync(HadithCreateDto dto);
        Task<Result<bool>> DeleteAsync(Guid id);
        Task<Result<bool>> UpdateAsync(Guid id, HadithPatchDto dto);
        Task<Result<List<HadithGetAllDto>>> GetAllAsync(HadithFilterDto? dto);
        Task<Result<HadithGetSingleDto>> GetSingleAsync(Guid id);

    }
}
