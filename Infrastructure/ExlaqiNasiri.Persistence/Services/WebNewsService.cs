using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.Abstraction.Helper;
using ExlaqiNasiri.Application.Constants;
using ExlaqiNasiri.Application.DTOs.WebNews;
using ExlaqiNasiri.Application.Repositories.News;
using ExlaqiNasiri.Application.ResultPattern;
using ExlaqiNasiri.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ExlaqiNasiri.Persistence.Services
{
    public class WebNewsService : IWebNewsService
    {
        readonly IWebNewsReadRepository _readRepo;
        readonly IWebNewsWriteRepository _writeRepo;
        readonly IWebHostEnvironment _env;
        readonly IHttpContextAccessor _accessor;
        readonly IFileUpload _fileUpload;
        public WebNewsService(IWebNewsReadRepository readRepo, IWebNewsWriteRepository writeRepo, IWebHostEnvironment env, IHttpContextAccessor accessor, IFileUpload fileUpload)
        {
            _readRepo = readRepo;
            _writeRepo = writeRepo;
            _env = env;
            _accessor = accessor;
            _fileUpload = fileUpload;
        }

        public async Task<Result<bool>> CreateAsync(WebNewsCommandDto dto)
        {
            WebNews news = new WebNews()
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Text = dto.Text,
                ImageName = _fileUpload.UploadFile(dto.Image, _env.WebRootPath, FilePaths.WebNewsImagePath),
            };
            news.ImageUrl = $"{_accessor.HttpContext.Request.Scheme}://{_accessor.HttpContext.Request.Host}/{FilePaths.WebNewsImagePath}/{news.ImageName}";
            var result = await _writeRepo.AddAsync(news);
            if (!result) return Result<bool>.Failure(Error.OperationFailError("add news"));
            var savecount = await _writeRepo.SaveAsync();
            if (savecount == 0) return Result<bool>.Failure(Error.SaveFailError("news"));

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            var news = await _readRepo.GetByIdAsync(id, true);
            if (news == null) return Result<bool>.Failure(Error.NotFoundError("news"));

            var path = $"{_env.WebRootPath}/{FilePaths.WebNewsImagePath}/{news.ImageName}";
            if (File.Exists(path)) File.Delete(path);

            var result = _writeRepo.RemovePermanently(news);
            if (!result) return Result<bool>.Failure(Error.OperationFailError("delete news"));

            var savecount = await _writeRepo.SaveAsync();
            if (savecount == 0) return Result<bool>.Failure(Error.SaveFailError("news"));

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> UpdateAsync(Guid id, WebNewsPatchDto dto)
        {
            var news = await _readRepo.GetByIdAsync(id, true);

            if (news == null) return Result<bool>.Failure(Error.NotFoundError("news"));
            news.Text = dto.Text ?? news.Text;
            news.Title = dto.Title ?? news.Title;


            var result = _writeRepo.Update(news);
            if (!result) return Result<bool>.Failure(Error.OperationFailError("update news"));

            var savecount = await _writeRepo.SaveAsync();
            if (savecount == 0) return Result<bool>.Failure(Error.SaveFailError("news"));

            return Result<bool>.Success(true);
        }

        public async Task<Result<List<WebNewsGetAllDto>>> GetAllAsync(WebNewsFilterDto? dto)
        {
            var query = dto != null ? _readRepo.GetFilteredAsync(dto) : _readRepo.GetAll(false);
            query = query.AsQueryable().AsNoTracking();
            var dtos = new List<WebNewsGetAllDto>();
            dtos = await query.Select(news => new WebNewsGetAllDto
            {
                Id = news.Id.ToString(),
                Title = news.Title,
                Text = news.Text,
                PostDate = DateTime.UtcNow
            }).ToListAsync();

            return Result<List<WebNewsGetAllDto>>.Success(dtos);
        }

        public async Task<Result<WebNewsGetSingleDto>> GetSingleAsync(Guid id)
        {
            var news = await _readRepo.GetByIdAsync(id, true);
            if (news == null) return Result<WebNewsGetSingleDto>.Failure(Error.NotFoundError("news"));
            var dto = new WebNewsGetSingleDto
            {
                Id = news.Id.ToString(),
                Title = news.Title,
                Text = news.Text,
                ImageName = news.ImageName,
                ImageUrl = news.ImageUrl,
            };

            return Result<WebNewsGetSingleDto>.Success(dto);
        }



    }
}
