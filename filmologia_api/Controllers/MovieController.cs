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
    public class MovieController : ControllerBase
    {
        TheMovieDb movieDb = new TheMovieDb();
        AppMovie app = new AppMovie();

        [HttpGet]
        [Route("search")]
        public ActionResult Search(string name, bool include_adult = false)
        {           

            if(name == null || name == "")
            {
                return BadRequest(new Error
                {
                    success = false,
                    statusCode = (int)HttpStatusCode.BadRequest,
                    type = "QueryParams inválido",
                    message = "O queryParams name está inválido ou vazio, favor verificar e tentar novamente."
                });
            }

            var ret = app.Search(name, include_adult, movieDb.ApiKey());

            return Ok(new Response
            {
                Success = true,
                Result = ret
            });
        }

        [HttpGet]
        [Route("detail")]
        public ActionResult Detail(int id_movie)
        {
            try
            {
                if (id_movie == 0)
                {
                    //return BadRequest(error);
                    return BadRequest(new Error {
                        success = false,
                        statusCode = (int)HttpStatusCode.BadRequest,
                        type = "QueryParams inválido",
                        message = "O queryParams id_movie está inválido, favor verificar e tentar novamente."
                    });
                }

                var filme = app.Detail(id_movie, movieDb.ApiKey());

                return Ok(new Response
                {
                    Success = true,
                    Result = filme
                });

            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
