using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Application.DTOs.WebNews
{
    public class WebNewsFilterDto
    {
        public DateTime? CreatedFrom { get; set; } 
        public DateTime? CreatedTo { get; set; }
    }
}
