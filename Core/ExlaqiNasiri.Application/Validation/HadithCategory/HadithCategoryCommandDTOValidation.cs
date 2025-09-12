using ExlaqiNasiri.Application.DTOs.HadithCategory;
using FluentValidation;

namespace ExlaqiNasiri.Application.Validation.HadithCategory
{
    public class HadithCategoryCommandDTOValidation : AbstractValidator<HadithCategoryCommandDTO>
    {
        public HadithCategoryCommandDTOValidation()
        {
            RuleFor(c => c.CategoryName)
                .NotEmpty()
                .NotNull();
        }
    }
}
