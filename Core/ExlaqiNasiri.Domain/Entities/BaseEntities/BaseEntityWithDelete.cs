namespace ExlaqiNasiri.Domain.Entities.BaseEntities
{
    public class BaseEntityWithDelete : BaseEntity
    {
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
