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
    public class LessonFieldsTests_UpdateFieldAsync
    {
        ILessonFieldService _service;
        Mock<ILessonFieldReadRepository> _readRepo;
        Mock<ILessonFieldWriteRepository> _writeRepo;
        public LessonFieldsTests_UpdateFieldAsync()
        {
            _readRepo = new Mock<ILessonFieldReadRepository>();
            _writeRepo = new Mock<ILessonFieldWriteRepository>();
            _service = new LessonFieldService(_readRepo.Object, _writeRepo.Object);
        }

        [Fact]
        public async Task UpdateAsync_WhenFieldIsNull_ShouldReturnNotFound()
        {
            var id = Guid.NewGuid();
            var patchDto = MockLessonFieldDtos.CreateMockPatchDto();
            _readRepo.Setup(l => l.GetWhereAsync(f => f.Id == id && !f.IsDeleted, true)).ReturnsAsync((LessonField)null);

            var result = await _service.UpdateAsync(id, patchDto);
            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, lesson field is not found!", result.Error?.Message);
        }

        [Fact]
        public async Task UpdateAsync_WhenUpdateFails_ShouldReturnFailure()
        {
            var field = MockLessonField.CreateMockLessonFieldEntity();
            var dto = MockLessonFieldDtos.CreateMockPatchDto();
            _readRepo.Setup(r => r.GetWhereAsync(It.IsAny<Expression<Func<LessonField, bool>>>(), true)).ReturnsAsync(field);
            _writeRepo.Setup(l => l.Update(field)).Returns(false);

            var result = await _service.UpdateAsync(field.Id, dto);
            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, the operation 'update lesson field' failed!", result.Error?.Message);
        }

        [Fact]
        public async Task UpdateAsync_WhenSaveFails_ShouldReturnFailure()
        {
            var field = MockLessonField.CreateMockLessonFieldEntity();
            var dto = MockLessonFieldDtos.CreateMockPatchDto();

            _readRepo.Setup(r => r.GetWhereAsync(It.IsAny<Expression<Func<LessonField, bool>>>(), true))
                     .ReturnsAsync(field);

            _writeRepo.Setup(w => w.Update(field)).Returns(true);
            _writeRepo.Setup(w => w.SaveAsync()).ReturnsAsync(0);

            var result = await _service.UpdateAsync(field.Id, dto);
            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, the save lesson field failed!", result.Error?.Message);

            _readRepo.Verify(r => r.GetWhereAsync(It.IsAny<Expression<Func<LessonField, bool>>>(), true), Times.Once);
            _writeRepo.Verify(w => w.Update(field), Times.Once);
            _writeRepo.Verify(w => w.SaveAsync(), Times.Once);
        }


    }
}
