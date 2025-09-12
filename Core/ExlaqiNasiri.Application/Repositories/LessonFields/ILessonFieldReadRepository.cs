using ExlaqiNasiri.Application.DTOs.LessonField;
using ExlaqiNasiri.Domain.Entities;

namespace ExlaqiNasiri.Application.Repositories.LessonFields
{
    public interface ILessonFieldReadRepository : IReadRepository<LessonField>
    {
        IQueryable<LessonField> GetFilteredAsync(LessonFieldFilterDto dto);
    }
}

