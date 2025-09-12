using ExlaqiNasiri.Application.DTOs.Article;
using ExlaqiNasiri.Application.ResultPattern;

namespace ExlaqiNasiri.Application.Abstraction
{
    public interface IArticleService
    {
        Task<Result<bool>> CreateAsync(ArticleCommandDto dto);
        Task<Result<bool>> DeleteAsync(Guid id);
        Task<Result<bool>> UpdateAsync(Guid id, ArticlePatchDto dto);
        Task<Result<List<ArticleGetAllDto>>> GetAllAsync(ArticleFilterDto? dto);
        Task<Result<ArticleGetSingleDto>> GetSingleAsync(Guid id);
    }
}
