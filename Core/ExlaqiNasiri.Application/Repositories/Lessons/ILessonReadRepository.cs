using ExlaqiNasiri.Application.DTOs.Lesson;
using ExlaqiNasiri.Domain.Entities;

namespace ExlaqiNasiri.Application.Repositories.Lessons
{
    public interface ILessonReadRepository : IReadRepository<Lesson>
    {
        IQueryable<Lesson> GetFilteredAsync(LessonFilterDto dto);
    }
}
