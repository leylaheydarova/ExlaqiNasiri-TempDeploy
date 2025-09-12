using ExlaqiNasiri.Application.DTOs.Lesson;
using ExlaqiNasiri.Application.Repositories.Lessons;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Persistence.Context;

namespace ExlaqiNasiri.Persistence.Repositories.Lessons
{
    public class LessonReadRepository : ReadRepository<Lesson>, ILessonReadRepository
    {
        public LessonReadRepository(ExlaqiNasiriDbContext context) : base(context)
        {
        }

        public IQueryable<Lesson> GetFilteredAsync(LessonFilterDto dto)
        {
            var query = _context.Lessons.AsQueryable();

            if (dto.CreatedFrom.HasValue) query = query.Where(c => c.CreatedAt >= dto.CreatedFrom);
            if (dto.CreatedTo.HasValue) query = query.Where(c => c.CreatedAt <= dto.CreatedTo);
            if (dto.LessonFieldId.HasValue) query = query.Where(c => c.LessonFieldId == dto.LessonFieldId);
            return query;
        }
    }
}
