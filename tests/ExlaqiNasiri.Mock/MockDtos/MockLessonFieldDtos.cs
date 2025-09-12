using ExlaqiNasiri.Application.DTOs.LessonField;

namespace ExlaqiNasiri.Mock.MockDtos
{
    public class MockLessonFieldDtos
    {
        public static LessonFieldCommandDto CreateMockCommandDto()
        {
            return new LessonFieldCommandDto()
            {
                LessonName = "Test"
            };
        }

        public static LessonFieldFilterDto CreateMockFilterDto()
        {
            return new LessonFieldFilterDto()
            {
                CreatedFrom = new DateTime(2023, 1, 1),
                CreatedTo = new DateTime(2025, 12, 31),
                isDeleted = false
            };
        }

        public static LessonFieldGetDto CreateMockGetDto()
        {
            return new LessonFieldGetDto()
            {
                Id = Guid.NewGuid().ToString(),
                Field = "Test"
            };
        }

        public static LessonFieldPatchDto CreateMockPatchDto()
        {
            return new LessonFieldPatchDto()
            {
                FieldName = "Test"
            };

        }
    }
}
