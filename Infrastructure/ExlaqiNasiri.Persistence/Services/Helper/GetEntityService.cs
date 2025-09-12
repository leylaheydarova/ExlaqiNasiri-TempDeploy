using ExlaqiNasiri.Application.Abstraction.Helper;
using ExlaqiNasiri.Application.Repositories.HadithCategories;
using ExlaqiNasiri.Application.Repositories.LessonFields;
using ExlaqiNasiri.Application.ResultPattern;
using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Domain.Entities.BaseEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExlaqiNasiri.Persistence.Services.Helper
{
    public class GetEntityService : IGetEntityService
    {
        readonly IHadithCategoryReadRepository _hadithcategoryReadRepository;
        readonly ILessonFieldReadRepository _lessonFieldReadRepository;
        readonly UserManager<BaseUser> _userManager;
        public GetEntityService(IHadithCategoryReadRepository hadithcategoryReadRepository, ILessonFieldReadRepository lessonFieldReadRepository, UserManager<BaseUser> userManager)
        {
            _hadithcategoryReadRepository = hadithcategoryReadRepository;
            _lessonFieldReadRepository = lessonFieldReadRepository;
            _userManager = userManager;
        }

        public async Task<Result<HadithCategory>> GetHadithCategoryAsync(Guid hadithcategoryId, bool isTracking)
        {
            var hadith = await _hadithcategoryReadRepository.GetWhereAsync(x => x.Id == hadithcategoryId && !x.IsDeleted, isTracking);
            if (hadith == null) return Result<HadithCategory>.Failure(Error.NotFoundError("category"));
            return Result<HadithCategory>.Success(hadith);
        }

        public async Task<Result<LessonField>> GetLessonFieldAsync(Guid lessonFieldId, bool isTracking)
        {
            var field = await _lessonFieldReadRepository.GetWhereAsync(f => f.Id == lessonFieldId && !f.IsDeleted, isTracking);
            if (field == null) return Result<LessonField>.Failure(Error.NotFoundError("field"));
            return Result<LessonField>.Success(field);
        }

        public async Task<Result<BaseUser>> GetUserAsync(string UserNameOrId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(e => e.Id == UserNameOrId || e.Email == UserNameOrId);
            if (user == null) return Result<BaseUser>.Failure(Error.NotFoundError("user"));
            return Result<BaseUser>.Success(user);
        }
    }
}
