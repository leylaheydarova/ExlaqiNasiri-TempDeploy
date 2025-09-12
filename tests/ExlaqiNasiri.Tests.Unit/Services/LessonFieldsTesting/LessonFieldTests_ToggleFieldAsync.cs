using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.Repositories.LessonFields;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Mock.MockDbModels;
using ExlaqiNasiri.Persistence.Services;
using Moq;

namespace ExlaqiNasiri.Tests.Unit.Services.LessonFieldsTesting
{
    public class LessonFieldTests_ToggleFieldAsync
    {
        ILessonFieldService _service;
        Mock<ILessonFieldReadRepository> _readRepo;
        Mock<ILessonFieldWriteRepository> _writeRepo;
        public LessonFieldTests_ToggleFieldAsync()
        {
            _readRepo = new Mock<ILessonFieldReadRepository>();
            _writeRepo = new Mock<ILessonFieldWriteRepository>();
            _service = new LessonFieldService(_readRepo.Object, _writeRepo.Object);
        }

        [Fact]
        public async Task ToggleAsync_WhenFieldIsNull_ShouldReturnNotFoundFailure()
        {
            var id = Guid.NewGuid();
            _readRepo.Setup(l => l.GetByIdAsync(id, true)).ReturnsAsync((LessonField)null);

            var result = await _service.ToggleAsync(id);
            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, lesson field is not found!", result.Error?.Message);
        }

        [Fact]
        public async Task ToggleAsync_WhenToggleFails_ShouldReturnFailure()
        {
            var field = MockLessonField.CreateMockLessonFieldEntity();
            _readRepo.Setup(l => l.GetByIdAsync(field.Id, true)).ReturnsAsync(field);
            _writeRepo.Setup(l => l.Toggle(field)).Returns(false);

            var result = await _service.ToggleAsync(field.Id);
            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, the operation 'delete lesson field' failed!", result.Error?.Message);

            _writeRepo.Verify(x => x.Toggle(It.IsAny<LessonField>()), Times.Once);
            _writeRepo.Verify(x => x.SaveAsync(), Times.Never);
        }

        [Fact]
        public async Task ToggleAsync_WhenSaveFails_ShouldReturnFailure()
        {
            var field = MockLessonField.CreateMockLessonFieldEntity();
            _readRepo.Setup(l => l.GetByIdAsync(field.Id, true)).ReturnsAsync(field);
            _writeRepo.Setup(l => l.Toggle(field)).Returns(true);
            _writeRepo.Setup(l => l.SaveAsync()).ReturnsAsync(0);

            var result = await _service.ToggleAsync(field.Id);
            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, the save lesson field failed!", result.Error?.Message);
        }

        [Fact]
        public async Task ToggleAsync_WhenToggleSuccessful_ShouldReturnSuccess()
        {
            var field = MockLessonField.CreateMockLessonFieldEntity();
            _readRepo.Setup(l => l.GetByIdAsync(field.Id, true)).ReturnsAsync(field);
            _writeRepo.Setup(l => l.Toggle(field)).Returns(true);
            _writeRepo.Setup(l => l.SaveAsync()).ReturnsAsync(1);

            var result = await _service.ToggleAsync(field.Id);
            Assert.True(result.IsSuccess);
            Assert.Equal(field.IsDeleted, result.Value);
            Assert.True(result.Error == null || string.IsNullOrEmpty(result.Error.Message));
        }
    }
}
