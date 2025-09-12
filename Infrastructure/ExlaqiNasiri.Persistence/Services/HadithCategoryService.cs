using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.DTOs.HadithCategory;
using ExlaqiNasiri.Application.Repositories.HadithCategories;
using ExlaqiNasiri.Application.ResultPattern;
using ExlaqiNasiri.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExlaqiNasiri.Persistence.Services
{
    public class HadithCategoryService : IHadithCategoryService
    {
        readonly IHadithCategoryWriteRepository _writeRepository;
        readonly IHadithCategoryReadRepository _readRepository;
        public HadithCategoryService(IHadithCategoryWriteRepository writeRepository, IHadithCategoryReadRepository readRepository)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
        }

        public async Task<Result<bool>> CreateAsync(HadithCategoryCommandDTO dto)
        {
            var category = new HadithCategory()
            {
                CategoryName = dto.CategoryName,
                Id = Guid.NewGuid()
            };

            var result = await _writeRepository.AddAsync(category);
            if (!result) return Result<bool>.Failure(Error.OperationFailError("add category"));

            var savecount = await _writeRepository.SaveAsync();
            if (savecount == 0) return Result<bool>.Failure(Error.SaveFailError("category"));

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            var category = await _readRepository.GetByIdAsync(id, true);
            if (category == null) return Result<bool>.Failure(Error.NotFoundError("category"));


            var result = _writeRepository.RemovePermanently(category);
            if (!result) return Result<bool>.Failure(Error.OperationFailError("delete category"));

            var savecount = await _writeRepository.SaveAsync();
            if (savecount == 0) return Result<bool>.Failure(Error.SaveFailError("category"));

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> ToggleAsync(Guid id)
        {
            var category = await _readRepository.GetByIdAsync(id, true);
            if (category == null) return Result<bool>.Failure(Error.NotFoundError("category"));

            var result = _writeRepository.Toggle(category);
            if (!result) return Result<bool>.Failure(Error.OperationFailError("delete category"));

            var savecount = await _writeRepository.SaveAsync();
            if (savecount == 0) return Result<bool>.Failure(Error.SaveFailError("category"));

            return Result<bool>.Success(category.IsDeleted);
        }

        public async Task<Result<bool>> UpdateAsync(Guid id, HadithCategoryCommandDTO dto)
        {
            var category = await _readRepository.GetWhereAsync(c => c.Id == id && !c.IsDeleted, true);
            if (category == null) return Result<bool>.Failure(Error.NotFoundError("category"));
            category.CategoryName = dto.CategoryName;

            var result = _writeRepository.Update(category);
            if (!result) return Result<bool>.Failure(Error.OperationFailError("update category"));

            var savecount = await _writeRepository.SaveAsync();
            if (savecount == 0) return Result<bool>.Failure(Error.SaveFailError("category"));

            return Result<bool>.Success(true);
        }

        //getall - filter - istese filterleyir, istemese hamisi gelir
        public async Task<Result<List<HadithCategoryGetDto>>> GetAllAsync(HadithCategoryFilterDto? dto)
        {
            var query = dto != null ? _readRepository.GetFilteredAsync(dto) : _readRepository.GetAll(false);
            query = query.AsQueryable().AsNoTracking();

            var dtos = new List<HadithCategoryGetDto>();
            dtos = await query.Select(category => new HadithCategoryGetDto
            {
                Id = category.Id.ToString(),
                Name = category.CategoryName
            }).ToListAsync();

            return Result<List<HadithCategoryGetDto>>.Success(dtos);
        }

        public async Task<Result<HadithCategoryGetDto>> GetSingleAsync(Guid id)
        {
            var category = await _readRepository.GetWhereAsync(c => c.Id == id && !c.IsDeleted, true);
            if (category == null) return Result<HadithCategoryGetDto>.Failure(Error.NotFoundError("category"));
            var dto = new HadithCategoryGetDto
            {
                Id = category.Id.ToString(),
                Name = category.CategoryName
            };

            return Result<HadithCategoryGetDto>.Success(dto);
        }
    }
}
