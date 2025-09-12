using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.Abstraction.Helper;
using ExlaqiNasiri.Application.Constants;
using ExlaqiNasiri.Application.Repositories.Articles;
using ExlaqiNasiri.Application.ResultPattern;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Mock.MockDbModels;
using ExlaqiNasiri.Mock.MockDtos;
using ExlaqiNasiri.Persistence.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Tests.Unit.Services.ArticleTests
{
    public class ArticleTests_CreateArticleAsync
    {
        IArticleService _service;
        Mock<IArticleReadRepository> _readRepo;
        Mock<IArticleWriteRepository> _writeRepo;
        Mock<IHttpContextAccessor> _accessor;
        Mock<IWebHostEnvironment> _env;
        Mock<IFileUpload> _fileHandler;

        public ArticleTests_CreateArticleAsync()
        {
            _readRepo = new Mock<IArticleReadRepository>();
            _writeRepo = new Mock<IArticleWriteRepository>();
            _accessor = new Mock<IHttpContextAccessor>();
            _env = new Mock<IWebHostEnvironment>();
            _fileHandler = new Mock<IFileUpload>();

            _env.Setup(x => x.WebRootPath).Returns("wwwroot");

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Scheme = "http";
            httpContext.Request.Host = new HostString("localhost");
            _accessor.Setup(x => x.HttpContext).Returns(httpContext);

            _service = new ArticleService(
                _readRepo.Object,
                _writeRepo.Object,
                _env.Object,
                _accessor.Object,
                _fileHandler.Object
            );
        }
        [Fact]
        public async Task CreateAsync_WhenCategoryNotFound_ReturnsFailure()
        {
            var dto = MockArticleDtos.CreateInvalidDto();
            var result = await _service.CreateAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, the operation 'add article' failed!", result.Error?.Message);
        }

        [Fact]
        public async Task CreateAsync_WhenAddFails_ReturnsFailure()
        {
            var dto = MockArticleDtos.CreateMockCommandDto();

            _fileHandler.Setup(x => x.UploadFile(It.IsAny<IFormFile>(), "wwwroot", FilePaths.ArticleImagePath))
                       .Returns("mock.png");

            _writeRepo.Setup(x => x.AddAsync(It.IsAny<Article>())).ReturnsAsync(false);

            var result = await _service.CreateAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, the operation 'add article' failed!", result.Error?.Message);
        }

        [Fact]
        public async Task CreateAsync_WhenSaveFails_ReturnsFailure()
        {
            var dto = MockArticleDtos.CreateMockCommandDto();
            _fileHandler.Setup(x => x.UploadFile(It.IsAny<IFormFile>(), "wwwroot", FilePaths.ArticleImagePath))
                       .Returns("mock.png");

            _writeRepo.Setup(x => x.AddAsync(It.IsAny<Article>())).ReturnsAsync(true);
            _writeRepo.Setup(x => x.SaveAsync()).ReturnsAsync(0);

            var result = await _service.CreateAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, the save article failed!", result.Error?.Message);
        }

        [Fact]
        public async Task CreateAsync_WhenValid_ReturnsSuccess()
        {
            var dto = MockArticleDtos.CreateMockCommandDto();
            _fileHandler.Setup(x => x.UploadFile(It.IsAny<IFormFile>(), "wwwroot", FilePaths.ArticleImagePath))
                       .Returns("mock.png");

            _writeRepo.Setup(x => x.AddAsync(It.IsAny<Article>())).ReturnsAsync(true);
            _writeRepo.Setup(x => x.SaveAsync()).ReturnsAsync(1);

            var result = await _service.CreateAsync(dto);

            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
        }
    }
}
