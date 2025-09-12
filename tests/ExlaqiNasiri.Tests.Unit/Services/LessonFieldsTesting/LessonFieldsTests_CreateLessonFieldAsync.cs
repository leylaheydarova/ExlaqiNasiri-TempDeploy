using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.DTOs.LessonField;
using ExlaqiNasiri.Application.Repositories.LessonFields;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Mock.MockDtos;
using ExlaqiNasiri.Persistence.Services;
using Moq;

namespace ExlaqiNasiri.Tests.Unit.Services.LessonFieldsTesting
{
    public class LessonFieldsTests_CreateLessonFieldAsync
    {
        ILessonFieldService _lessonFieldService;
        Mock<ILessonFieldReadRepository> _readRepo;
        Mock<ILessonFieldWriteRepository> _writeRepo;
        LessonFieldCommandDto _dto;
        public LessonFieldsTests_CreateLessonFieldAsync()
        {
            _readRepo = new Mock<ILessonFieldReadRepository>();
            _writeRepo = new Mock<ILessonFieldWriteRepository>();
            _lessonFieldService = new LessonFieldService(_readRepo.Object, _writeRepo.Object);
            _dto = MockLessonFieldDtos.CreateMockCommandDto();
        }


        [Fact]
        public async Task CreateAsync_WhenDtoIsValid_ShouldReturnSuccess()
        {
            _writeRepo.Setup(x => x.AddAsync(It.Is<LessonField>(l => l.LessonName == _dto.LessonName))).ReturnsAsync(true);
            _writeRepo.Setup(x => x.SaveAsync()).ReturnsAsync(1);
            var result = await _lessonFieldService.CreateAsync(_dto);

            Assert.True(result.IsSuccess);
            Assert.True(result.Value);

            _writeRepo.Verify(x => x.AddAsync(It.IsAny<LessonField>()), Times.Once);
            _writeRepo.Verify(x => x.SaveAsync(), Times.Once);

        }

        [Fact]
        public async Task CreateAsync_WhenAddFails_ShouldReturnFailure()
        {
            _writeRepo.Setup(x => x.AddAsync(It.IsAny<LessonField>())).ReturnsAsync(false);

            var result = await _lessonFieldService.CreateAsync(_dto);
            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, the operation 'add lesson field' failed!", result.Error?.Message);

            _writeRepo.Verify(x => x.AddAsync(It.IsAny<LessonField>()), Times.Once);
            _writeRepo.Verify(x => x.SaveAsync(), Times.Never);
        }

        [Fact]
        public async Task CreateAsync_WhenSaveFails_ShouldReturnFailure()
        {
            _writeRepo.Setup(x => x.AddAsync(It.IsAny<LessonField>())).ReturnsAsync(true);
            _writeRepo.Setup(x => x.SaveAsync()).ReturnsAsync(0);

            var result = await _lessonFieldService.CreateAsync(_dto);
            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, the save lesson field failed!", result.Error?.Message);

            _writeRepo.Verify(x => x.AddAsync(It.IsAny<LessonField>()), Times.Once);
            _writeRepo.Verify(x => x.SaveAsync(), Times.Once);
        }
    }
}
