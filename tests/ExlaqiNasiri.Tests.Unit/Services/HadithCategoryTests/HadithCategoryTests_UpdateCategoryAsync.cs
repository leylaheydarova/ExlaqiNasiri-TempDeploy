using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.DTOs.HadithCategory;
using ExlaqiNasiri.Application.Repositories.HadithCategories;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Mock.MockDtos;
using ExlaqiNasiri.Persistence.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Tests.Unit.Services.HadithCategoryTests
{
    public class HadithCategoryTests_UpdateCategoryAsync
    {
        IHadithCategoryService _service;
        Mock<IHadithCategoryReadRepository> _readRepo;
        Mock<IHadithCategoryWriteRepository> _writeRepo;

        public HadithCategoryTests_UpdateCategoryAsync()
        {
            _readRepo = new Mock<IHadithCategoryReadRepository>();
            _writeRepo = new Mock<IHadithCategoryWriteRepository>();
            _service = new HadithCategoryService(_writeRepo.Object, _readRepo.Object);
        }

        [Fact]
        public async Task UpdateAsync_ValidId_ReturnsSuccess()
        {
            var id = Guid.NewGuid();
            var category = new HadithCategory { Id = id, CategoryName = "Test 1" };
            var dto = MockHadithCategoryDtos.CreateValidCommandDto();

            _readRepo.Setup(x => x.GetWhereAsync(c => c.Id == id && !c.IsDeleted, true))
                     .ReturnsAsync(category);
            _writeRepo.Setup(x => x.Update(category)).Returns(true);
            _writeRepo.Setup(x => x.SaveAsync()).ReturnsAsync(1);

            var result = await _service.UpdateAsync(id, dto);

            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
            Assert.Equal(dto.CategoryName, category.CategoryName);
        }

        [Fact]
        public async Task UpdateAsync_CategoryNotFound_ReturnsFailure()
        {
            var id = Guid.NewGuid();
            var dto = MockHadithCategoryDtos.CreateEmptyCommandDto();

            _readRepo.Setup(x => x.GetWhereAsync(c => c.Id == id && !c.IsDeleted, true))
                     .ReturnsAsync((HadithCategory)null);

            var result = await _service.UpdateAsync(id, dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, category is not found!", result.Error?.Message);
        }

        [Fact]
        public async Task UpdateAsync_UpdateFails_ReturnsFailure()
        {
            var id = Guid.NewGuid();
            var category = new HadithCategory { Id = id, CategoryName = "Test 1" };
            var dto = MockHadithCategoryDtos.CreateValidCommandDto();

            _readRepo.Setup(x => x.GetWhereAsync(c => c.Id == id && !c.IsDeleted, true))
                     .ReturnsAsync(category);
            _writeRepo.Setup(x => x.Update(category)).Returns(false);

            var result = await _service.UpdateAsync(id, dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, the operation 'update category' failed!", result.Error?.Message);
        }

        [Fact]
        public async Task UpdateAsync_SaveFails_ReturnsFailure()
        {
            var id = Guid.NewGuid();
            var category = new HadithCategory { Id = id, CategoryName = "Test 1" };
            var dto = MockHadithCategoryDtos.CreateValidCommandDto();

            _readRepo.Setup(x => x.GetWhereAsync(c => c.Id == id && !c.IsDeleted, true))
                     .ReturnsAsync(category);
            _writeRepo.Setup(x => x.Update(category)).Returns(true);
            _writeRepo.Setup(x => x.SaveAsync()).ReturnsAsync(0);

            var result = await _service.UpdateAsync(id, dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, the save category failed!", result.Error?.Message);
        }
    }

}
