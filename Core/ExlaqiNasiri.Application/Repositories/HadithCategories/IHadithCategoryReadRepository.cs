using ExlaqiNasiri.Application.DTOs.HadithCategory;
using ExlaqiNasiri.Domain.Entities;

namespace ExlaqiNasiri.Application.Repositories.HadithCategories
{
    public interface IHadithCategoryReadRepository : IReadRepository<HadithCategory>
    {
        IQueryable<HadithCategory> GetFilteredAsync(HadithCategoryFilterDto dto);
    }
}
