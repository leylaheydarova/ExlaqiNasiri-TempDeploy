using ExlaqiNasiri.Application.ResultPattern;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Domain.Entities.BaseEntities;

namespace ExlaqiNasiri.Application.Abstraction.Helper
{
    public interface IGetEntityService
    {
        Task<Result<HadithCategory>> GetHadithCategoryAsync(Guid hadithcategoryId, bool isTracking);
        Task<Result<LessonField>> GetLessonFieldAsync(Guid lessonFieldId, bool isTracking);
        Task<Result<BaseUser>> GetUserAsync(string UserNameOrId);
    }
}
