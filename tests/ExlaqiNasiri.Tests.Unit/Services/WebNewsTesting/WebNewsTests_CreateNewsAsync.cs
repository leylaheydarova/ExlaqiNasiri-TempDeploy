using ExlaqiNasiri.Application.Abstraction.Helper;
using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.Repositories.News;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExlaqiNasiri.Persistence.Services;
using ExlaqiNasiri.Application.ResultPattern;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Mock.MockDtos;
using ExlaqiNasiri.Mock.MockDbModels;
using ExlaqiNasiri.Application.Constants;

namespace ExlaqiNasiri.Tests.Unit.Services.WebNewsTesting
{
    public class WebNewsTests_CreateNewsAsync
    {
        IWebNewsService _service;
        Mock<IWebNewsReadRepository> _readRepo;
        Mock<IWebNewsWriteRepository> _writeRepo;
        Mock<IHttpContextAccessor> _accessor;
        Mock<IWebHostEnvironment> _env;
        Mock<IFileUpload> _fileHandler;

        public WebNewsTests_CreateNewsAsync()
        {
            _readRepo = new Mock<IWebNewsReadRepository>();
            _writeRepo = new Mock<IWebNewsWriteRepository>();
            _env = new Mock<IWebHostEnvironment>();
            _accessor = new Mock<IHttpContextAccessor>();
            _fileHandler = new Mock<IFileUpload>();

            _env.Setup(x => x.WebRootPath).Returns("wwwroot");

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Scheme = "http";
            httpContext.Request.Host = new HostString("localhost");
            _accessor.Setup(x => x.HttpContext).Returns(httpContext);

            _service = new WebNewsService(_readRepo.Object, _writeRepo.Object, _env.Object, _accessor.Object, _fileHandler.Object);
        }

        [Fact]
        public async Task CreateAsync_WhenCategoryNotFound_ReturnsFailure()
        {
            var dto = MockWebNewsDtos.CreateInvalidDto();

            var result = await _service.CreateAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, the operation 'add news' failed!", result.Error?.Message);
        }

        [Fact]
        public async Task CreateAsync_WhenAddFails_ReturnsFailure()
        {
            var dto = MockWebNewsDtos.CreateValidDto();
            var mockCategory = MockWebNews.CreateMockEntity();

            _fileHandler.Setup(x => x.UploadFile(It.IsAny<IFormFile>(), "wwwroot", FilePaths.WebNewsImagePath))
                       .Returns("mock.png");

            _writeRepo.Setup(x => x.AddAsync(It.IsAny<WebNews>())).ReturnsAsync(false);

            var result = await _service.CreateAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, the operation 'add news' failed!", result.Error?.Message);
        }


        [Fact]
        public async Task CreateAsync_WhenSaveFails_ReturnsFailure()
        {
            var dto = MockWebNewsDtos.CreateValidDto();
            var mockCategory = MockWebNews.CreateMockEntity();

            _fileHandler.Setup(x => x.UploadFile(It.IsAny<IFormFile>(), "wwwroot", FilePaths.WebNewsImagePath))
                       .Returns("mock.png");

            _writeRepo.Setup(x => x.AddAsync(It.IsAny<WebNews>())).ReturnsAsync(true);
            _writeRepo.Setup(x => x.SaveAsync()).ReturnsAsync(0);

            var result = await _service.CreateAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, the save news failed!", result.Error?.Message);
        }

        [Fact]
        public async Task CreateAsync_WhenValid_ReturnsSuccess()
        {
            var dto = MockWebNewsDtos.CreateValidDto();
            var mockCategory = MockWebNews.CreateMockEntity();

            _fileHandler.Setup(x => x.UploadFile(It.IsAny<IFormFile>(), "wwwroot", FilePaths.WebNewsImagePath))
                       .Returns("mock.png");

            _writeRepo.Setup(x => x.AddAsync(It.IsAny<WebNews>())).ReturnsAsync(true);
            _writeRepo.Setup(x => x.SaveAsync()).ReturnsAsync(1);

            var result = await _service.CreateAsync(dto);

            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
        }
    }
}
