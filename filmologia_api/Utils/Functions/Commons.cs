using filmologia_api.Entities;
using filmologia_api.Entities.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace filmologia_api.Utils.Functions
{
    public class Commons
    {

        public dynamic HideParamsUser(Usuario user)
        {
            
            dynamic ret = new
            {
                idUsuario = user.idUsuario,
                Nome = user.Nome,
                Email = user.Email,
                Sexo = user.Sexo,
                DataNascimento = user.DtaNascimento
            };

            return ret;
            
        }

        public EntityVerify VerifyUser(Usuario user)
        {
            EntityVerify retorno = new EntityVerify();
            retorno.Verifica = true;
            DateTime dateValue;
            if (user.Nome == "")
            {
                retorno.Verifica = false;
                retorno.Mensagem = "Nome não pode ser vazio";
            }else if (user.Email.IndexOf("@") == -1 || user.Email == "")
            {
                retorno.Verifica = false;
                retorno.Mensagem = "E-mail inválido";
            }else if (user.Senha.Length < 8 )
            {
                retorno.Verifica = false;
                retorno.Mensagem = "Senha inválida. Minimo de 8 caracteres";
            }else if (user.Sexo == "" || user.Sexo.Length > 1)
            {
                retorno.Verifica = false;
                retorno.Mensagem = "Campo sexo inválido";
            }
            else if (user.DtaNascimento.ToString() == "" || !DateTime.TryParse(user.DtaNascimento.ToString(), out dateValue))
            {
                retorno.Verifica = false;
                retorno.Mensagem = "Data de nascimento inválida";
            }

            return retorno;
        }

        public EntityVerify VerifyUserMovie(UsuarioFilme movie)
        {
            EntityVerify retorno = new EntityVerify();
            retorno.Verifica = true;
  
            if (movie.IdUsuario == 0)
            {
                retorno.Verifica = false;
                retorno.Mensagem = "IdUsuario não pode ser nulo ou zero";
            }
            else if (movie.IdFilmeAPI == 0)
            {
                retorno.Verifica = false;
                retorno.Mensagem = "IdFilmeAPI não pode ser nulo ou zero";
            }
            else if (movie.Nome == "")
            {
                retorno.Verifica = false;
                retorno.Mensagem = "Título do filme não pode ser vazio";
            }

            return retorno;
        }
    }
}
