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
using ExlaqiNasiri.Application.DTOs.Hadith;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Mock.HelperMock;
using ExlaqiNasiri.Mock.MockDbModels;
using ExlaqiNasiri.Mock.MockDtos;
using ExlaqiNasiri.Application.DTOs.WebNews;

namespace ExlaqiNasiri.Tests.Unit.Services.WebNewsTesting
{
    public class WebNewsTests_GetAllAsync
    {
        IWebNewsService _service;
        Mock<IWebNewsReadRepository> _readRepo;
        Mock<IWebNewsWriteRepository> _writeRepo;
        Mock<IHttpContextAccessor> _accessor;
        Mock<IWebHostEnvironment> _env;
        Mock<IFileUpload> _fileHandler;

        public WebNewsTests_GetAllAsync()
        {
            _readRepo = new Mock<IWebNewsReadRepository>();
            _writeRepo = new Mock<IWebNewsWriteRepository>();
            _env = new Mock<IWebHostEnvironment>();
            _accessor = new Mock<IHttpContextAccessor>();
            _fileHandler = new Mock<IFileUpload>();

            _service = new WebNewsService(_readRepo.Object, _writeRepo.Object, _env.Object, _accessor.Object, _fileHandler.Object);
        }

        [Fact]
        public async Task GetAllAsync_DtoIsNull_ReturnsAll()
        {
            var mockData = MockWebNews.CreateMockList();
            var asyncData = new MockDbSet.TestAsyncEnumerable<WebNews>(mockData);

            _readRepo.Setup(x => x.GetAll(false)).Returns(asyncData);

            var result = await _service.GetAllAsync(null);

            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Count);
            Assert.Equal("Test1", result.Value[0].Title);
            Assert.Equal("Test2", result.Value[1].Title);
            Assert.Equal("Test1", result.Value[0].Text);
            Assert.Equal("Test2", result.Value[1].Text);
        }


        [Fact]
        public async Task GetAllAsync_DtoWithCategoryAndDate_ReturnsFilteredData()
        {
            var filterDto = new WebNewsFilterDto
            {
                CreatedFrom = new DateTime(2024, 01, 01),
                CreatedTo = new DateTime(2024, 12, 31),
            };

            var filteredData = MockWebNews.CreateMockListWithFilterData();
            var asyncQueryable = new MockDbSet.TestAsyncEnumerable<WebNews>(filteredData);

            _readRepo.Setup(r => r.GetFilteredAsync(filterDto)).Returns(asyncQueryable);

            var result = await _service.GetAllAsync(filterDto);

            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Count);
        }

        [Fact]
        public async Task GetAllAsync_WhenNoDataAvailable_ReturnsEmptyList()
        {
            var filterDto = MockWebNewsDtos.CreateFilterDto();
            var emptyData = new List<WebNews>();
            var asyncQueryable = new MockDbSet.TestAsyncEnumerable<WebNews>(emptyData);

            _readRepo.Setup(r => r.GetFilteredAsync(filterDto)).Returns(asyncQueryable);

            var result = await _service.GetAllAsync(filterDto);

            Assert.True(result.IsSuccess);
            Assert.Empty(result.Value);
        }
    }
}
