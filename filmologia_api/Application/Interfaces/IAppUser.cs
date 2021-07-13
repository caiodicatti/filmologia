using filmologia_api.Entities;
using filmologia_api.Entities.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace filmologia_api.Application.Interface
{
    public interface IAppUser
    {
        public List<dynamic> AllUsers();
        public Object Cadastro(Usuario usuario);
        public dynamic Login(Login login);
        public Object CadastroFilme(UsuarioFilme filme);
        public Object ListaFilmes(int idUsuario);
        public bool DeletaFilme(int idUsuario, int idFilme);
        public Object AtualizaUsuario(Usuario usuario);
    }
}
