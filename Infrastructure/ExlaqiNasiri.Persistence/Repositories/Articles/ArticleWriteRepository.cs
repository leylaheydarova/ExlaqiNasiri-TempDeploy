using ExlaqiNasiri.Application.Repositories;
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
    public class ArticleWriteRepository : WriteRepository<Article>, IArticleWriteRepository
    {
        public ArticleWriteRepository(ExlaqiNasiriDbContext context) : base(context)
        {
        }
    }
}
