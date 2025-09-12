using ExlaqiNasiri.Mock.MockDtos;
using ExlaqiNasiri.Mock.MockDbModels;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Application.Constants;
using ExlaqiNasiri.Application.Abstraction.Helper;
using ExlaqiNasiri.Application.Repositories.News;
using ExlaqiNasiri.Persistence.Services;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ExlaqiNasiri.Application.Abstraction;

namespace ExlaqiNasiri.Tests.Unit.Services.WebNewsTesting
{
    public class WebNewsTests_UpdateNewsAsync
    {
        private readonly IWebNewsService _service;
        private readonly Mock<IWebNewsReadRepository> _readRepo;
        private readonly Mock<IWebNewsWriteRepository> _writeRepo;
        private readonly Mock<IFileUpload> _fileHandler;

        public WebNewsTests_UpdateNewsAsync()
        {
            _readRepo = new Mock<IWebNewsReadRepository>();
            _writeRepo = new Mock<IWebNewsWriteRepository>();
            _fileHandler = new Mock<IFileUpload>();

            var env = new Mock<IWebHostEnvironment>();
            env.Setup(x => x.WebRootPath).Returns("wwwroot");

            var accessor = new Mock<IHttpContextAccessor>();
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Scheme = "http";
            httpContext.Request.Host = new HostString("localhost");
            accessor.Setup(x => x.HttpContext).Returns(httpContext);

            _service = new WebNewsService(_readRepo.Object, _writeRepo.Object, env.Object, accessor.Object, _fileHandler.Object);
        }

        [Fact]
        public async Task UpdateAsync_WebNewsNotFound_ReturnsFailure()
        {
            var id = Guid.NewGuid();
            var dto = MockWebNewsDtos.CreateValidDto();

            _readRepo.Setup(x => x.GetWhereAsync(c => c.Id == id, true))
                .ReturnsAsync((WebNews)null);

            var result = await _service.UpdateAsync(id, dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, news is not found!", result.Error?.Message);
        }

        [Fact]
        public async Task UpdateAsync_UpdateFails_ReturnsFailure()
        {
            var id = Guid.NewGuid();
            var dto = MockWebNewsDtos.CreateValidDto();
            var entity = MockWebNews.CreateMockEntity();

            _readRepo.Setup(x => x.GetWhereAsync(c => c.Id == id, true)).ReturnsAsync(entity);
            _fileHandler.Setup(f => f.UploadFile(dto.ImageName, "wwwroot", FilePaths.WebNewsImagePath)).Returns("newImage.png");
            _writeRepo.Setup(w => w.Update(entity)).Returns(false);

            var result = await _service.UpdateAsync(id, dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, the operation 'update news' failed!", result.Error?.Message);
        }

        [Fact]
        public async Task UpdateAsync_SaveFails_ReturnsFailure()
        {
            var id = Guid.NewGuid();
            var dto = MockWebNewsDtos.CreateValidDto();
            var entity = MockWebNews.CreateMockEntity();

            _readRepo.Setup(x => x.GetWhereAsync(c => c.Id == id, true)).ReturnsAsync(entity);
            _fileHandler.Setup(f => f.UploadFile(dto.ImageName, "wwwroot", FilePaths.WebNewsImagePath)).Returns("newImage.png");
            _writeRepo.Setup(w => w.Update(entity)).Returns(true);
            _writeRepo.Setup(w => w.SaveAsync()).ReturnsAsync(0);

            var result = await _service.UpdateAsync(id, dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, the save news failed!", result.Error?.Message);
        }

        [Fact]
        public async Task UpdateAsync_WhenValid_ReturnsSuccess()
        {
            var id = Guid.NewGuid();
            var dto = MockWebNewsDtos.CreateValidDto();
            var entity = MockWebNews.CreateMockEntity();

            _readRepo.Setup(x => x.GetWhereAsync(c => c.Id == id, true)).ReturnsAsync(entity);
            _fileHandler.Setup(f => f.UploadFile(dto.ImageName, "wwwroot", FilePaths.WebNewsImagePath)).Returns("newImage.png");
            _writeRepo.Setup(w => w.Update(entity)).Returns(true);
            _writeRepo.Setup(w => w.SaveAsync()).ReturnsAsync(1);

            var result = await _service.UpdateAsync(id, dto);

            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
        }
    }
}
