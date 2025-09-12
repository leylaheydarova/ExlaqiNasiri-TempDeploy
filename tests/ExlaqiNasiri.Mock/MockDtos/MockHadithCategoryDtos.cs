using ExlaqiNasiri.Application.DTOs.HadithCategory;

namespace ExlaqiNasiri.Mock.MockDtos
{
    public class MockHadithCategoryDtos
    {
        public static HadithCategoryFilterDto CreateFilterDto()
        {
            return new HadithCategoryFilterDto
            {
                CreatedFrom = new DateTime(2023, 1, 1),
                CreatedTo = new DateTime(2023, 12, 31),
                isDeleted = false
            };
        }

        public static HadithCategoryCommandDTO CreateValidCommandDto()
        {
            return new HadithCategoryCommandDTO
            {
                CategoryName = "Test 2"
            };
        }

        public static HadithCategoryCommandDTO CreateEmptyCommandDto()
        {
            return new HadithCategoryCommandDTO
            {
                CategoryName = string.Empty
            };
        }
    }
}
