using filmologia_api.Application;
using filmologia_api.Entities;
using filmologia_api.Keys;
using Microsoft.AspNetCore.Mvc;
using System;

namespace filmologia_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        TheMovieDb movieDb = new TheMovieDb();
        AppGenre app = new AppGenre();

        [HttpGet]
        [Route("genres")]
        public ActionResult Genres()
        {
            try
            {
                var genero = app.Genres(movieDb.ApiKey());

                return Ok(new Response
                {
                    Success = true,
                    Result = genero
                });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
