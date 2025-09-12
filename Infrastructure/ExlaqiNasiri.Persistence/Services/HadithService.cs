using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.Abstraction.Helper;
using ExlaqiNasiri.Application.Constants;
using ExlaqiNasiri.Application.DTOs.Hadith;
using ExlaqiNasiri.Application.Repositories.Hadiths;
using ExlaqiNasiri.Application.ResultPattern;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ExlaqiNasiri.Persistence.Services
{
    public class HadithService : IHadithService
    {
        readonly IHadithReadRepository _readRepository;
        readonly IHadithWriteRepository _writeRepository;
        readonly IGetEntityService _getEntity;
        readonly IWebHostEnvironment _env;
        readonly IHttpContextAccessor _accessor;
        readonly IFileUpload _fileUpload;

        public HadithService(IGetEntityService getEntity, IHttpContextAccessor accessor, IWebHostEnvironment env, IFileUpload fileUpload, IHadithReadRepository readRepository, IHadithWriteRepository writeRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _getEntity = getEntity;
            _env = env;
            _accessor = accessor;
            _fileUpload = fileUpload;
        }

        public async Task<Result<bool>> CreateAsync(HadithCreateDto dto)
        {
            var category = await _getEntity.GetHadithCategoryAsync(dto.CategoryId, false);
            if (!category.IsSuccess) return Result<bool>.Failure(category.Error);
            Domain.Entities.Hadith hadith = new Domain.Entities.Hadith() //bele yazdim errora gore 
            {
                Id = Guid.NewGuid(),
                HadithContext = dto.HadithContext,
                Text = dto.Text,
                Source = dto.Source,
                ImageName = _fileUpload.UploadFile(dto.Image, _env.WebRootPath, FilePaths.HadithImagePath),
                CategoryId = category.Value.Id
            };
            hadith.ImageUrl = $"{_accessor.HttpContext.Request.Scheme}://{_accessor.HttpContext.Request.Host}/{FilePaths.HadithImagePath}/{hadith.ImageName}";
            var result = await _writeRepository.AddAsync(hadith);
            if (!result) return Result<bool>.Failure(Error.OperationFailError("add hadith"));
            var savecount = await _writeRepository.SaveAsync();
            if (savecount == 0) return Result<bool>.Failure(Error.SaveFailError("hadith"));

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            var hadith = await _readRepository.GetByIdAsync(id, true);
            if (hadith == null) return Result<bool>.Failure(Error.NotFoundError("hadith"));

            var path = $"{_env.WebRootPath}/{FilePaths.HadithImagePath}/{hadith.ImageName}";
            if (File.Exists(path)) File.Delete(path);

            var result = _writeRepository.RemovePermanently(hadith);
            if (!result) return Result<bool>.Failure(Error.OperationFailError("delete hadith"));

            var savecount = await _writeRepository.SaveAsync();
            if (savecount == 0) return Result<bool>.Failure(Error.SaveFailError("hadith"));

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> UpdateAsync(Guid id, HadithPatchDto dto)
        {
            var hadith = await _readRepository.GetByIdAsync(id, true);

            if (hadith == null) return Result<bool>.Failure(Error.NotFoundError("hadith"));
            hadith.Text = dto.Text ?? hadith.Text;
            hadith.HadithContext = dto.HadithContext ?? hadith.HadithContext;
            hadith.Source = dto.Source ?? hadith.Source;


            var result = _writeRepository.Update(hadith);
            if (!result) return Result<bool>.Failure(Error.OperationFailError("update hadith"));

            var savecount = await _writeRepository.SaveAsync();
            if (savecount == 0) return Result<bool>.Failure(Error.SaveFailError("hadith"));

            return Result<bool>.Success(true);
        }

        public async Task<Result<List<HadithGetAllDto>>> GetAllAsync(HadithFilterDto? dto)
        {
            var query = dto != null ? _readRepository.GetFilteredAsync(dto) : _readRepository.GetAll(false);
            query = query.AsQueryable().AsNoTracking();
            var dtos = new List<HadithGetAllDto>();
            dtos = await query.Select(hadith => new HadithGetAllDto
            {
                Id = hadith.Id.ToString(),
                HadithContext = hadith.HadithContext,
                Source = hadith.Source,
                ImageName = hadith.ImageName,
                ImageUrl = hadith.ImageUrl
            }).ToListAsync();

            return Result<List<HadithGetAllDto>>.Success(dtos);
        }

        public async Task<Result<HadithGetSingleDto>> GetSingleAsync(Guid id)
        {
            var hadith = await _readRepository.GetByIdAsync(id, false);
            if (hadith == null) return Result<HadithGetSingleDto>.Failure(Error.NotFoundError("hadith"));
            var dto = new HadithGetSingleDto
            {
                Id = hadith.Id.ToString(),
                HadithContext = hadith.HadithContext,
                Source = hadith.Source,
                ImageName = hadith.ImageName,
                ImageUrl = hadith.ImageUrl,
                Text = hadith.Text
            };

            return Result<HadithGetSingleDto>.Success(dto);
        }
    }
}
