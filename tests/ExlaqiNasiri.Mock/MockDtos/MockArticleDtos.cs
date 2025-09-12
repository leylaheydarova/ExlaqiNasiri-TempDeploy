using ExlaqiNasiri.Application.DTOs.Article;
using ExlaqiNasiri.Application.DTOs.Hadith;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Mock.MockDtos
{
    public class MockArticleDtos
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

        public static ArticleCommandDto CreateMockCommandDto()
        {
            return new ArticleCommandDto
            {
                Title = "Test Title",
                Text = "Test Text",
                Author = "Test Author",
                Image = CreateMockFormFile("test.jpg", "image/jpeg")
            };
        }

        public static ArticleCommandDto CreateInvalidDto()
        {
            return new ArticleCommandDto
            {
                Title = string.Empty,
                Text = string.Empty,
                Author= string.Empty,
                Image = null
            };
        }

        public static ArticleFilterDto CreateFilterDto()
        {
            return new ArticleFilterDto
            {
                CreatedFrom = new DateTime(2024, 01, 01),
                CreatedTo = new DateTime(2025, 01, 01)
            };
        }
    }
}
