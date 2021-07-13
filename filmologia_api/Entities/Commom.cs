using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace filmologia_api.Entities
{
    public class Login
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }

    public class EntityVerify
    {
        public bool Verifica { get; set; }
        public string Mensagem { get; set; }
    }

    public class Response
    {
        public bool Success { get; set; }
        public dynamic Result { get; set; }
    }

}
