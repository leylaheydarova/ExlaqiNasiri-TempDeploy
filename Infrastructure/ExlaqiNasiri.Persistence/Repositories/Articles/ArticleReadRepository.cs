using ExlaqiNasiri.Application.DTOs.Article;
using ExlaqiNasiri.Application.DTOs.Hadith;
using ExlaqiNasiri.Application.Repositories.Articles;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Persistence.Repositories.Articles
{
    public class ArticleReadRepository : ReadRepository<Article>, IArticleReadRepository
    {
        public ArticleReadRepository(ExlaqiNasiriDbContext context) : base(context)
        {
        }
        public IQueryable<Article> GetFilteredAsync(ArticleFilterDto dto)
        {
            var query = _context.Articles.AsQueryable();

            if (dto.CreatedFrom.HasValue) query = query.Where(c => c.CreatedAt >= dto.CreatedFrom);
            if (dto.CreatedTo.HasValue) query = query.Where(c => c.CreatedAt <= dto.CreatedTo);

            return query;
        }
    }
}
