using filmologia_api.Application.Interface;
using filmologia_api.Entities;
using filmologia_api.Entities.Tables;
using filmologia_api.Repository;
using filmologia_api.Repository.Context;
using filmologia_api.Repository.Interface;
using filmologia_api.Utils.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace filmologia_api.Application
{
    public class AppUser : IAppUser
    {
        public AppUser(IUsuarioRepository repositorio_)
        {
            repositorio = repositorio_;
        }
        public readonly IUsuarioRepository repositorio;

        public List<dynamic> AllUsers()
        {
            try
            {
                List<Usuario> usuarios = new List<Usuario>();
                List<dynamic> listaRetorno = new List<dynamic>();
                Commons commom = new Commons();
                usuarios = repositorio.listarUsuarios();

                if (usuarios.Count() > 0)
                {
                    foreach (var user in usuarios)
                    {
                        listaRetorno.Add(commom.HideParamsUser(user));
                    }
                }

                return listaRetorno;

            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Object Cadastro(Usuario usuario)
        {
            Commons commom = new Commons();

            EntityVerify verifica = commom.VerifyUser(usuario);
            if (verifica.Verifica)
            {
                Hash encript = new Hash(SHA512.Create());
                usuario.Senha = encript.CriptografarSenha(usuario.Senha);

                var retorno = commom.HideParamsUser(repositorio.Cadastro(usuario));
                return retorno;
            }
            else
            {
                Error error = new Error();
                error.success = false;
                error.statusCode = 422;
                error.type = "Falha no cadastro";
                error.message = verifica.Mensagem;
                return error;
            }
        }

        public dynamic Login(Login login)
        {
            Hash encript = new Hash(SHA512.Create());
            Usuario usuario = repositorio.Login(login);

            bool verifica = encript.VerificarSenha(login.Senha, usuario.Senha);

            if (verifica)
            {
                dynamic ret = new
                {
                    Autenticado = verifica,
                    User = usuario
                };

                return ret;
            }
            else
            {
                dynamic ret = new
                {
                    Autenticado = verifica,
                    Mensagem = "Email ou senha incorreto."
                };

                return ret;
            }
        }

        public Object CadastroFilme(UsuarioFilme filme)
        {
            Commons commom = new Commons();

            EntityVerify verifica = commom.VerifyUserMovie(filme);

            if (verifica.Verifica)
            {
                UsuarioFilme ret = repositorio.CadastroFilme(filme);
                return ret;
            }
            else
            {
                Error error = new Error();
                error.success = false;
                error.statusCode = 422;
                error.type = "Falha no cadastro";
                error.message = verifica.Mensagem;
                return error;
            }

        }

        public Object ListaFilmes(int idUsuario)
        {
           List<UsuarioFilme> filmes = repositorio.ListaFilmes(idUsuario);

            if (filmes.Count() <= 0)
            {
                return new
                {
                    success = true,
                    statusCode = 204,
                    message = "O usuário não possui filmes vinculados a ele"
                };
            }

            return filmes;
        }

        public bool DeletaFilme(int idUsuario, int idFilme)
        {
            bool verify = repositorio.DeletaFilme(idUsuario, idFilme);
            return verify;
        }

        public Object AtualizaUsuario (Usuario usuario)
        {
            Commons commom = new Commons();
            return commom.HideParamsUser(repositorio.AtualizaUsuario(usuario));
        }
    }
}
