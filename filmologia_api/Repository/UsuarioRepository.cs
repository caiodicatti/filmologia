using filmologia_api.Entities;
using filmologia_api.Entities.Tables;
using filmologia_api.Repository.Context;
using filmologia_api.Repository.Interface;
using filmologia_api.Utils.Functions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace filmologia_api.Repository
{
    public class UsuarioRepository: IUsuarioRepository
    {

        private DatabaseContext context;

        public UsuarioRepository(DatabaseContext _context)
        {
            context = _context;
        }

        public UsuarioRepository()
        {
        }


        public List<Usuario> listarUsuarios()
        {
           var usuarios = context.Usuario.AsNoTracking().ToList();
            return usuarios;
        }

        public Usuario Cadastro(Usuario usuario)
        {
            context.Usuario.Add(usuario);
            context.SaveChanges();
            return usuario;
        }

        public Usuario Login(Login login)
        {
            Usuario usuario = context.Usuario.Where(u => u.Email == login.Email).FirstOrDefault();
            return usuario;
        }

        public List<UsuarioFilme> ListaFilmes(int idUsuario)
        {
            List<UsuarioFilme> filmes = context.UsuarioFilme.AsNoTracking().Where(filme => filme.IdUsuario == idUsuario).ToList();
            return filmes;
        }

        public UsuarioFilme CadastroFilme(UsuarioFilme filme)
        {
            context.UsuarioFilme.Add(filme);
            context.SaveChanges();
            return filme;
        }

        public bool DeletaFilme(int idUsuario, int idFilme)
        {
            if (context.UsuarioFilme.AsNoTracking().Any(f => f.IdUsuario == idUsuario && f.IdFilme == idFilme))
            {
                UsuarioFilme filme = context.UsuarioFilme.AsNoTracking().Where(f => f.IdUsuario == idUsuario && f.IdFilme == idFilme).FirstOrDefault();
                context.Remove(filme);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Usuario AtualizaUsuario(Usuario usuario)
        {
            Usuario userBD = context.Usuario.AsNoTracking().Where(user => user.idUsuario == usuario.idUsuario).FirstOrDefault();
            userBD.Nome = usuario.Nome;
            userBD.Sexo = usuario.Sexo;
            userBD.DtaNascimento = usuario.DtaNascimento;

            context.Usuario.Update(userBD);
            context.SaveChanges();
            return userBD;
        }
    }
}
