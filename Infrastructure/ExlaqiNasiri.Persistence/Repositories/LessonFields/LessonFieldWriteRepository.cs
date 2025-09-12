using ExlaqiNasiri.Application.Repositories;
using ExlaqiNasiri.Application.Repositories.LessonFields;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Persistence.Context;

namespace ExlaqiNasiri.Persistence.Repositories.LessonFields
{
    public class LessonFieldWriteRepository : WriteRepositoryWithDelete<LessonField>, ILessonFieldWriteRepository, IWriteRepositoryWithDelete<LessonField>
    {
        public LessonFieldWriteRepository(ExlaqiNasiriDbContext context) : base(context)
        {
        }
    }
}
