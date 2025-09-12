using ExlaqiNasiri.Application.Repositories.Hadiths;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Persistence.Context;

namespace ExlaqiNasiri.Persistence.Repositories.Hadiths
{
    public class HadithWriteRepository : WriteRepository<Hadith>, IHadithWriteRepository
    {
        public HadithWriteRepository(ExlaqiNasiriDbContext context) : base(context)
        {
        }
    }
}
