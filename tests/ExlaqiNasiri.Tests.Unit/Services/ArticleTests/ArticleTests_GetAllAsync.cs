using ExlaqiNasiri.Application.Abstraction.Helper;
using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.Repositories.Articles;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExlaqiNasiri.Persistence.Services;
using ExlaqiNasiri.Application.DTOs.Article;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Mock.HelperMock;
using ExlaqiNasiri.Mock.MockDbModels;
using ExlaqiNasiri.Mock.MockDtos;

namespace ExlaqiNasiri.Tests.Unit.Services.ArticleTests
{
    public class ArticleTests_GetAllAsync
    {
        IArticleService _service;
        Mock<IArticleReadRepository> _readRepo;
        Mock<IArticleWriteRepository> _writeRepo;
        Mock<IHttpContextAccessor> _accessor;
        Mock<IWebHostEnvironment> _env;
        Mock<IFileUpload> _fileHandler;

        public ArticleTests_GetAllAsync()
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
        public async Task GetAllAsync_DtoIsNull_ReturnsAll()
        {
            var mockData = MockArticle.CreateMockList();
            var asyncData = new MockDbSet.TestAsyncEnumerable<Article>(mockData);

            _readRepo.Setup(x => x.GetAll(false)).Returns(asyncData);

            var result = await _service.GetAllAsync(null);

            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Count);
            Assert.Equal("Test Title 1", result.Value[0].Title);
            Assert.Equal("Test Text 2", result.Value[1].Text);
        }

        [Fact]
        public async Task GetAllAsync_DtoWithDate_ReturnsFilteredData()
        {
            var filterDto = MockArticleDtos.CreateFilterDto();

            var filteredData = MockArticle.CreateMockListWithFilterData(Guid.NewGuid());
            var asyncQueryable = new MockDbSet.TestAsyncEnumerable<Article>(filteredData);

            _readRepo.Setup(r => r.GetFilteredAsync(filterDto)).Returns(asyncQueryable);

            var result = await _service.GetAllAsync(filterDto);

            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Count);
            Assert.All(result.Value, a =>
            {
                Assert.Contains("Title", a.Title);
            });
        }

        [Fact]
        public async Task GetAllAsync_WhenNoDataAvailable_ReturnsEmptyList()
        {
            var filterDto = MockArticleDtos.CreateFilterDto();
            var emptyData = new List<Article>();
            var asyncQueryable = new MockDbSet.TestAsyncEnumerable<Article>(emptyData);

            _readRepo.Setup(r => r.GetFilteredAsync(filterDto)).Returns(asyncQueryable);

            var result = await _service.GetAllAsync(filterDto);

            Assert.True(result.IsSuccess);
            Assert.Empty(result.Value);
        }
    }
}
