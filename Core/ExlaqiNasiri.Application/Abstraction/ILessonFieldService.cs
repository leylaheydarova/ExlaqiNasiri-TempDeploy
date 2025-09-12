using ExlaqiNasiri.Application.DTOs.LessonField;
using ExlaqiNasiri.Application.ResultPattern;

namespace ExlaqiNasiri.Application.Abstraction
{
    public interface ILessonFieldService
    {
        Task<Result<bool>> CreateAsync(LessonFieldCommandDto dto);
        Task<Result<bool>> UpdateAsync(Guid id, LessonFieldPatchDto dto);
        Task<Result<bool>> DeleteAsync(Guid id);
        Task<Result<bool>> ToggleAsync(Guid id);

        Task<Result<List<LessonFieldGetDto>>> GetAllAsync(LessonFieldFilterDto? dto);
        Task<Result<LessonFieldGetDto>> GetSingleAsync(Guid id);
    }
}
