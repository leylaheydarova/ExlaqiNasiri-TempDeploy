using ExlaqiNasiri.Application.Abstraction.Helper;
using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.Repositories.News;
using ExlaqiNasiri.Persistence.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Mock.MockDbModels;

namespace ExlaqiNasiri.Tests.Unit.Services.WebNewsTesting
{
    public class WebNewsTests_GetSingleAsync
    {
        IWebNewsService _service;
        Mock<IWebNewsReadRepository> _readRepo;
        Mock<IWebNewsWriteRepository> _writeRepo;
        Mock<IHttpContextAccessor> _accessor;
        Mock<IWebHostEnvironment> _env;
        Mock<IFileUpload> _fileHandler;

        public WebNewsTests_GetSingleAsync()
        {
            _readRepo = new Mock<IWebNewsReadRepository>();
            _writeRepo = new Mock<IWebNewsWriteRepository>();
            _env = new Mock<IWebHostEnvironment>();
            _accessor = new Mock<IHttpContextAccessor>();
            _fileHandler = new Mock<IFileUpload>();

            _service = new WebNewsService(_readRepo.Object, _writeRepo.Object, _env.Object, _accessor.Object, _fileHandler.Object);
        }

        [Fact]
        public async Task GetSingleAsync_WhenHadithExists_ReturnsSuccessWithDto()
        {
            var mockNews = MockWebNews.CreateMockEntity();
            _readRepo.Setup(r => r.GetWhereAsync(It.IsAny<System.Linq.Expressions.Expression<Func<WebNews, bool>>>(), true))
                     .ReturnsAsync(mockNews);

            var result = await _service.GetSingleAsync(mockNews.Id);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(mockNews.Id.ToString(), result.Value.Id);
            Assert.Equal(mockNews.Text, result.Value.Text);
            Assert.Equal(mockNews.Title, result.Value.Title);
            Assert.Equal(mockNews.ImageName, result.Value.ImageName);
            Assert.Equal(mockNews.ImageUrl, result.Value.ImageUrl);
        }

        [Fact]
        public async Task GetSingleAsync_WhenHadithDoesNotExist_ReturnsFailure()
        {
            _readRepo.Setup(r => r.GetWhereAsync(It.IsAny<System.Linq.Expressions.Expression<Func<WebNews, bool>>>(), true))
                     .ReturnsAsync((WebNews?)null);

            var result = await _service.GetSingleAsync(Guid.NewGuid());

            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Error);
            Assert.Equal("Sorry, news is not found!", result.Error.Message);
        }


        [Fact]
        public async Task GetSingleAsync_MapsAllPropertiesCorrectly()
        {
            var mockNews = new WebNews
            {
                Id = Guid.NewGuid(),
                Title = "Title test",
                Text = "Text test",
                ImageName = "image.png",
                ImageUrl = "urlimage.png"
            };

            _readRepo.Setup(r => r.GetWhereAsync(It.IsAny<System.Linq.Expressions.Expression<Func<WebNews, bool>>>(), true))
                     .ReturnsAsync(mockNews);

            var result = await _service.GetSingleAsync(mockNews.Id);

            Assert.True(result.IsSuccess);
            Assert.Equal(mockNews.Text, result.Value.Text);
            Assert.Equal(mockNews.Title, result.Value.Title);
            Assert.Equal(mockNews.ImageName, result.Value.ImageName);
            Assert.Equal(mockNews.ImageUrl, result.Value.ImageUrl);
        }
    }
}
