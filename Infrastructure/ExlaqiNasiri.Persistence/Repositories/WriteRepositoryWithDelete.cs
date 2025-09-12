using ExlaqiNasiri.Application.Repositories;
using ExlaqiNasiri.Domain.Entities.BaseEntities;
using ExlaqiNasiri.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ExlaqiNasiri.Persistence.Repositories
{
    public class WriteRepositoryWithDelete<T> : WriteRepository<T>, IWriteRepositoryWithDelete<T> where T : BaseEntityWithDelete
    {
        public WriteRepositoryWithDelete(ExlaqiNasiriDbContext context) : base(context)
        {

        }

        public bool Toggle(T entity)
        {
            entity.IsDeleted = !entity.IsDeleted;
            return _context.Entry(entity).State == EntityState.Modified;
        }
    }
}
