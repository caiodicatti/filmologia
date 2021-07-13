using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace filmologia_api.Entities
{
    public class BelongsToCollection
    {
        public int id { get; set; }
        public string name { get; set; }
        public string poster_path { get; set; }
        public string backdrop_path { get; set; }
    }
}
