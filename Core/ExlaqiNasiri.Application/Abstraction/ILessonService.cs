using ExlaqiNasiri.Application.DTOs.Lesson;
using ExlaqiNasiri.Application.ResultPattern;

namespace ExlaqiNasiri.Application.Abstraction
{
    public interface ILessonService
    {
        Task<Result<bool>> CreateAsync(LessonCreateDto dto);
        Task<Result<bool>> DeleteAsync(Guid id);
        Task<Result<bool>> UpdateAsync(Guid id, LessonUpdateDto dto);
        Task<Result<List<LessonGetAllDto>>> GetAllAsync(LessonFilterDto? dto);
        Task<Result<LessonGetSingleDto>> GetSingleAsync(Guid id);
    }
}
