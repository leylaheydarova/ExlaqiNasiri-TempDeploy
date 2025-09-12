using ExlaqiNasiri.Domain.Entities;

namespace ExlaqiNasiri.Application.Repositories.LessonFields
{
    public interface ILessonFieldWriteRepository : IWriteRepository<LessonField>, IWriteRepositoryWithDelete<LessonField>
    {
    }
}
