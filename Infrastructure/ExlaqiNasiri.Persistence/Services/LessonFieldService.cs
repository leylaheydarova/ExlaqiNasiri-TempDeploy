using ExlaqiNasiri.Application.Abstraction;
using ExlaqiNasiri.Application.DTOs.LessonField;
using ExlaqiNasiri.Application.Repositories.LessonFields;
using ExlaqiNasiri.Application.ResultPattern;
using ExlaqiNasiri.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExlaqiNasiri.Persistence.Services
{
    public class LessonFieldService : ILessonFieldService
    {
        readonly ILessonFieldReadRepository _readRepository;
        readonly ILessonFieldWriteRepository _writeRepository;
        public LessonFieldService(ILessonFieldReadRepository readRepository, ILessonFieldWriteRepository writeRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        public async Task<Result<bool>> CreateAsync(LessonFieldCommandDto dto)
        {
            var field = new LessonField()
            {
                Id = Guid.NewGuid(),
                LessonName = dto.LessonName
            };

            var result = await _writeRepository.AddAsync(field);
            if (!result) return Result<bool>.Failure(Error.OperationFailError("add lesson field"));

            var saveCount = await _writeRepository.SaveAsync();
            if (saveCount == 0) return Result<bool>.Failure(Error.SaveFailError("lesson field"));

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            var field = await _readRepository.GetByIdAsync(id, true);
            if (field == null) return Result<bool>.Failure(Error.NotFoundError("lesson field"));

            var result = _writeRepository.RemovePermanently(field);
            if (!result) return Result<bool>.Failure(Error.OperationFailError("delete lesson field"));

            var savecount = await _writeRepository.SaveAsync();
            if (savecount == 0) return Result<bool>.Failure(Error.SaveFailError("lesson field"));

            return Result<bool>.Success(true);
        }

        public async Task<Result<List<LessonFieldGetDto>>> GetAllAsync(LessonFieldFilterDto? dto)
        {
            var query = dto != null ? _readRepository.GetFilteredAsync(dto) : _readRepository.GetAll(false);
            query = query.AsQueryable().AsNoTracking();

            var dtos = new List<LessonFieldGetDto>();
            dtos = await query.Select(field => new LessonFieldGetDto
            {
                Id = field.Id.ToString(),
                Field = field.LessonName,
            }).ToListAsync();

            return Result<List<LessonFieldGetDto>>.Success(dtos);
        }

        public async Task<Result<LessonFieldGetDto>> GetSingleAsync(Guid id)
        {
            var field = await _readRepository.GetWhereAsync(l => l.Id == id && !l.IsDeleted, true);
            if (field == null) return Result<LessonFieldGetDto>.Failure(Error.NotFoundError("lesson field"));

            var dto = new LessonFieldGetDto
            {
                Id = field.Id.ToString(),
                Field = field.LessonName,
            };

            return Result<LessonFieldGetDto>.Success(dto);
        }

        public async Task<Result<bool>> ToggleAsync(Guid id)
        {
            var field = await _readRepository.GetByIdAsync(id, true);
            if (field == null) return Result<bool>.Failure(Error.NotFoundError("lesson field"));

            var result = _writeRepository.Toggle(field);
            if (!result) return Result<bool>.Failure(Error.OperationFailError("delete lesson field"));

            var savecount = await _writeRepository.SaveAsync();
            if (savecount == 0) return Result<bool>.Failure(Error.SaveFailError("lesson field"));

            return Result<bool>.Success(field.IsDeleted);
        }

        public async Task<Result<bool>> UpdateAsync(Guid id, LessonFieldPatchDto dto)
        {
            var field = await _readRepository.GetWhereAsync(l => l.Id == id && !l.IsDeleted, true);
            if (field == null) return Result<bool>.Failure(Error.NotFoundError("lesson field"));

            if (dto.FieldName != null)
                field.LessonName = dto.FieldName;

            var result = _writeRepository.Update(field);
            if (!result) return Result<bool>.Failure(Error.OperationFailError("update lesson field"));

            var saveCount = await _writeRepository.SaveAsync();
            if (saveCount == 0) return Result<bool>.Failure(Error.SaveFailError("lesson field"));

            return Result<bool>.Success(true);
        }
    }
}
