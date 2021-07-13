using filmologia_api.Application;
using filmologia_api.Entities;
using filmologia_api.Keys;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace filmologia_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        TheMovieDb movieDB = new TheMovieDb();
        AppPeople app = new AppPeople();


        [Route("popular")]
        [HttpGet]
        public ActionResult Popular()
        {
            try
            {
                var atores = app.Popular(movieDB.ApiKey());

                return Ok(new Response
                {
                    Success = true,
                    Result = atores
                });

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }      
        }

        [Route("detail")]
        [HttpGet]
        public ActionResult Detail(int id_people)
        {
            try
            {
                if (id_people == 0)
                {
                    return BadRequest(new Error { 
                        success = false,
                        statusCode = (int)HttpStatusCode.BadRequest,
                        type = "QueryParams inválido",
                        message = "O queryParams id_people está inválido, favor verificar e tentar novamente."
                    });
                }
                var ator = app.Detail(id_people, movieDB.ApiKey());

                return Ok(new Response
                {
                    Success = true,
                    Result = ator
                });

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
