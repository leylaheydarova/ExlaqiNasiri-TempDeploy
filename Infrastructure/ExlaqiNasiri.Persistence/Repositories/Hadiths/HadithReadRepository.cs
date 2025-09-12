using ExlaqiNasiri.Application.DTOs.Hadith;
using ExlaqiNasiri.Application.Repositories.Hadiths;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ExlaqiNasiri.Persistence.Repositories.Hadiths
{
    public class HadithReadRepository : ReadRepository<Hadith>, IHadithReadRepository
    {
        public HadithReadRepository(ExlaqiNasiriDbContext context) : base(context)
        {
        }

        public IQueryable<Hadith> GetFilteredAsync(HadithFilterDto dto)
        {
            var query = _context.Hadiths.AsQueryable().AsNoTracking();

            if (dto.CreatedFrom.HasValue) query = query.Where(c => c.CreatedAt >= dto.CreatedFrom);
            if (dto.CreatedTo.HasValue) query = query.Where(c => c.CreatedAt <= dto.CreatedTo);
            if (dto.CategoryId != null) query = query.Where(c => c.CategoryId == dto.CategoryId);
            if (!string.IsNullOrWhiteSpace(dto.Source)) query = query.Where(c => c.Source == dto.Source);
            return query;
        }
    }
}
