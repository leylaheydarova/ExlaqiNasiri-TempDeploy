using ExlaqiNasiri.Application.DTOs.Hadith;
using ExlaqiNasiri.Application.DTOs.WebNews;
using ExlaqiNasiri.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Application.Repositories.News
{
    public interface IWebNewsReadRepository : IReadRepository<WebNews>
    {
        IQueryable<WebNews> GetFilteredAsync(WebNewsFilterDto dto);
    }
}
