using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using filmologia_api.Entities;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using filmologia_api.Application.Interface;

namespace filmologia_api.Application
{
    public class AppGenre : IAppGenre
    {
        public Object Genres(string apiKey)
        {
            RestClient client;
            RestRequest request;
            IRestResponse response;

            string baseUrl = "https://api.themoviedb.org/";
            client = new RestClient(baseUrl);
            request = new RestRequest($"3/genre/movie/list?api_key={apiKey}&=&language=pt-BR", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("content-type", "application/json");
            //requisição
            response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = JObject.Parse(response.Content)["genres"];
                List<Genre> genero = JsonConvert.DeserializeObject<List<Genre>>(json.ToString());

                return genero;
            }
            else
            {
                Error error = new Error();
                error.success = false;
                error.statusCode = 502;
                error.type = "Falha na requisição";
                error.message = "Algo deu errado na busca de gêneros.";
                return error;
            }
        }
    }
}
