using ExlaqiNasiri.Application.Abstraction.Helper;
using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.Repositories.Articles;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExlaqiNasiri.Persistence.Services;
using ExlaqiNasiri.Domain.Entities;

namespace ExlaqiNasiri.Tests.Unit.Services.ArticleTests
{
    public class ArticleTests_DeleteArticleAsync
    {
        IArticleService _service;
        Mock<IArticleReadRepository> _readRepo;
        Mock<IArticleWriteRepository> _writeRepo;
        Mock<IHttpContextAccessor> _accessor;
        Mock<IWebHostEnvironment> _env;
        Mock<IFileUpload> _fileHandler;

        public ArticleTests_DeleteArticleAsync()
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
        public async Task DeleteAsync_WhenHadithNotFound_ReturnsFailure()
        {
            _readRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), true)).ReturnsAsync((Article)null);

            var result = await _service.DeleteAsync(Guid.NewGuid());

            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, article is not found!", result.Error?.Message);
        }

        [Fact]
        public async Task DeleteAsync_WhenRemoveFails_ReturnsFailure()
        {
            var article = new Article{ Id = Guid.NewGuid(), ImageName = "mock.png" };
            _readRepo.Setup(r => r.GetByIdAsync(article.Id, true)).ReturnsAsync(article);
            _writeRepo.Setup(r => r.RemovePermanently(article)).Returns(false);

            var result = await _service.DeleteAsync(article.Id);

            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, the operation 'delete article' failed!", result.Error?.Message);
        }

        [Fact]
        public async Task DeleteAsync_WhenSaveFails_ReturnsFailure()
        {
            var article = new Article { Id = Guid.NewGuid(), ImageName = "img.png" };
            _readRepo.Setup(r => r.GetByIdAsync(article.Id, true)).ReturnsAsync(article);
            _writeRepo.Setup(r => r.RemovePermanently(article)).Returns(true);
            _writeRepo.Setup(r => r.SaveAsync()).ReturnsAsync(0);

            var result = await _service.DeleteAsync(article.Id);

            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, the save article failed!", result.Error?.Message);
        }

        [Fact]
        public async Task DeleteAsync_WhenAllValid_ReturnsSuccess()
        {
            var article = new Article { Id = Guid.NewGuid(), ImageName = "img.png" };
            _readRepo.Setup(r => r.GetByIdAsync(article.Id, true)).ReturnsAsync(article);
            _writeRepo.Setup(r => r.RemovePermanently(article)).Returns(true);
            _writeRepo.Setup(r => r.SaveAsync()).ReturnsAsync(1);

            var result = await _service.DeleteAsync(article.Id);

            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
        }

    }
}
