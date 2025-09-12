using ExlaqiNasiri.Domain.Entities;

namespace ExlaqiNasiri.Application.Repositories.HadithCategories
{
    public interface IHadithCategoryWriteRepository : IWriteRepository<HadithCategory>, IWriteRepositoryWithDelete<HadithCategory>
    {
    }
}
