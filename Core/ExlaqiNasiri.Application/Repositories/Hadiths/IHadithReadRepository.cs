using ExlaqiNasiri.Application.DTOs.Hadith;
using ExlaqiNasiri.Domain.Entities;

namespace ExlaqiNasiri.Application.Repositories.Hadiths
{
    public interface IHadithReadRepository : IReadRepository<Hadith>
    {
        IQueryable<Hadith> GetFilteredAsync(HadithFilterDto dto);
    }
}
