using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.DTOs.HadithCategory;
using ExlaqiNasiri.Application.Repositories.HadithCategories;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Persistence.Services;
using Moq;

namespace ExlaqiNasiri.Tests.Unit.Services.HadithCategoryTests
{
    public class HadithCategoryTests_CreateCategoryAsync
    {
        IHadithCategoryService _service;
        Mock<IHadithCategoryReadRepository> _mockReadRepo;
        Mock<IHadithCategoryWriteRepository> _mockWriteRepo;

        public HadithCategoryTests_CreateCategoryAsync()
        {
            _mockWriteRepo = new Mock<IHadithCategoryWriteRepository>();
            _mockReadRepo = new Mock<IHadithCategoryReadRepository>();
            _service = new HadithCategoryService(_mockWriteRepo.Object, _mockReadRepo.Object);
        }


        [Fact]
        public async Task CreateAsync_ValidDto_ReturnSuccess()
        {
            var dto = new HadithCategoryCommandDTO { CategoryName = "Test" };

            _mockWriteRepo.Setup(x => x.AddAsync(It.IsAny<HadithCategory>())).ReturnsAsync(true);
            _mockWriteRepo.Setup(x => x.SaveAsync()).ReturnsAsync(1);

            var result = await _service.CreateAsync(dto);
            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
        }

        [Fact]
        public async Task CreateAsync_AddFails_ReturnsFailure()
        {
            var dto = new HadithCategoryCommandDTO { CategoryName = "Test" };
            _mockWriteRepo.Setup(x => x.AddAsync(It.IsAny<HadithCategory>())).ReturnsAsync(false);

            var result = await _service.CreateAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, the operation 'add category' failed!", result.Error?.Message);
        }

        [Fact]
        public async Task CreateAsync_SaveFails_ReturnsFailure()
        {
            var dto = new HadithCategoryCommandDTO { CategoryName = "Test" };

            _mockWriteRepo.Setup(x => x.AddAsync(It.IsAny<HadithCategory>())).ReturnsAsync(true);
            _mockWriteRepo.Setup(x => x.SaveAsync()).ReturnsAsync(0);

            var result = await _service.CreateAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, the save category failed!", result.Error?.Message);
        }
    }
}
