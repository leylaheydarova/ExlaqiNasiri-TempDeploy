using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.Abstraction.Helper;
using ExlaqiNasiri.Application.Constants;
using ExlaqiNasiri.Application.DTOs.Article;
using ExlaqiNasiri.Application.Repositories.Articles;
using ExlaqiNasiri.Application.ResultPattern;
using ExlaqiNasiri.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ExlaqiNasiri.Persistence.Services
{
    public class ArticleService : IArticleService
    {
        readonly IArticleReadRepository _readRepo;
        readonly IArticleWriteRepository _writeRepo;
        readonly IWebHostEnvironment _env;
        readonly IHttpContextAccessor _accessor;
        readonly IFileUpload _fileUpload;

        public ArticleService(IArticleReadRepository readRepo, IArticleWriteRepository writeRepo, IWebHostEnvironment env, IHttpContextAccessor accessor, IFileUpload fileUpload)
        {
            _readRepo = readRepo;
            _writeRepo = writeRepo;
            _env = env;
            _accessor = accessor;
            _fileUpload = fileUpload;
        }

        public async Task<Result<bool>> CreateAsync(ArticleCommandDto dto)
        {
            Article article = new Article()
            {
                Id = Guid.NewGuid(),
                Text = dto.Text,
                Title = dto.Title,
                Author = dto.Author,
                ImageName = _fileUpload.UploadFile(dto.Image, _env.WebRootPath, FilePaths.ArticleImagePath)

            };
            article.ImageUrl = $"{_accessor.HttpContext.Request.Scheme}://{_accessor.HttpContext.Request.Host}/{FilePaths.ArticleImagePath}/{article.ImageName}";
            var result = await _writeRepo.AddAsync(article);
            if (!result) return Result<bool>.Failure(Error.OperationFailError("add article"));
            var savecount = await _writeRepo.SaveAsync();
            if (savecount == 0) return Result<bool>.Failure(Error.SaveFailError("article"));

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            var article = await _readRepo.GetByIdAsync(id, true);
            if (article == null) return Result<bool>.Failure(Error.NotFoundError("article"));

            var path = $"{_env.WebRootPath}/{FilePaths.ArticleImagePath}/{article.ImageName}";
            if (File.Exists(path)) File.Delete(path);

            var result = _writeRepo.RemovePermanently(article);
            if (!result) return Result<bool>.Failure(Error.OperationFailError("delete article"));

            var savecount = await _writeRepo.SaveAsync();
            if (savecount == 0) return Result<bool>.Failure(Error.SaveFailError("article"));

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> UpdateAsync(Guid id, ArticlePatchDto dto)
        {
            var article = await _readRepo.GetByIdAsync(id, true);

            if (article == null) return Result<bool>.Failure(Error.NotFoundError("article"));
            article.Text = dto.Text ?? article.Text;
            article.Author = dto.Author ?? article.Author;
            article.Title = dto.Title ?? article.Title;


            var result = _writeRepo.Update(article);
            if (!result) return Result<bool>.Failure(Error.OperationFailError("update article"));

            var savecount = await _writeRepo.SaveAsync();
            if (savecount == 0) return Result<bool>.Failure(Error.SaveFailError("article"));

            return Result<bool>.Success(true);
        }


        public async Task<Result<List<ArticleGetAllDto>>> GetAllAsync(ArticleFilterDto? dto)
        {
            var query = dto != null ? _readRepo.GetFilteredAsync(dto) : _readRepo.GetAll(false);
            query = query.AsQueryable().AsNoTracking();

            var dtos = new List<ArticleGetAllDto>();
            dtos = await query.Select(article => new ArticleGetAllDto
            {
                Id = article.Id.ToString(),
                Title = article.Title,
                Text = article.Text,
                ImageName = article.ImageName,
                ImageUrl = article.ImageUrl,
                PostDate = DateTime.UtcNow
            }).ToListAsync();

            return Result<List<ArticleGetAllDto>>.Success(dtos);
        }

        public async Task<Result<ArticleGetSingleDto>> GetSingleAsync(Guid id)
        {
            var article = await _readRepo.GetByIdAsync(id, true);
            if (article == null) return Result<ArticleGetSingleDto>.Failure(Error.NotFoundError("article"));
            var dto = new ArticleGetSingleDto
            {
                Id = article.Id.ToString(),
                ImageName = article.ImageName,
                ImageUrl = article.ImageUrl,
                Text = article.Text,
                Title = article.Title,
                Author = article.Author
            };

            return Result<ArticleGetSingleDto>.Success(dto);
        }
    }
}
