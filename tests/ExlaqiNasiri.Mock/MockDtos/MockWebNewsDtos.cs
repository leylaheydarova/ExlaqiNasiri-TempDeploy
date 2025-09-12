using ExlaqiNasiri.Application.DTOs.Hadith;
using ExlaqiNasiri.Application.DTOs.WebNews;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace ExlaqiNasiri.Mock.MockDtos
{
    public static class MockWebNewsDtos
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

        public static WebNewsCommandDto CreateValidDto()
        {
            return new WebNewsCommandDto
            {
                Title = "TestTitle",
                Text = "TestText",
                ImageName = CreateMockFormFile("mock.jpg", "mock/jpeg")
            };
        }

        public static WebNewsCommandDto CreateInvalidDto()
        {
            return new WebNewsCommandDto
            {
                Title = string.Empty,
                Text = string.Empty,
                ImageName = null
            };
        }

        public static WebNewsFilterDto CreateFilterDto()
        {
            return new WebNewsFilterDto
            {
                CreatedFrom = new DateTime(2024, 01, 01),
                CreatedTo = new DateTime(2025, 01, 01)
            };
        }


    }
}
