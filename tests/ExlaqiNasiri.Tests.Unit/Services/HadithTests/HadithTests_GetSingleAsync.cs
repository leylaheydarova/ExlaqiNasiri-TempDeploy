using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.Abstraction.Helper;
using ExlaqiNasiri.Application.DTOs.Hadith;
using ExlaqiNasiri.Application.Repositories.Hadiths;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Mock.MockDbModels;
using ExlaqiNasiri.Persistence.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace ExlaqiNasiri.Tests.Unit.Services.HadithTests
{
    public class HadithTests_GetSingleAsync
    {
        private readonly IHadithService _service;
        private readonly Mock<IHadithReadRepository> _readRepo;
        private readonly Mock<IHadithWriteRepository> _writeRepo;
        Mock<IHttpContextAccessor> _accessor;
        Mock<IWebHostEnvironment> _env;
        Mock<IFileUpload> _fileHandler;
        Mock<IGetEntityService> _getEntity;

        public HadithTests_GetSingleAsync()
        {
            _readRepo = new Mock<IHadithReadRepository>();
            _writeRepo = new Mock<IHadithWriteRepository>();
            _env = new Mock<IWebHostEnvironment>();
            _accessor = new Mock<IHttpContextAccessor>();
            _fileHandler = new Mock<IFileUpload>();
            _getEntity = new Mock<IGetEntityService>();

            _service = new HadithService(_getEntity.Object, _accessor.Object, _env.Object, _fileHandler.Object, _readRepo.Object, _writeRepo.Object);
        }

        [Fact]
        public async Task GetSingleAsync_WhenHadithExists_ReturnsSuccessWithDto()
        {
            var mockHadith = MockHadith.CreateMockEntity();
            _readRepo.Setup(r => r.GetWhereAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Hadith, bool>>>(), true))
                     .ReturnsAsync(mockHadith);

            var result = await _service.GetSingleAsync(mockHadith.Id);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(mockHadith.Id.ToString(), result.Value.Id);
            Assert.Equal(mockHadith.Text, result.Value.Text);
            Assert.Equal(mockHadith.Source, result.Value.Source);
            Assert.Equal(mockHadith.ImageName, result.Value.ImageName);
            Assert.Equal(mockHadith.ImageUrl, result.Value.ImageUrl);
        }

        [Fact]
        public async Task GetSingleAsync_WhenHadithDoesNotExist_ReturnsFailure()
        {
            _readRepo.Setup(r => r.GetWhereAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Hadith, bool>>>(), true))
                     .ReturnsAsync((Hadith?)null);
                                
            var result = await _service.GetSingleAsync(Guid.NewGuid());

            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Error);
            Assert.Equal("Sorry, hadith is not found!", result.Error.Message);
        }


        [Fact]
        public async Task GetSingleAsync_MapsAllPropertiesCorrectly()
        {
            var mockHadith = new Hadith
            {
                Id = Guid.NewGuid(),
                HadithContext = "Context test",
                Text = "Text test",
                Source = "Source test",
                ImageName = "image.png",
                ImageUrl = "urlimage.png"
            };

            _readRepo.Setup(r => r.GetWhereAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Hadith, bool>>>(), true))
                     .ReturnsAsync(mockHadith);

            var result = await _service.GetSingleAsync(mockHadith.Id);

            Assert.True(result.IsSuccess);
            Assert.Equal(mockHadith.HadithContext, result.Value.HadithContext);
            Assert.Equal(mockHadith.Text, result.Value.Text);
            Assert.Equal(mockHadith.Source, result.Value.Source);
            Assert.Equal(mockHadith.ImageName, result.Value.ImageName);
            Assert.Equal(mockHadith.ImageUrl, result.Value.ImageUrl);
        }
    }
}
