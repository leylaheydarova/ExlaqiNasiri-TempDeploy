using ExlaqiNasiri.Application.Abstraction.Helper;
using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.Repositories.Hadiths;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.IO;
using System.Threading.Tasks;
using ExlaqiNasiri.Persistence.Services;
using ExlaqiNasiri.Application.DTOs.Hadith;
using ExlaqiNasiri.Application.ResultPattern;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Application.Constants;
using ExlaqiNasiri.Mock.MockDbModels;
using ExlaqiNasiri.Mock.MockDtos;
using Xunit;

namespace ExlaqiNasiri.Tests.Unit.Services.HadithTests
{
    public class HadithTests_UpdateHadithAsync
    {
        IHadithService _service;
        Mock<IHadithReadRepository> _readRepo;
        Mock<IHadithWriteRepository> _writeRepo;
        Mock<IHttpContextAccessor> _accessor;
        Mock<IWebHostEnvironment> _env;
        Mock<IFileUpload> _fileHandler;

        public HadithTests_UpdateHadithAsync()
        {
            _readRepo = new Mock<IHadithReadRepository>();
            _writeRepo = new Mock<IHadithWriteRepository>();
            _env = new Mock<IWebHostEnvironment>();
            _accessor = new Mock<IHttpContextAccessor>();
            _fileHandler = new Mock<IFileUpload>();

            _env.Setup(x => x.WebRootPath).Returns("wwwroot");

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Scheme = "http";
            httpContext.Request.Host = new HostString("localhost");
            _accessor.Setup(x => x.HttpContext).Returns(httpContext);

            _service = new HadithService(null, _accessor.Object, _env.Object, _fileHandler.Object, _readRepo.Object, _writeRepo.Object);
        }

        [Fact]
        public async Task UpdateAsync_HadithNotFound_ReturnsFailure()
        {
            var id = Guid.NewGuid();
            var dto = MockHadithDtos.CreateUpdateDto();

            _readRepo.Setup(x => x.GetWhereAsync(c => c.Id == id, true)).ReturnsAsync((Hadith)null);

            var result = await _service.UpdateAsync(id, dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, hadith is not found!", result.Error?.Message);
        }

        [Fact]
        public async Task UpdateAsync_UpdateFails_ReturnsFailure()
        {
            var id = Guid.NewGuid();
            var dto = MockHadithDtos.CreateUpdateDto();
            var hadith = MockHadith.CreateMockEntity();

            _readRepo.Setup(x => x.GetWhereAsync(c => c.Id == id, true)).ReturnsAsync(hadith);
            _fileHandler.Setup(f => f.UploadFile(dto.Image, "wwwroot", FilePaths.HadithImagePath)).Returns("newImage.png");
            _writeRepo.Setup(w => w.Update(hadith)).Returns(false);

            var result = await _service.UpdateAsync(id, dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, the operation 'update hadith' failed!", result.Error?.Message);
        }

        [Fact]
        public async Task UpdateAsync_SaveFails_ReturnsFailure()
        {
            var id = Guid.NewGuid();
            var dto = MockHadithDtos.CreateUpdateDto();
            var hadith = MockHadith.CreateMockEntity();

            _readRepo.Setup(x => x.GetWhereAsync(c => c.Id == id, true)).ReturnsAsync(hadith);
            _fileHandler.Setup(f => f.UploadFile(dto.Image, "wwwroot", FilePaths.HadithImagePath)).Returns("newImage.png");
            _writeRepo.Setup(w => w.Update(hadith)).Returns(true);
            _writeRepo.Setup(w => w.SaveAsync()).ReturnsAsync(0);

            var result = await _service.UpdateAsync(id, dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, the save hadith failed!", result.Error?.Message);
        }

        [Fact]
        public async Task UpdateAsync_WhenValid_ReturnsSuccess()
        {
            var id = Guid.NewGuid();
            var dto = MockHadithDtos.CreateUpdateDto();
            var hadith = MockHadith.CreateMockEntity();

            _readRepo.Setup(x => x.GetWhereAsync(c => c.Id == id, true)).ReturnsAsync(hadith);
            _fileHandler.Setup(f => f.UploadFile(dto.Image, "wwwroot", FilePaths.HadithImagePath)).Returns("newImage.png");
            _writeRepo.Setup(w => w.Update(hadith)).Returns(true);
            _writeRepo.Setup(w => w.SaveAsync()).ReturnsAsync(1);

            var result = await _service.UpdateAsync(id, dto);

            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
        }
    }
}
