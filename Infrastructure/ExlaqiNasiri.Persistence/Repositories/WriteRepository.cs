using ExlaqiNasiri.Application.Repositories;
using ExlaqiNasiri.Domain.Entities.BaseEntities;
using ExlaqiNasiri.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace ExlaqiNasiri.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        internal readonly ExlaqiNasiriDbContext _context;

        public WriteRepository(ExlaqiNasiriDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<bool> AddAsync(T entity)
        {
            EntityEntry entry = await _context.AddAsync(entity);
            return entry.State == EntityState.Added;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public bool RemovePermanently(T entity)
        {
            EntityEntry entry = _context.Remove(entity);
            return entry.State == EntityState.Deleted;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public bool Update(T entity)
        {
            EntityEntry entry = _context.Update(entity);
            return entry.State == EntityState.Modified;
        }
    }
}
