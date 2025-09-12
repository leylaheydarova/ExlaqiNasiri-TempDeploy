using ExlaqiNasiri.Application.DTOs.LessonField;
using ExlaqiNasiri.Application.Repositories.LessonFields;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Persistence.Context;

namespace ExlaqiNasiri.Persistence.Repositories.LessonFields
{
    public class LessonFieldReadRepository : ReadRepository<LessonField>, ILessonFieldReadRepository
    {
        public LessonFieldReadRepository(ExlaqiNasiriDbContext context) : base(context)
        {
        }

        public IQueryable<LessonField> GetFilteredAsync(LessonFieldFilterDto dto)
        {
            var query = _context.LessonFields.AsQueryable();

            if (dto.CreatedFrom.HasValue) query = query.Where(c => c.CreatedAt >= dto.CreatedFrom);
            if (dto.CreatedTo.HasValue) query = query.Where(c => c.CreatedAt <= dto.CreatedTo);
            if (dto.isDeleted.HasValue) query = query.Where(c => c.IsDeleted == (bool)dto.isDeleted);

            return query;
        }
    }
}
