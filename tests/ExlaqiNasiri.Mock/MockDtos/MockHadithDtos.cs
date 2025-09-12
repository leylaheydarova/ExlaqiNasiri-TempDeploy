using ExlaqiNasiri.Application.DTOs.Hadith;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace ExlaqiNasiri.Mock.MockDtos
{
    public static class MockHadithDtos
    {
        public static IFormFile CreateMockFormFile(string fileName, string contentType)
        {
            var content = "Fake file content";
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
            return new FormFile(stream, 0, stream.Length, "file", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = contentType
            };
        }

        public static HadithCreateDto CreateValidDto()
        {
            return new HadithCreateDto
            {
                CategoryId = Guid.NewGuid(),
                HadithContext = "Test Context",
                Text = "Test Text",
                Source = "Test Source",
                Image = CreateMockFormFile("test.jpg", "image/jpeg")
            };
        }

        public static HadithCreateDto CreateInvalidDto()
        {
            return new HadithCreateDto
            {
                CategoryId = Guid.NewGuid(),
                HadithContext = string.Empty,
                Text = string.Empty,
                Source = string.Empty,
                Image = null
            };
        }

        public static HadithPatchDto CreateUpdateDto()
        {
            return new HadithPatchDto
            {
                Text = "Updated text",
                HadithContext = "Updated context",
                Source = "Updated source",
                Image = CreateMockFormFile("updated.png", "image/png")
            };
        }

        public static HadithFilterDto CreateFilterDto()
        {
            return new HadithFilterDto
            {
                CreatedFrom = new DateTime(2024, 01, 01),
                CreatedTo = new DateTime(2025, 01, 01),
                CategoryId = Guid.NewGuid(),
                Source = "Test"
            };
        }

        public static HadithFilterDto CreateFilterDtoWithDifferentSource()
        {
            return new HadithFilterDto
            {
                CreatedFrom = new DateTime(2023, 01, 01),
                CreatedTo = new DateTime(2024, 01, 01),
                CategoryId = Guid.NewGuid(),
                Source = "OtherTest"
            };
        }
    }
}
