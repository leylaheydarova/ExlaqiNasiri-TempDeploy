using ExlaqiNasiri.Application.DTOs.WebNews;
using ExlaqiNasiri.Application.Repositories.News;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Persistence.Repositories.News
{
    public class WebNewsReadRepository : ReadRepository<WebNews>, IWebNewsReadRepository
    {
        public WebNewsReadRepository(ExlaqiNasiriDbContext context) : base(context)
        {
        }

        public IQueryable<WebNews> GetFilteredAsync(WebNewsFilterDto dto)
        {
            var query = _context.WebNewses.AsQueryable();

            if (dto.CreatedFrom.HasValue) query = query.Where(c => c.CreatedAt >= dto.CreatedFrom);
            if (dto.CreatedTo.HasValue) query = query.Where(c => c.CreatedAt <= dto.CreatedTo);

            return query;
        }
    }
}
