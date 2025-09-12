using ExlaqiNasiri.Application.Repositories.Lessons;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Persistence.Context;

namespace ExlaqiNasiri.Persistence.Repositories.Lessons
{
    public class LessonWriteRepository : WriteRepository<Lesson>, ILessonWriteRepository
    {
        public LessonWriteRepository(ExlaqiNasiriDbContext context) : base(context)
        {
        }
    }
}
