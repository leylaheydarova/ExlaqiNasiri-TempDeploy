using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.Repositories.HadithCategories;
using ExlaqiNasiri.Application.Repositories.Hadiths;
using ExlaqiNasiri.Persistence.Repositories;
using ExlaqiNasiri.Persistence.Services;
using ExlaqiNasiri.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExlaqiNasiri.Application.Abstraction.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ExlaqiNasiri.Tests.Unit.Services.HadithTests
{
    public class HadithTests_DeleteHadithAsync
    {
        IHadithService _service;
        Mock<IHadithReadRepository> _readRepo;
        Mock<IHadithWriteRepository> _writeRepo;
        Mock<IHttpContextAccessor> _accessor;
        Mock<IWebHostEnvironment> _env;
        Mock<IFileUpload> _fileHandler;
        Mock<IGetEntityService> _getEntity;
        public HadithTests_DeleteHadithAsync()
        {
            _readRepo = new Mock<IHadithReadRepository>();
            _writeRepo = new Mock<IHadithWriteRepository>();
            _env = new Mock<IWebHostEnvironment>();
            _accessor = new Mock<IHttpContextAccessor>();
            _fileHandler = new Mock<IFileUpload>();
            _getEntity= new Mock<IGetEntityService>();
            _service = new HadithService(_getEntity.Object, _accessor.Object, _env.Object, _fileHandler.Object, _readRepo.Object, _writeRepo.Object);
        }

        [Fact]
        public async Task DeleteAsync_WhenHadithNotFound_ReturnsFailure()
        {
            _readRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), true)).ReturnsAsync((Hadith)null);

            var result = await _service.DeleteAsync(Guid.NewGuid());

            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, hadith is not found!", result.Error?.Message);
        }

        [Fact]
        public async Task DeleteAsync_WhenRemoveFails_ReturnsFailure()
        {
            var hadith = new Hadith { Id = Guid.NewGuid(), ImageName = "mock.png" };
            _readRepo.Setup(r => r.GetByIdAsync(hadith.Id, true)).ReturnsAsync(hadith);
            _writeRepo.Setup(r => r.RemovePermanently(hadith)).Returns(false);

            var result = await _service.DeleteAsync(hadith.Id);

            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, the operation 'delete hadith' failed!", result.Error?.Message);
        }

        [Fact]
        public async Task DeleteAsync_WhenSaveFails_ReturnsFailure()
        {
            var hadith = new Hadith { Id = Guid.NewGuid(), ImageName = "img.png" };
            _readRepo.Setup(r => r.GetByIdAsync(hadith.Id, true)).ReturnsAsync(hadith);
            _writeRepo.Setup(r => r.RemovePermanently(hadith)).Returns(true);
            _writeRepo.Setup(r => r.SaveAsync()).ReturnsAsync(0);

            var result = await _service.DeleteAsync(hadith.Id);

            Assert.False(result.IsSuccess);
            Assert.Equal("Sorry, the save hadith failed!", result.Error?.Message);
        }

        [Fact]
        public async Task DeleteAsync_WhenAllValid_ReturnsSuccess()
        {
            var hadith = new Hadith { Id = Guid.NewGuid(), ImageName = "img.png" };
            _readRepo.Setup(r => r.GetByIdAsync(hadith.Id, true)).ReturnsAsync(hadith);
            _writeRepo.Setup(r => r.RemovePermanently(hadith)).Returns(true);
            _writeRepo.Setup(r => r.SaveAsync()).ReturnsAsync(1);

            var result = await _service.DeleteAsync(hadith.Id);

            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
        }

    }
}
