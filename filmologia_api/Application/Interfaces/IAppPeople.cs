using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace filmologia_api.Application.Interface
{
    public interface IAppPeople
    {
        public Object Popular(string apiKey);
        public Object Detail(int idPeople, string apiKey);
    }
}
