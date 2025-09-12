using ExlaqiNasiri.Application.DTOs.LessonField;
using FluentValidation;

namespace ExlaqiNasiri.Application.Validation.LessonField
{
    public class LessonFieldCreateDTOValidation : AbstractValidator<LessonFieldCommandDto>
    {
        public LessonFieldCreateDTOValidation()
        {
            RuleFor(l => l.LessonName)
                .NotEmpty()
                .NotNull();
        }
    }
}
