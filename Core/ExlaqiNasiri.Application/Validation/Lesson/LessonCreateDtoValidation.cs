using ExlaqiNasiri.Application.DTOs.Lesson;
using FluentValidation;

namespace ExlaqiNasiri.Application.Validation.Lesson
{
    public class LessonCreateDtoValidation : AbstractValidator<LessonCreateDto>
    {
        public LessonCreateDtoValidation()
        {
            RuleFor(l => l.Title)
                .MinimumLength(3)
                .NotNull()
                .NotEmpty();
            RuleFor(l => l.Text)
                .NotEmpty()
                .NotNull()
                .MinimumLength(10);
        }
    }
}
