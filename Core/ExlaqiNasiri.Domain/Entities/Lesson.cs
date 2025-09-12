using ExlaqiNasiri.Domain.Entities.BaseEntities;

namespace ExlaqiNasiri.Domain.Entities
{
    public class Lesson : BaseEntity
    {
        public Guid LessonFieldId { get; set; }
        public LessonField Field { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }

    }
}
