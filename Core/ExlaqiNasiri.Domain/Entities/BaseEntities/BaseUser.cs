using Microsoft.AspNetCore.Identity;

namespace ExlaqiNasiri.Domain.Entities.BaseEntities
{
    public class BaseUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpireDate { get; set; }
        public DateTime CreadetAt { get; set; } = DateTime.UtcNow;
    }
}
