using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.Repositories.HadithCategories;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Mock.HelperMock;
using ExlaqiNasiri.Mock.MockDbModels;
using ExlaqiNasiri.Mock.MockDtos;
using ExlaqiNasiri.Persistence.Services;
using Moq;

namespace ExlaqiNasiri.Tests.Unit.Services.HadithCategoryTests
{
    public class HadithCategoryTests_GetAllAsync
    {
        IHadithCategoryService _service;
        Mock<IHadithCategoryReadRepository> _readRepo;
        Mock<IHadithCategoryWriteRepository> _writeRepo;
        public HadithCategoryTests_GetAllAsync()
        {
            _readRepo = new Mock<IHadithCategoryReadRepository>();
            _writeRepo = new Mock<IHadithCategoryWriteRepository>();
            _service = new HadithCategoryService(_writeRepo.Object, _readRepo.Object);
        }

        [Fact]
        public async Task GetAllAsync_DtoIsNull_ReturnsAll()
        {
            var mockData = MockHadithCategory.CreateMockQuery();
            var asyncData = new MockDbSet.TestAsyncEnumerable<HadithCategory>(mockData);
            _readRepo.Setup(x => x.GetAll(false)).Returns(asyncData);

            var result = await _service.GetAllAsync(null);

            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Count);
            Assert.Equal("Test1", result.Value[0].Name);
            Assert.Equal("Test2", result.Value[1].Name);
        }

        [Fact]
        public async Task GetAllAsync_DtoIsNotNull_ReturnsFilteredData()
        {
            var filterDto = MockHadithCategoryDtos.CreateFilterDto();
            var filteredData = MockHadithCategory.CreateMockQueryWithFilterData();
            var asyncQueryable = new MockDbSet.TestAsyncEnumerable<HadithCategory>(filteredData);

            _readRepo.Setup(r => r.GetFilteredAsync(filterDto)).Returns(asyncQueryable);

            var result = await _service.GetAllAsync(filterDto);

            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Count);
            Assert.Contains(result.Value, c => c.Name == "Filtered Category");
            Assert.Contains(result.Value, c => c.Name == "Other Category");
        }

        [Fact]
        public async Task GetAllAsync_WhenNoDataAvailable_ReturnsEmptyList()
        {
            var filterDto = MockHadithCategoryDtos.CreateFilterDto();
            var emptyData = new List<HadithCategory>();
            var asyncQueryable = new MockDbSet.TestAsyncEnumerable<HadithCategory>(emptyData);

            _readRepo.Setup(r => r.GetFilteredAsync(filterDto)).Returns(asyncQueryable);

            var result = await _service.GetAllAsync(filterDto);

            Assert.True(result.IsSuccess);
            Assert.Empty(result.Value);
        }
    }
}
