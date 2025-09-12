using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.Abstraction.Helper;
using ExlaqiNasiri.Application.Repositories.Articles;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Mock.MockDbModels;
using ExlaqiNasiri.Persistence.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Tests.Unit.Services.ArticleTests
{
    public class ArticleTests_GetSingleAsync
    {
        IArticleService _service;
        Mock<IArticleReadRepository> _readRepo;
        Mock<IArticleWriteRepository> _writeRepo;
        Mock<IHttpContextAccessor> _accessor;
        Mock<IWebHostEnvironment> _env;
        Mock<IFileUpload> _fileHandler;

        public ArticleTests_GetSingleAsync()
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

            _service = new ArticleService(_readRepo.Object, _writeRepo.Object, _env.Object, _accessor.Object, _fileHandler.Object);
        }

        [Fact]
        public async Task GetSingleAsync_WhenArticleExists_ReturnsSuccessWithDto()
        {
            var mockArticle = MockArticle.CreateMockEntity();

            _readRepo.Setup(r => r.GetWhereAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Article, bool>>>(), true))
                     .ReturnsAsync(mockArticle);

            var result = await _service.GetSingleAsync(mockArticle.Id);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(mockArticle.Id.ToString(), result.Value.Id);
            Assert.Equal(mockArticle.Title, result.Value.Title);
            Assert.Equal(mockArticle.Text, result.Value.Text);
            Assert.Equal(mockArticle.Author, result.Value.Author);
            Assert.Equal(mockArticle.ImageName, result.Value.ImageName);
            Assert.Equal(mockArticle.ImageUrl, result.Value.ImageUrl);
        }

        [Fact]
        public async Task GetSingleAsync_WhenArticleDoesNotExist_ReturnsFailure()
        {
            _readRepo.Setup(r => r.GetWhereAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Article, bool>>>(), true))
                     .ReturnsAsync((Article?)null);

            var result = await _service.GetSingleAsync(Guid.NewGuid());

            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Error);
            Assert.Equal("Sorry, article is not found!", result.Error.Message);
        }

        [Fact]
        public async Task GetSingleAsync_MapsAllPropertiesCorrectly()
        {
            var mockArticle = new Article
            {
                Id = Guid.NewGuid(),
                Title = "Context test",
                Text = "Text test",
                Author = "Author test",
                ImageName = "image.png",
                ImageUrl = "http://localhost/image.png"
            };

            _readRepo.Setup(r => r.GetWhereAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Article, bool>>>(), true))
                     .ReturnsAsync(mockArticle);

            var result = await _service.GetSingleAsync(mockArticle.Id);

            Assert.True(result.IsSuccess);
            Assert.Equal(mockArticle.Title, result.Value.Title);
            Assert.Equal(mockArticle.Text, result.Value.Text);
            Assert.Equal(mockArticle.Author, result.Value.Author);
            Assert.Equal(mockArticle.ImageName, result.Value.ImageName);
            Assert.Equal(mockArticle.ImageUrl, result.Value.ImageUrl);
        }
    }
}
