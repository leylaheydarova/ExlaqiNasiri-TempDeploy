using ExlaqiNasiri.Domain.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Application.Repositories
{
    public interface IReadRepository<T>:IRepository<T> where T:BaseEntity
    {
        IQueryable<T> GetAll(bool isTracking, params string[] includes);
        IQueryable<T> GetAllWhere(Expression<Func<T, bool>> predicate, bool isTracking, params string[] includes);
        Task<T> GetByIdAsync(Guid id, bool isTracking, params string[] includes);
        Task<T> GetWhereAsync(Expression<Func<T, bool>> predicate, bool isTracking, params string[] includes);
    }
}
