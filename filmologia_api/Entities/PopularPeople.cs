using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace filmologia_api.Entities
{
    public class PopularPeople
{    
        public bool adult { get; set; }
        public int gender { get; set; }
        public int id { get; set; }
        public List<Movie> known_for { get; set; }
        public string known_for_department { get; set; }
        public string name { get; set; }
        public double popularity { get; set; }
        public string profile_path { get; set; }
        
    }
}
