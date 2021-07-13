using filmologia_api.Entities;
using filmologia_api.Entities.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace filmologia_api.Repository.Interface
{
    public interface IUsuarioRepository
    {
        public List<Usuario> listarUsuarios();
        public Usuario Cadastro(Usuario usuario);
        public Usuario Login(Login login);
        public UsuarioFilme CadastroFilme(UsuarioFilme filme);
        public List<UsuarioFilme> ListaFilmes(int idUsuario);
        public bool DeletaFilme(int idUsuario, int idFilme);
        public Usuario AtualizaUsuario(Usuario usuario);
    }
}
