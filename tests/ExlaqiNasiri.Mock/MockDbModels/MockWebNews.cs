using ExlaqiNasiri.Domain.Entities;

namespace ExlaqiNasiri.Mock.MockDbModels
{
    public static class MockWebNews
    {
        public static WebNews CreateMockEntity()
        {
            return new WebNews
            {
                Id = Guid.NewGuid(),
                Title = "Test Title",
                Text = "Test Text",
                ImageName = "mock.png",
                ImageUrl = "urlmock.png",
                CreatedAt = DateTime.UtcNow
            };
        }

        public static List<WebNews> CreateMockList()
        {
            return new List<WebNews>
            {
                new WebNews
                {
                    Id = Guid.NewGuid(),
                    Title = "Test1",
                    Text = "Test1",
                    ImageName = "mock1.png",
                    ImageUrl = "urlmock1.png",
                    CreatedAt = new DateTime(2024, 05, 01)
                },
                new WebNews
                {
                    Id = Guid.NewGuid(),
                    Title = "Test2",
                    Text = "Test2",
                    ImageName = "mock2.png",
                    ImageUrl = "urlmock2.png",
                    CreatedAt = new DateTime(2024, 07, 01)
                }
            };
        }

        public static List<WebNews> CreateMockListWithFilterData()
        {
            return new List<WebNews>
            {
                new WebNews
                {
                    Id = Guid.NewGuid(),
                    Title = "FilteredTitle1",
                    Text = "FilteredText2",
                    ImageName = "filtered1.png",
                    ImageUrl = "filteredUrl1.png",
                    CreatedAt = new DateTime(2024, 06, 15)
                },
                new WebNews
                {
                    Id = Guid.NewGuid(),
                    Title = "FilteredTitle2",
                    Text = "FilteredText2",
                    ImageName = "filtered2.png",
                    ImageUrl = "filteredUrl2.png",
                    CreatedAt = new DateTime(2024, 10, 01)
                }
            };
        }
    }
}
