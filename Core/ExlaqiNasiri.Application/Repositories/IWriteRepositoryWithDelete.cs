using ExlaqiNasiri.Domain.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Application.Repositories
{
    public interface IWriteRepositoryWithDelete<T>:IWriteRepository<T> where T : BaseEntityWithDelete
    {
        bool Toggle(T entity);
    }
}
