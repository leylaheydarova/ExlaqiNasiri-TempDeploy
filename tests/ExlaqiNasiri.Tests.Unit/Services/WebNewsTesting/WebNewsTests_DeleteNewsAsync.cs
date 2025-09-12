using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.Abstraction.Helper;
using ExlaqiNasiri.Application.Repositories.News;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Persistence.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Tests.Unit.Services.WebNewsTesting
{
    public class WebNewsTests_DeleteNewsAsync
    {
        IWebNewsService _service;
        Mock<IWebNewsReadRepository> _readRepo;
        Mock<IWebNewsWriteRepository> _writeRepo;
        Mock<IHttpContextAccessor> _accessor;
        Mock<IWebHostEnvironment> _env;
        Mock<IFileUpload> _fileHandler;

        public WebNewsTests_DeleteNewsAsync()
        {
            _readRepo = new Mock<IWebNewsReadRepository>();
            _writeRepo = new Mock<IWebNewsWriteRepository>();
            _env = new Mock<IWebHostEnvironment>();
            _accessor = new Mock<IHttpContextAccessor>();
            _fileHandler = new Mock<IFileUpload>();

            _service = new WebNewsService(_readRepo.Object, _writeRepo.Object, _env.Object, _accessor.Object, _fileHandler.Object);
        }

        [Fact]
        public async Task DeleteAsync_WhenHadithNotFound_ReturnsFailure()
        {
            _readRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), true)).ReturnsAsync((WebNews)null);

            var result = await _service.DeleteAsync(Guid.NewGuid());

            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, news is not found!", result.Error?.Message);
        }

        [Fact]
        public async Task DeleteAsync_WhenRemoveFails_ReturnsFailure()
        {
            var news = new WebNews { Id = Guid.NewGuid(), ImageName = "mock.png" };
            _readRepo.Setup(r => r.GetByIdAsync(news.Id, true)).ReturnsAsync(news);
            _writeRepo.Setup(r => r.RemovePermanently(news)).Returns(false);

            var result = await _service.DeleteAsync(news.Id);

            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, the operation 'delete news' failed!", result.Error?.Message);
        }

        [Fact]
        public async Task DeleteAsync_WhenSaveFails_ReturnsFailure()
        {
            var news = new WebNews { Id = Guid.NewGuid(), ImageName = "img.png" };
            _readRepo.Setup(r => r.GetByIdAsync(news.Id, true)).ReturnsAsync(news);
            _writeRepo.Setup(r => r.RemovePermanently(news)).Returns(true);
            _writeRepo.Setup(r => r.SaveAsync()).ReturnsAsync(0);

            var result = await _service.DeleteAsync(news.Id);

            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, the save news failed!", result.Error?.Message);
        }

        [Fact]
        public async Task DeleteAsync_WhenAllValid_ReturnsSuccess()
        {
            var news = new WebNews { Id = Guid.NewGuid(), ImageName = "img.png" };
            _readRepo.Setup(r => r.GetByIdAsync(news.Id, true)).ReturnsAsync(news);
            _writeRepo.Setup(r => r.RemovePermanently(news)).Returns(true);
            _writeRepo.Setup(r => r.SaveAsync()).ReturnsAsync(1);

            var result = await _service.DeleteAsync(news.Id);

            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
        }

    }
}
