using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Application.DTOs.WebNews
{
    public class WebNewsGetSingleDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }
    }
}
