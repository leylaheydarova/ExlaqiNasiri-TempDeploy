using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.DTOs.Hadith;
using ExlaqiNasiri.Application.Repositories.Hadiths;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Mock.HelperMock;
using ExlaqiNasiri.Mock.MockDbModels;
using ExlaqiNasiri.Mock.MockDtos;
using ExlaqiNasiri.Persistence.Services;
using Moq;

namespace ExlaqiNasiri.Tests.Unit.Services.HadithTests
{
    public class HadithTests_GetAllAsync
    {
        IHadithService _service;
        Mock<IHadithReadRepository> _readRepo;
        Mock<IHadithWriteRepository> _writeRepo;

        public HadithTests_GetAllAsync()
        {
            _readRepo = new Mock<IHadithReadRepository>();
            _writeRepo = new Mock<IHadithWriteRepository>();
            _service = new HadithService(null, null, null, null, _readRepo.Object, _writeRepo.Object);
        }

        [Fact]
        public async Task GetAllAsync_DtoIsNull_ReturnsAll()
        {
            var mockData = MockHadith.CreateMockList();
            var asyncData = new MockDbSet.TestAsyncEnumerable<Hadith>(mockData);

            _readRepo.Setup(x => x.GetAll(false)).Returns(asyncData);

            var result = await _service.GetAllAsync(null);

            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Count);
            Assert.Equal("Test Hadith 1", result.Value[0].HadithContext);
            Assert.Equal("Test Hadith 2", result.Value[1].HadithContext);
        }

        [Fact]
        public async Task GetAllAsync_DtoWithCategoryAndDate_ReturnsFilteredData()
        {
            var categoryId = Guid.NewGuid();
            var filterDto = new HadithFilterDto
            {
                CreatedFrom = new DateTime(2024, 01, 01),
                CreatedTo = new DateTime(2024, 12, 31),
                CategoryId = categoryId,
                Source = "FilterSource"
            };

            var filteredData = MockHadith.CreateMockListWithFilterData(categoryId);
            var asyncQueryable = new MockDbSet.TestAsyncEnumerable<Hadith>(filteredData);

            _readRepo.Setup(r => r.GetFilteredAsync(filterDto)).Returns(asyncQueryable);

            var result = await _service.GetAllAsync(filterDto);

            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Count);
            Assert.All(result.Value, h =>
            {
                Assert.Equal("FilterSource", h.Source);
            });
        }

        [Fact]
        public async Task GetAllAsync_WhenNoDataAvailable_ReturnsEmptyList()
        {
            var filterDto = MockHadithDtos.CreateFilterDto();
            var emptyData = new List<Hadith>();
            var asyncQueryable = new MockDbSet.TestAsyncEnumerable<Hadith>(emptyData);

            _readRepo.Setup(r => r.GetFilteredAsync(filterDto)).Returns(asyncQueryable);

            var result = await _service.GetAllAsync(filterDto);

            Assert.True(result.IsSuccess);
            Assert.Empty(result.Value);
        }
    }
}
