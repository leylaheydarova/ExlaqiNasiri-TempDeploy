using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExlaqiNasiri.Application.Features.Command
{
    public class CommandResponse
    {
        public int StatusCode { get; set; } = 200;
        public string Message { get; set; }
        public bool Result { get; set; }
    }
}
