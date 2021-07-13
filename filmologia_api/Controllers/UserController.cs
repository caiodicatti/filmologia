using filmologia_api.Application.Interface;
using filmologia_api.Entities;
using filmologia_api.Entities.Tables;
using filmologia_api.Utils.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Net;

namespace filmologia_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppSettings appSettings;
        public readonly IAppUser app;


        public UserController(IAppUser _app, IOptions<AppSettings> _appSettings)
        {
            app = _app;
            appSettings = _appSettings.Value;
        }

        [Route("users")]
        [HttpGet]
        //public ActionResult AllUsers([FromServices] DatabaseContext context)
        public ActionResult AllUsers()
        {
            try
            {
                var users = app.AllUsers();
                return Ok(new Response
                {
                    Success = true,
                    Result = users
                });

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("user")]
        [HttpPost]
        public ActionResult Cadastro(Usuario usuario)
        {
            try
            {
                var user = app.Cadastro(usuario);
                return Ok(new Response
                {
                    Success = true,
                    Result = user
                });

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Login dados)
        {
            try
            {
                var ret = app.Login(dados);
                var token = TokenService.GenerateToken(appSettings, ret.User);

                return Ok(new Response
                {
                    Success = true,
                    Result = new {
                        User = new
                        {
                            idUsuario = ret.User.idUsuario,
                            nome = ret.User.Nome,
                            email = ret.User.Email,
                            sexo = ret.User.Sexo,
                            dataNascimento = ret.User.DtaNascimento,
                        },
                        token = token
                    }
                });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("movie")]
        [HttpPost]
        [Authorize]
        public ActionResult CadastroFilme(UsuarioFilme filme)
        {
            try
            {
                var ret = app.CadastroFilme(filme);
                return Ok(new Response
                {
                    Success = true,
                    Result = ret
                });

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("movies")]
        [HttpGet]
        [Authorize]
        public ActionResult ListaFilmes(int idUsuario)
        {
            try
            {
                if (idUsuario == 0)
                {
                    //return new HttpResponseMessage(HttpStatusCode.NoContent);
                    return BadRequest(new Error
                    {
                        success = false,
                        statusCode = (int)HttpStatusCode.BadRequest,
                        type = "QueryParams inválido",
                        message = "O queryParams idUsuario está inválido ou vazio, favor verificar e tentar novamente."
                    });
                }

                var ret = app.ListaFilmes(idUsuario);
                return Ok(new Response
                {
                    Success = true,
                    Result = ret
                });

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("user")]
        [HttpDelete]
        [Authorize]
        public ActionResult DeletaUsuario(int idUsuario, int idFilme)
        {
            try
            {
                if (idUsuario == 0 || idFilme == 0)
                {
                    return BadRequest(new Error
                    {
                        success = false,
                        statusCode = (int)HttpStatusCode.BadRequest,
                        type = "QueryParams inválido",
                        message = "O queryParams idFilme e/ou idUsuario está inválido ou vazio, favor verificar e tentar novamente."
                    });
                }

                bool verify = app.DeletaFilme(idUsuario, idFilme);
                if (verify)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest(new Error
                    {
                        success = false,
                        statusCode = (int)HttpStatusCode.BadRequest,
                        type = "Filme inexistente",
                        message = "O Id do filme passado não corresponde a nenhum cadastro do banco de dados"
                    });

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("user")]
        [HttpPut]
        [Authorize]
        public ActionResult AtualizaUsuario(Usuario usuario)
        {
            try
            {
                if (usuario.idUsuario == 0 || usuario.Nome == "" || usuario.Sexo == "")
                {
                    return BadRequest(new Error
                    {
                        success = false,
                        statusCode = (int)HttpStatusCode.BadRequest,
                        type = "Json inválido",
                        message = "O JSON está inválido, favor verificar e tentar novamente."
                    });
                }

                var ret = app.AtualizaUsuario(usuario);
                return Ok(new Response
                {
                    Success = true,
                    Result = ret
                });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
