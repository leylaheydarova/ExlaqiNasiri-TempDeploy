using ExlaqiNasiri.Domain.Entities.BaseEntities;

namespace ExlaqiNasiri.Domain.Entities
{
    public class Wishlist : BaseEntity
    {
        public Guid ItemId { get; set; }
        public Guid UserId { get; set; }
        public ICollection<WishlistItem> wishlistItems { get; set; } = new List<WishlistItem>();

    }
}
