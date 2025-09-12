using ExlaqiNasiri.Domain.Entities;

namespace ExlaqiNasiri.Mock.MockDbModels
{
    public static class MockHadith
    {
        public static Hadith CreateMockEntity()
        {
            return new Hadith
            {
                Id = Guid.NewGuid(),
                HadithContext = "Test",
                Text = "Test",
                Source = "Test",
                ImageName = "mock.png",
                CategoryId = Guid.NewGuid()
            };
        }

        public static List<Hadith> CreateMockList()
        {
            return new List<Hadith>
            {
                new Hadith
                {
                    Id = Guid.NewGuid(),
                    HadithContext = "Test Hadith 1",
                    Source = "Source1",
                    ImageName = "image1.png",
                    ImageUrl = "url1.png",
                    CategoryId = Guid.NewGuid(),
                    CreatedAt = new DateTime(2024, 05, 01)
                },
                new Hadith
                {
                    Id = Guid.NewGuid(),
                    HadithContext = "Test Hadith 2",
                    Source = "Source2",
                    ImageName = "image2.png",
                    ImageUrl = "url2.png",
                    CategoryId = Guid.NewGuid(),
                    CreatedAt = new DateTime(2024, 07, 01)
                }
            };
        }

        public static List<Hadith> CreateMockListWithFilterData(Guid categoryId)
        {
            return new List<Hadith>
            {
                new Hadith
                {
                    Id = Guid.NewGuid(),
                    HadithContext = "Filtered Hadith",
                    Source = "FilterSource",
                    ImageName = "filtered.png",
                    ImageUrl = "filteredUrl.png",
                    CategoryId = categoryId,
                    CreatedAt = new DateTime(2024, 06, 15)
                },
                new Hadith
                {
                    Id = Guid.NewGuid(),
                    HadithContext = "Other Hadith",
                    Source = "FilterSource",
                    ImageName = "other.png",
                    ImageUrl = "otherUrl.png",
                    CategoryId = categoryId,
                    CreatedAt = new DateTime(2024, 10, 01)
                }
            };
        }
    }
}
