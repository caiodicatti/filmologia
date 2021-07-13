using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace filmologia_api.Application.Interface
{
    public interface IAppMovie
    {
        public Object Search(string query, bool include_adult, string apiKey);
        public Object Detail(int idMovie, string apiKey);
    }
}
