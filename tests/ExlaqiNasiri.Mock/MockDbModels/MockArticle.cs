using ExlaqiNasiri.Domain.Entities;

namespace ExlaqiNasiri.Mock.MockDbModels
{
    public class MockArticle
    {
        public static Article CreateMockEntity()
        {
            return new Article
            {
                Title = "Test",
                Text = "Test",
                Author = "Test",
                ImageName = "mock.png",
            };
        }

        public static List<Article> CreateMockList()
        {
            return new List<Article>
            {
                new Article
                {
                    Id = Guid.NewGuid(),
                    Title = "Test Title 1",
                    Text = "Test Text 1",
                    Author = "Test Author 1",
                    ImageName = "image1.png",
                    ImageUrl = "url1.png",
                    CreatedAt = new DateTime(2024, 05, 01)
                },
                new Article
                {
                    Id = Guid.NewGuid(),
                    Title = "Test Title 1",
                    Text = "Test Text 2",
                    Author = "Test Author 2",
                    ImageName = "image1.png",
                    ImageUrl = "url1.png",
                    CreatedAt = new DateTime(2024, 07, 01)
                }
            };
        }

        public static List<Article> CreateMockListWithFilterData(Guid categoryId)
        {
            return new List<Article>
            {
                new Article
                {
                    Id = Guid.NewGuid(),
                    Title = "Filtered Title",
                    Text = "Filtered Text",
                    Author = "Filtered Author",
                    ImageName = "filtered.png",
                    ImageUrl = "filteredUrl.png",
                    CreatedAt = new DateTime(2024, 06, 15)
                },
                new Article
                {
                    Id = Guid.NewGuid(),
                    Title = "other Title",
                    Text = "other Text",
                    Author = "other Author",
                    ImageName = "other.png",
                    ImageUrl = "otherUrl.png",
                    CreatedAt = new DateTime(2024, 10, 01)
                }
            };
        }
    }
}
