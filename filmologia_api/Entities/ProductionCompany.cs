using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace filmologia_api.Entities
{
    public class ProductionCompany
    {
        public int id { get; set; }
        public string logo_path { get; set; }
        public string name { get; set; }
        public string origin_country { get; set; }
    }
}
