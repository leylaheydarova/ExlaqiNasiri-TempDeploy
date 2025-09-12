using ExlaqiNasiri.Domain.Entities.BaseEntities;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Application.Repositories
{
    public interface IWriteRepository<T>:IRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T entity);
        bool RemovePermanently(T entity);
        bool Update(T entity);
        Task<IDbContextTransaction> BeginTransactionAsync(); //transaction bir endpoint daxilində 1-dən çox Save işlədəndə istifadə edilməlidir.
        Task<int> SaveAsync();
        int Save();
    }
}
