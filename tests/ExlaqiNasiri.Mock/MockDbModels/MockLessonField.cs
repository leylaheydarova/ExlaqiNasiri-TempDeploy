using ExlaqiNasiri.Domain.Entities;

namespace ExlaqiNasiri.Mock.MockDbModels
{
    public class MockLessonField
    {
        public static LessonField CreateMockEntity()
        {
            return new LessonField()
            {
                Id = Guid.NewGuid(),
                LessonName = "Test",
                IsDeleted = false
            };
        }

        public static IQueryable<LessonField> CreateMockQuery()
        {
            return new List<LessonField>
            {
                new LessonField
                {
                    Id = Guid.NewGuid(),
                    LessonName = "Test1"
                },
                new LessonField
                {
                    Id = Guid.NewGuid(),
                    LessonName = "Test2"
                }
            }.AsQueryable();
        }

        public static IQueryable<LessonField> CreateMockQueryWithFilterData()
        {
            return new List<LessonField>()
            {
                new LessonField
                {
                    Id = Guid.NewGuid(),
                    LessonName = "FilteredTest1",
                    CreatedAt = new DateTime(2024,1,1),
                    IsDeleted = false
                },
                 new LessonField
                {
                    Id = Guid.NewGuid(),
                    LessonName = "FilteredTest2",
                    CreatedAt = new DateTime(2023,5,18),
                    IsDeleted = false
                }
            }.AsQueryable();
        }

        public static LessonField CreateMockLessonField()
        {
            return new LessonField
            {
                Id = Guid.NewGuid(),
                LessonName = "Test",
                IsDeleted = true
            };

        }

        public static LessonField CreateMockLessonFieldEntity()
        {
            return new LessonField
            {
                Id = Guid.NewGuid(),
                LessonName = "Test",
                IsDeleted = false
            };
        }
    }
}
