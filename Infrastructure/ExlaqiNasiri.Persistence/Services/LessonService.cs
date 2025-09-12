using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.Abstraction.Helper;
using ExlaqiNasiri.Application.Constants;
using ExlaqiNasiri.Application.DTOs.Lesson;
using ExlaqiNasiri.Application.Repositories.Lessons;
using ExlaqiNasiri.Application.ResultPattern;
using ExlaqiNasiri.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ExlaqiNasiri.Persistence.Services
{
    public class LessonService : ILessonService
    {
        readonly IGetEntityService _getEntityService;
        readonly IFileUpload _uploadFile;
        readonly IWebHostEnvironment _env;
        readonly IHttpContextAccessor _accessor;
        readonly ILessonWriteRepository _writeRepository;
        readonly ILessonReadRepository _readRepository;

        public LessonService(IGetEntityService getEntityService, IFileUpload uploadFile, IWebHostEnvironment env, IHttpContextAccessor accessor, ILessonWriteRepository writeRepository, ILessonReadRepository readRepository)
        {
            _getEntityService = getEntityService;
            _uploadFile = uploadFile;
            _env = env;
            _accessor = accessor;
            _writeRepository = writeRepository;
            _readRepository = readRepository;
        }

        public async Task<Result<bool>> CreateAsync(LessonCreateDto dto)
        {
            var field = await _getEntityService.GetLessonFieldAsync(dto.LessonFieldId, false);
            if (!field.IsSuccess) return Result<bool>.Failure(field.Error);
            Lesson lesson = new Lesson()
            {
                Id = Guid.NewGuid(),
                Text = dto.Text,
                Title = dto.Title,
                ImageName = _uploadFile.UploadFile(dto.Image, _env.WebRootPath, FilePaths.LessonImagePath),
                LessonFieldId = field.Value.Id
            };
            lesson.ImageUrl = $"{_accessor.HttpContext.Request.Scheme}://{_accessor.HttpContext.Request.Host}/{FilePaths.LessonImagePath}/{lesson.ImageName}";
            var result = await _writeRepository.AddAsync(lesson);
            if (!result) return Result<bool>.Failure(Error.OperationFailError("add lesson"));
            var saveCount = await _writeRepository.SaveAsync();
            if (saveCount == 0) return Result<bool>.Failure(Error.SaveFailError("lesson"));

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            var lesson = await _readRepository.GetByIdAsync(id, true);
            if (lesson == null) return Result<bool>.Failure(Error.NotFoundError("lesson"));

            var path = $"{_env.WebRootPath}/{FilePaths.LessonImagePath}/{lesson.ImageName}";
            if (File.Exists(path)) File.Delete(path);

            var result = _writeRepository.RemovePermanently(lesson);
            if (!result) return Result<bool>.Failure(Error.OperationFailError("delete lesson"));

            var saveCount = await _writeRepository.SaveAsync();
            if (saveCount == 0) return Result<bool>.Failure(Error.SaveFailError("lesson"));

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> UpdateAsync(Guid id, LessonUpdateDto dto)
        {
            if (dto == null)
                return Result<bool>.Failure(Error.NotFoundError("dto"));

            var lesson = await _readRepository.GetByIdAsync(id, true);
            if (lesson == null) return Result<bool>.Failure(Error.NotFoundError("lesson"));
            lesson.Text = dto.Text != null ? dto.Text : lesson.Text;
            lesson.Title = dto.Title != null ? dto.Title : lesson.Title;

            var path = $"{_env.WebRootPath}/{FilePaths.LessonImagePath}/{lesson.ImageName}";
            if (File.Exists(path)) File.Delete(path);

            lesson.ImageName = _uploadFile.UploadFile(dto.Image, _env.WebRootPath, FilePaths.LessonImagePath);
            lesson.ImageUrl = $"{_accessor.HttpContext.Request.Scheme}://{_accessor.HttpContext.Request.Host}/{FilePaths.LessonImagePath}/{lesson.ImageUrl}";

            var result = _writeRepository.Update(lesson);
            if (!result) return Result<bool>.Failure(Error.OperationFailError("update lesson"));

            var saveCount = await _writeRepository.SaveAsync();
            if (saveCount == 0) return Result<bool>.Failure(Error.SaveFailError("lesson"));

            return Result<bool>.Success(true);
        }

        public async Task<Result<List<LessonGetAllDto>>> GetAllAsync(LessonFilterDto? dto)
        {
            var query = dto != null ? _readRepository.GetFilteredAsync(dto) : _readRepository.GetAll(false).AsNoTracking();

            var dtos = new List<LessonGetAllDto>();
            dtos = await query.Select(lesson => new LessonGetAllDto
            {
                Id = lesson.Id,
                Title = lesson.Title,
                Text = lesson.Text,
                ImageName = lesson.ImageName,
                ImageUrl = lesson.ImageUrl
            }).ToListAsync();
            return Result<List<LessonGetAllDto>>.Success(dtos);
        }

        public async Task<Result<LessonGetSingleDto>> GetSingleAsync(Guid id)
        {
            var lesson = await _readRepository.GetByIdAsync(id, true);
            if (lesson == null) return Result<LessonGetSingleDto>.Failure(Error.NotFoundError("lesson"));
            var dto = new LessonGetSingleDto()
            {
                Id = lesson.Id.ToString(),
                ImageName = lesson.ImageName,
                ImageUrl = lesson.ImageUrl,
                Text = lesson.Text,
                Title = lesson.Title
            };
            return Result<LessonGetSingleDto>.Success(dto);
        }


    }
}
