using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace filmologia_api.Entities
{
    public class Error
    {
        public bool success { get; set; }
        public int statusCode { get; set; }
        public string type { get; set; }
        public string message { get; set; }
    }
}
