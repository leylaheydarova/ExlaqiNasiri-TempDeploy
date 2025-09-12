using ExlaqiNasiri.Domain.Entities;

namespace ExlaqiNasiri.Mock.MockDbModels
{
    public static class MockHadithCategory
    {
        public static HadithCategory CreateMockEntity()
        {
            return new HadithCategory()
            {
                Id = Guid.NewGuid(),
                CategoryName = "Test Category",
                IsDeleted = false
            };
        }

        public static HadithCategory CreateMockEntityWithTrueIsDeleted()
        {
            return new HadithCategory()
            {
                Id = Guid.NewGuid(),
                CategoryName = "Deleted Category",
                IsDeleted = true
            };
        }

        public static IQueryable<HadithCategory> CreateMockQuery()
        {
            return new List<HadithCategory>
            {
                new HadithCategory
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "Test1"
                },
                new HadithCategory
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "Test2"
                }
            }.AsQueryable();
        }

        public static List<HadithCategory> CreateMockQueryWithFilterData()
        {
            return new List<HadithCategory>()
        {
            new HadithCategory
            {
                Id = Guid.NewGuid(),
                CategoryName = "Filtered Category",
                CreatedAt = new DateTime(2023, 6, 15),
                IsDeleted = false
            },
            new HadithCategory
            {
                Id = Guid.NewGuid(),
                CategoryName = "Other Category",
                CreatedAt = new DateTime(2022, 10, 10),
                IsDeleted = true
            }
        };
        }


    }
}
