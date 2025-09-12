using ExlaqiNasiri.Application.Repositories;
using ExlaqiNasiri.Application.Repositories.HadithCategories;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Persistence.Context;

namespace ExlaqiNasiri.Persistence.Repositories.HadithCategories
{
    public class HadithCategoryWriteRepository : WriteRepositoryWithDelete<HadithCategory>, IHadithCategoryWriteRepository, IWriteRepositoryWithDelete<HadithCategory>
    {
        public HadithCategoryWriteRepository(ExlaqiNasiriDbContext context) : base(context)
        {
        }
    }
}
