using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.Repositories.LessonFields;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Mock.MockDbModels;
using ExlaqiNasiri.Persistence.Services;
using Moq;

namespace ExlaqiNasiri.Tests.Unit.Services.LessonFieldsTesting
{
    public class LessonFieldsTests_DeleteLessonFieldAsync
    {
        ILessonFieldService _lessonFieldService;
        Mock<ILessonFieldReadRepository> _readRepo;
        Mock<ILessonFieldWriteRepository> _writeRepo;
        LessonField _field;
        Guid _id;
        public LessonFieldsTests_DeleteLessonFieldAsync()
        {
            _readRepo = new Mock<ILessonFieldReadRepository>();
            _writeRepo = new Mock<ILessonFieldWriteRepository>();
            _lessonFieldService = new LessonFieldService(_readRepo.Object, _writeRepo.Object);
            _field = MockLessonField.CreateMockEntity();
            _id = Guid.NewGuid();
        }

        [Fact]
        public async Task DeleteAsync_WhenIdIsValid_ShouldReturnSuccess()
        {
            _readRepo.Setup(x => x.GetByIdAsync(_field.Id, true)).ReturnsAsync(_field);
            _writeRepo.Setup(x => x.RemovePermanently(_field)).Returns(true);
            _writeRepo.Setup(x => x.SaveAsync()).ReturnsAsync(1);

            var result = await _lessonFieldService.DeleteAsync(_field.Id);
            Assert.True(result.IsSuccess);
            Assert.True(result.Value);

            _readRepo.Verify(x => x.GetByIdAsync(_field.Id, true), Times.Once);
            _writeRepo.Verify(x => x.RemovePermanently(_field), Times.Once);
            _writeRepo.Verify(x => x.SaveAsync(), Times.Once);

        }

        [Fact]
        public async Task DeleteAsync_WhenFieldIsNull_ShouldReturnFailure()
        {
            _readRepo.Setup(x => x.GetByIdAsync(_id, true)).ReturnsAsync((LessonField?)null);

            var result = await _lessonFieldService.DeleteAsync(_id);
            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, lesson field is not found!", result.Error?.Message);


            _readRepo.Verify(x => x.GetByIdAsync(_id, true), Times.Once);
            _writeRepo.Verify(x => x.RemovePermanently(It.IsAny<LessonField>()), Times.Never);
            _writeRepo.Verify(x => x.SaveAsync(), Times.Never);
        }

        [Fact]
        public async Task DeleteAsync_WhenRemoveFails_ShouldReturnFailure()
        {
            _readRepo.Setup(x => x.GetByIdAsync(_field.Id, true)).ReturnsAsync(_field);
            _writeRepo.Setup(x => x.RemovePermanently(_field)).Returns(false);

            var result = await _lessonFieldService.DeleteAsync(_field.Id);

            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, the operation 'delete lesson field' failed!", result.Error?.Message);

            _readRepo.Verify(x => x.GetByIdAsync(_field.Id, true), Times.Once);
            _writeRepo.Verify(x => x.RemovePermanently(_field), Times.Once);
            _writeRepo.Verify(x => x.SaveAsync(), Times.Never);

        }

        [Fact]
        public async Task DeleteAsync_WhenSaveFails_ShouldReturnFailure()
        {
            _readRepo.Setup(x => x.GetByIdAsync(_field.Id, true)).ReturnsAsync(_field);
            _writeRepo.Setup(x => x.RemovePermanently(_field)).Returns(true);
            _writeRepo.Setup(x => x.SaveAsync()).ReturnsAsync(0);

            var result = await _lessonFieldService.DeleteAsync(_field.Id);
            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, the save lesson field failed!", result.Error?.Message);

            _readRepo.Verify(x => x.GetByIdAsync(_field.Id, true), Times.Once);
            _writeRepo.Verify(x => x.RemovePermanently(_field), Times.Once);
            _writeRepo.Verify(x => x.SaveAsync(), Times.Once);
        }
    }
}
