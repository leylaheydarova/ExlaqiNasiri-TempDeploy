using ExlaqiNasiri.Application.Abstraction.Helper;
using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.Repositories.Articles;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Mock.MockDbModels;
using ExlaqiNasiri.Mock.MockDtos;
using ExlaqiNasiri.Persistence.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using ExlaqiNasiri.Application.Constants;

public class ArticleTests_UpdateArticleAsync
{
    IArticleService _service;
    Mock<IArticleReadRepository> _readRepo;
    Mock<IArticleWriteRepository> _writeRepo;
    Mock<IHttpContextAccessor> _accessor;
    Mock<IWebHostEnvironment> _env;
    Mock<IFileUpload> _fileHandler;

    public ArticleTests_UpdateArticleAsync()
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
    public async Task UpdateAsync_HadithNotFound_ReturnsFailure()
    {
        var id = Guid.NewGuid();
        var dto = MockArticleDtos.CreateMockCommandDto();

        _readRepo.Setup(x => x.GetWhereAsync(c => c.Id == id, true)).ReturnsAsync((Article)null);

        var result = await _service.UpdateAsync(id, dto);

        Assert.False(result.IsSuccess);
        Assert.Equal("Sorry, article is not found!", result.Error?.Message);
    }

    [Fact]
    public async Task UpdateAsync_UpdateFails_ReturnsFailure()
    {
        var id = Guid.NewGuid();
        var dto = MockArticleDtos.CreateMockCommandDto();
        var article = MockArticle.CreateMockEntity();

        _readRepo.Setup(x => x.GetWhereAsync(c => c.Id == id, true)).ReturnsAsync(article);
        _fileHandler.Setup(f => f.UploadFile(dto.Image, "wwwroot", FilePaths.ArticleImagePath)).Returns("newImage.png");
        _writeRepo.Setup(w => w.Update(article)).Returns(false);

        var result = await _service.UpdateAsync(id, dto);

        Assert.False(result.IsSuccess);
        Assert.Equal("Sorry, the operation 'update article' failed!", result.Error?.Message);
    }

    [Fact]
    public async Task UpdateAsync_SaveFails_ReturnsFailure()
    {
        var id = Guid.NewGuid();
        var dto = MockArticleDtos.CreateMockCommandDto();
        var article = MockArticle.CreateMockEntity();

        _readRepo.Setup(x => x.GetWhereAsync(c => c.Id == id, true)).ReturnsAsync(article);
        _fileHandler.Setup(f => f.UploadFile(dto.Image, "wwwroot", FilePaths.ArticleImagePath)).Returns("newImage.png");
        _writeRepo.Setup(w => w.Update(article)).Returns(true);
        _writeRepo.Setup(w => w.SaveAsync()).ReturnsAsync(0);

        var result = await _service.UpdateAsync(id, dto);

        Assert.False(result.IsSuccess);
        Assert.Equal("Sorry, the save article failed!", result.Error?.Message);
    }

    [Fact]
    public async Task UpdateAsync_WhenValid_ReturnsSuccess()
    {
        var id = Guid.NewGuid();
        var dto = MockArticleDtos.CreateMockCommandDto();
        var article = MockArticle.CreateMockEntity();

        _readRepo.Setup(x => x.GetWhereAsync(c => c.Id == id, true)).ReturnsAsync(article);
        _fileHandler.Setup(f => f.UploadFile(dto.Image, "wwwroot", FilePaths.ArticleImagePath)).Returns("newImage.png");
        _writeRepo.Setup(w => w.Update(article)).Returns(true);
        _writeRepo.Setup(w => w.SaveAsync()).ReturnsAsync(1);

        var result = await _service.UpdateAsync(id, dto);

        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
    }
}
