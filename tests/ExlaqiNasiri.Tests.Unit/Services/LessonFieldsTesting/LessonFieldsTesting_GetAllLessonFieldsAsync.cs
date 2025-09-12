using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.DTOs.LessonField;
using ExlaqiNasiri.Application.Repositories.LessonFields;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Mock.HelperMock;
using ExlaqiNasiri.Mock.MockDbModels;
using ExlaqiNasiri.Mock.MockDtos;
using ExlaqiNasiri.Persistence.Services;
using Moq;

namespace ExlaqiNasiri.Tests.Unit.Services.LessonFieldsTesting
{
    public class LessonFieldsTesting_GetAllLessonFieldsAsync
    {
        ILessonFieldService _service;
        Mock<ILessonFieldReadRepository> _readRepo;
        Mock<ILessonFieldWriteRepository> _writeRepo;
        public LessonFieldsTesting_GetAllLessonFieldsAsync()
        {
            _readRepo = new Mock<ILessonFieldReadRepository>();
            _writeRepo = new Mock<ILessonFieldWriteRepository>();
            _service = new LessonFieldService(_readRepo.Object, _writeRepo.Object);
        }

        [Fact]
        public async Task GetAllAsync_WhenDtoIsNull_ShouldReturnAll()
        {
            var mockData = MockLessonField.CreateMockQuery();
            var asyncData = new MockDbSet.TestAsyncEnumerable<LessonField>(mockData);
            _readRepo.Setup(l => l.GetAll(false)).Returns(asyncData);

            var result = await _service.GetAllAsync(null);

            _readRepo.Verify(r => r.GetAll(false), Times.Once);
            _readRepo.Verify(r => r.GetFilteredAsync(It.IsAny<LessonFieldFilterDto>()), Times.Never);

            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Count);
            Assert.Equal("Test1", result.Value[0].Field);
            Assert.Equal("Test2", result.Value[1].Field);
        }

        [Fact]
        public async Task GetAllAsync_WhenDtoIsNotNull_ShouldReturnFilteredData()
        {
            var filteredDto = MockLessonFieldDtos.CreateMockFilterDto();
            var filteredData = MockLessonField.CreateMockQueryWithFilterData();
            var asyncData = new MockDbSet.TestAsyncEnumerable<LessonField>(filteredData);
            _readRepo.Setup(l => l.GetFilteredAsync(filteredDto)).Returns(asyncData);

            var result = await _service.GetAllAsync(filteredDto);

            _readRepo.Verify(r => r.GetFilteredAsync(filteredDto), Times.Once);
            _readRepo.Verify(r => r.GetAll(It.IsAny<bool>()), Times.Never);

            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Count);
            Assert.Contains(result.Value, l => l.Field == "FilteredTest1");
            Assert.Contains(result.Value, l => l.Field == "FilteredTest2");
        }

        [Fact]
        public async Task GetAllAsync_WhenNoDataAvailable_ShouldReturnEmptyList()
        {
            var filterDto = MockLessonFieldDtos.CreateMockFilterDto();
            var emptyData = new List<LessonField>();
            var asyncData = new MockDbSet.TestAsyncEnumerable<LessonField>(emptyData);

            _readRepo.Setup(l => l.GetFilteredAsync(filterDto)).Returns(asyncData);
            var result = await _service.GetAllAsync(filterDto);

            _readRepo.Verify(r => r.GetFilteredAsync(filterDto), Times.Once);
            _readRepo.Verify(r => r.GetAll(It.IsAny<bool>()), Times.Never);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Empty(result.Value);
        }
    }
}
