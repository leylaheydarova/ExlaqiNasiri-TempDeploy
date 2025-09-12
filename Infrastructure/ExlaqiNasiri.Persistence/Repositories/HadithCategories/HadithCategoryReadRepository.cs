using ExlaqiNasiri.Application.DTOs.HadithCategory;
using ExlaqiNasiri.Application.Repositories.HadithCategories;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Persistence.Context;

namespace ExlaqiNasiri.Persistence.Repositories.HadithCategories
{
    public class HadithCategoryReadRepository : ReadRepository<HadithCategory>, IHadithCategoryReadRepository
    {
        public HadithCategoryReadRepository(ExlaqiNasiriDbContext context) : base(context)
        {
        }

        public IQueryable<HadithCategory> GetFilteredAsync(HadithCategoryFilterDto dto)
        {
            var query = _context.HadithCategories.AsQueryable();

            if (dto.CreatedFrom.HasValue) query = query.Where(c => c.CreatedAt >= dto.CreatedFrom);
            if (dto.CreatedTo.HasValue) query = query.Where(c => c.CreatedAt <= dto.CreatedTo);
            if (dto.isDeleted.HasValue) query = query.Where(c => c.IsDeleted == (bool)dto.isDeleted);

            return query;
        }
    }
}
