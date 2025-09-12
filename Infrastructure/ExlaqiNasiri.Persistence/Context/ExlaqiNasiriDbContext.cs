using ExlaqiNasiri.Domain.Entities;
using ExlaqiNasiri.Domain.Entities.BaseEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExlaqiNasiri.Persistence.Context
{
    public class ExlaqiNasiriDbContext : IdentityDbContext<BaseUser>
    {
        public ExlaqiNasiriDbContext(DbContextOptions<ExlaqiNasiriDbContext> options) : base(options)
        {

        }

        public DbSet<HadithCategory> HadithCategories { get; set; }
        public DbSet<Hadith> Hadiths { get; set; }
        public DbSet<LessonField> LessonFields { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<WebUser> WebUsers { get; set; }
        public DbSet<WebNews> WebNewses { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Lesson>()
            .HasOne(l => l.Field)
            .WithMany(f => f.Lessons)
            .HasForeignKey(l => l.LessonFieldId);
        }
    }
}
