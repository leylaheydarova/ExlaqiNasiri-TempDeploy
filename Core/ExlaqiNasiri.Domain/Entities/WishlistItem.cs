using ExlaqiNasiri.Domain.Entities.BaseEntities;

namespace ExlaqiNasiri.Domain.Entities
{
    public class WishlistItem : BaseEntityWithDelete
    {
        public Guid ItemId { get; set; }
        public Wishlist Wishlist { get; set; }
    }
}
