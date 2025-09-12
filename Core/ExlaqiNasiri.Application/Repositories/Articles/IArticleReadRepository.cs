using ExlaqiNasiri.Application.DTOs.Article;
using ExlaqiNasiri.Application.DTOs.Hadith;
using ExlaqiNasiri.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Application.Repositories.Articles
{
    public interface IArticleReadRepository : IReadRepository<Article>
    {
        IQueryable<Article> GetFilteredAsync(ArticleFilterDto dto);
    }
}
