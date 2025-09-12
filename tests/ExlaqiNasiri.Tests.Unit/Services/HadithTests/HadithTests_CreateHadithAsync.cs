using ExlaqiNasiri.Application.Abstraction.Helper;
using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.Repositories.Hadiths;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExlaqiNasiri.Persistence.Services;
using ExlaqiNasiri.Application.DTOs.Hadith;
using ExlaqiNasiri.Application.ResultPattern;
using ExlaqiNasiri.Persistence.Services.Helper;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Application.Constants;
using ExlaqiNasiri.Mock.MockDbModels;
using ExlaqiNasiri.Mock.MockDtos;

namespace ExlaqiNasiri.Tests.Unit.Services.HadithTests
{
    public class HadithTests_CreateHadithAsync
    {
        IHadithService _service;
        Mock<IHadithReadRepository> _readRepo;
        Mock<IHadithWriteRepository> _writeRepo;
        Mock<IHttpContextAccessor> _accessor;
        Mock<IWebHostEnvironment> _env;
        Mock<IFileUpload> _fileHandler;
        Mock<IGetEntityService> _getEntity;

        public HadithTests_CreateHadithAsync()
        {
            _readRepo = new Mock<IHadithReadRepository>();
            _writeRepo = new Mock<IHadithWriteRepository>();
            _env = new Mock<IWebHostEnvironment>();
            _accessor = new Mock<IHttpContextAccessor>();
            _fileHandler = new Mock<IFileUpload>();
            _getEntity = new Mock<IGetEntityService>();

            _env.Setup(x => x.WebRootPath).Returns("wwwroot");

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Scheme = "http";
            httpContext.Request.Host = new HostString("localhost");
            _accessor.Setup(x => x.HttpContext).Returns(httpContext);

            _service = new HadithService(_getEntity.Object, _accessor.Object, _env.Object, _fileHandler.Object, _readRepo.Object, _writeRepo.Object);
        }

        [Fact]
        public async Task CreateAsync_WhenCategoryNotFound_ReturnsFailure()
        {
            var dto = MockHadithDtos.CreateInvalidDto();

            _getEntity.Setup(x => x.GetHadithCategoryAsync(dto.CategoryId, false)).ReturnsAsync(Result<HadithCategory>.Failure(Error.NotFoundError("category")));

            var result = await _service.CreateAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, category is not found!", result.Error?.Message);
        }

        [Fact]
        public async Task CreateAsync_WhenAddFails_ReturnsFailure()
        {
            var dto = MockHadithDtos.CreateValidDto();
            var mockCategory = MockHadithCategory.CreateMockEntity();

            _getEntity.Setup(x => x.GetHadithCategoryAsync(dto.CategoryId, false)).ReturnsAsync(Result<HadithCategory>.Success(mockCategory));

            _fileHandler.Setup(x => x.UploadFile(It.IsAny<IFormFile>(), "wwwroot", FilePaths.HadithImagePath))
                       .Returns("mock.png");

            _writeRepo.Setup(x => x.AddAsync(It.IsAny<Hadith>())).ReturnsAsync(false);

            var result = await _service.CreateAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, the operation 'add hadith' failed!", result.Error?.Message);
        }

        [Fact]
        public async Task CreateAsync_WhenSaveFails_ReturnsFailure()
        {
            var dto = MockHadithDtos.CreateValidDto();
            var mockCategory = MockHadithCategory.CreateMockEntity();

            _getEntity.Setup(x => x.GetHadithCategoryAsync(dto.CategoryId, false)).ReturnsAsync(Result<HadithCategory>.Success(mockCategory));

            _fileHandler.Setup(x => x.UploadFile(It.IsAny<IFormFile>(), "wwwroot", FilePaths.HadithImagePath))
                       .Returns("mock.png");

            _writeRepo.Setup(x => x.AddAsync(It.IsAny<Hadith>())).ReturnsAsync(true);
            _writeRepo.Setup(x => x.SaveAsync()).ReturnsAsync(0);

            var result = await _service.CreateAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, the save hadith failed!", result.Error?.Message);
        }

        [Fact]
        public async Task CreateAsync_WhenValid_ReturnsSuccess()
        {
            var dto = MockHadithDtos.CreateValidDto();
            var mockCategory = MockHadithCategory.CreateMockEntity();

            _getEntity.Setup(x => x.GetHadithCategoryAsync(dto.CategoryId, false)).ReturnsAsync(Result<HadithCategory>.Success(mockCategory));

            _fileHandler.Setup(x => x.UploadFile(It.IsAny<IFormFile>(), "wwwroot", FilePaths.HadithImagePath))
                       .Returns("mock.png");

            _writeRepo.Setup(x => x.AddAsync(It.IsAny<Hadith>())).ReturnsAsync(true);
            _writeRepo.Setup(x => x.SaveAsync()).ReturnsAsync(1);

            var result = await _service.CreateAsync(dto);

            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
        }
    }
}
