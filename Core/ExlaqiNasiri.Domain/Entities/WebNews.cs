using ExlaqiNasiri.Domain.Entities.BaseEntities;

namespace ExlaqiNasiri.Domain.Entities
{
    public class WebNews : BaseEntity
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }
    }
}
