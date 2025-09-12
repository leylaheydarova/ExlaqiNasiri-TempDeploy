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
    public class HadithCategoryTests_ToggleCategoryAsync
    {
        IHadithCategoryService _service;
        Mock<IHadithCategoryReadRepository> _readRepo;
        Mock<IHadithCategoryWriteRepository> _writeRepo;

        public HadithCategoryTests_ToggleCategoryAsync()
        {
            _readRepo = new Mock<IHadithCategoryReadRepository>();
            _writeRepo = new Mock<IHadithCategoryWriteRepository>();
            _service = new HadithCategoryService(_writeRepo.Object, _readRepo.Object);
        }

        [Fact]
        public async Task ToggleAsync_ValidId_ReturnsSuccess()
        {
            var category = new HadithCategory { Id = Guid.NewGuid(), CategoryName = "Test" };
            _readRepo.Setup(x => x.GetByIdAsync(category.Id, true)).ReturnsAsync(category);
            _writeRepo.Setup(x => x.Toggle(category)).Returns(true);
            _writeRepo.Setup(x => x.SaveAsync()).ReturnsAsync(1);

            var result = await _service.ToggleAsync(category.Id);
            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
        }

        [Fact]
        public async Task ToggleAsync_CategoryNotFound_ReturnsFailure()
        {
            var id = Guid.NewGuid();
            _readRepo.Setup(x => x.GetByIdAsync(id, true)).ReturnsAsync((HadithCategory)null);
            var result = await _service.ToggleAsync(id);
            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, category is not found!", result.Error?.Message);
        }

        [Fact]
        public async Task DeleteAsync_RemoveFails_ReturnsFailure()
        {
            var category = new HadithCategory { Id = Guid.NewGuid(), CategoryName = "Test" };
            _readRepo.Setup(x => x.GetByIdAsync(category.Id, true)).ReturnsAsync(category);
            _writeRepo.Setup(x => x.Toggle(category)).Returns(false);

            var result = await _service.ToggleAsync(category.Id);
            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, the operation 'delete category' failed!", result.Error?.Message);
        }

        [Fact]
        public async Task DeleteAsync_SaveFails_ReturnsFailure()
        {
            var category = new HadithCategory { Id = Guid.NewGuid(), CategoryName = "Test" };
            _readRepo.Setup(x => x.GetByIdAsync(category.Id, true)).ReturnsAsync(category);
            _writeRepo.Setup(x => x.Toggle(category)).Returns(true);
            _writeRepo.Setup(x => x.SaveAsync()).ReturnsAsync(0);

            var result = await _service.ToggleAsync(category.Id);
            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, the save category failed!", result.Error?.Message);

        }
    }
}

