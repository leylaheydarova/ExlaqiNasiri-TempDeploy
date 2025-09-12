using ExlaqiNasiri.Domain.Entities.BaseEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Application.Repositories
{
    public interface IRepository<T> where T:BaseEntity
    {
        public DbSet<T> Table { get; }
    }
}
