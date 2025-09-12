using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.Repositories.HadithCategories;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Persistence.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Tests.Unit.Services.HadithCategoryTests
{
    public class HadithCategoryTests_GetSingleAsync
    {
        IHadithCategoryService _service;
        Mock<IHadithCategoryReadRepository> _readRepo;
        Mock<IHadithCategoryWriteRepository> _writeRepo;

        public HadithCategoryTests_GetSingleAsync()
        {
            _readRepo = new Mock<IHadithCategoryReadRepository>();
            _writeRepo = new Mock<IHadithCategoryWriteRepository>();
            _service = new HadithCategoryService(_writeRepo.Object, _readRepo.Object);
        }

        [Fact]
        public async Task GetSingleAsync_ValidId_ReturnsSuccess()
        {
            var id = Guid.NewGuid();
            var category = new HadithCategory { Id = id, CategoryName = "Test" };

            _readRepo.Setup(x => x.GetWhereAsync(c => c.Id == id && !c.IsDeleted, true))
                     .ReturnsAsync(category);

            var result = await _service.GetSingleAsync(id);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(id.ToString(), result.Value.Id);
            Assert.Equal("Test", result.Value.Name);
        }

        [Fact]
        public async Task GetSingleAsync_CategoryNotFound_ReturnsFailure()
        {
            var id = Guid.NewGuid();

            _readRepo.Setup(x => x.GetWhereAsync(c => c.Id == id && !c.IsDeleted, true))
                     .ReturnsAsync((HadithCategory)null);

            var result = await _service.GetSingleAsync(id);

            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, category is not found!", result.Error?.Message);
        }
    }
}
