using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.Repositories.LessonFields;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Mock.MockDbModels;
using ExlaqiNasiri.Mock.MockDtos;
using ExlaqiNasiri.Persistence.Services;
using Moq;
using System.Linq.Expressions;

namespace ExlaqiNasiri.Tests.Unit.Services.LessonFieldsTesting
{
    public class LessonFieldsTests_GetSingleFieldAsync
    {
        ILessonFieldService _service;
        Mock<ILessonFieldReadRepository> _readRepo;
        Mock<ILessonFieldWriteRepository> _writeRepo;
        public LessonFieldsTests_GetSingleFieldAsync()
        {
            _readRepo = new Mock<ILessonFieldReadRepository>();
            _writeRepo = new Mock<ILessonFieldWriteRepository>();
            _service = new LessonFieldService(_readRepo.Object, _writeRepo.Object);
        }

        [Fact]
        public async Task GetSingleAsync_WhenFieldIsNull_ShouldReturnFailure()
        {
            var id = Guid.NewGuid();
            _readRepo.Setup(l => l.GetWhereAsync(l => l.Id == id && !l.IsDeleted, true)).ReturnsAsync((LessonField)null);
            var result = await _service.GetSingleAsync(id);

            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, lesson field is not found!", result.Error.Message);
        }

        [Fact]
        public async Task GetSingleAsync_WhenIsDeletedIsTrue_ShouldReturnFailure()
        {
            var deletedFile = MockLessonField.CreateMockLessonField();
            _readRepo.Setup(l => l.GetWhereAsync(l => l.Id == deletedFile.Id && !l.IsDeleted, true)).ReturnsAsync((LessonField)null);

            var result = await _service.GetSingleAsync(deletedFile.Id);
            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, lesson field is not found!", result.Error.Message);
        }

        [Fact]
        public async Task GetSingleAsync_WhenFieldExists_ShouldReturnDto()
        {
            var field = MockLessonField.CreateMockLessonFieldEntity();
            var getDto = MockLessonFieldDtos.CreateMockGetDto();
            getDto.Id = field.Id.ToString();
            getDto.Field = field.LessonName;
            _readRepo.Setup(r => r.GetWhereAsync(It.IsAny<Expression<Func<LessonField, bool>>>(), true)).ReturnsAsync(field);

            var result = await _service.GetSingleAsync(field.Id);
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(getDto.Id, result.Value.Id);
            Assert.Equal(getDto.Field, result.Value.Field);
            Assert.True(result.Error == null || string.IsNullOrEmpty(result.Error.Message));
        }
    }
}
