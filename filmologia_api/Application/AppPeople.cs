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
    public class AppPeople : IAppPeople
    {

        public Object Popular(string apiKey)
        {
            RestClient client;
            RestRequest request;
            IRestResponse response;

            string baseUrl = "https://api.themoviedb.org/";
            client = new RestClient(baseUrl);
            request = new RestRequest($"3/person/popular?api_key={apiKey}&language=pt-BR", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("content-type", "application/json");
            //requisição
            response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = JObject.Parse(response.Content)["results"];
                List<PopularPeople> peoples = JsonConvert.DeserializeObject<List<PopularPeople>>(json.ToString());

                foreach (var people in peoples)
                {
                    people.profile_path = "https://www.themoviedb.org/t/p/w300_and_h450_bestv2/" + people.profile_path;

                    foreach (var movie in people.known_for)
                    {
                        movie.backdrop_path = "https://www.themoviedb.org/t/p/w300_and_h450_bestv2/" + movie.backdrop_path;
                    }
                }

                return peoples;
            }
            else
            {
                Error error = new Error();
                error.success = false;
                error.statusCode = 502;
                error.type = "Falha na requisição";
                error.message = "Algo deu errado na busca de atores.";
                return error;
            }

        }

        public Object Detail(int idPeople, string apiKey)
        {
            RestClient client;
            RestRequest request;
            IRestResponse response;

            string baseUrl = "https://api.themoviedb.org/";
            client = new RestClient(baseUrl);
            request = new RestRequest($"/3/person/{idPeople}?api_key={apiKey}&language=pt-BR", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("content-type", "application/json");
            //requisição
            response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = JObject.Parse(response.Content);
                People people = JsonConvert.DeserializeObject<People>(json.ToString());

                people.profile_path = "https://www.themoviedb.org/t/p/w300_and_h450_bestv2/" + people.profile_path;
   
                return people;
            }
            else
            {
                Error error = new Error();
                error.success = false;
                error.statusCode = 502;
                error.type = "Falha na requisição";
                error.message = "Algo deu errado na busca do ator.";
                return error;
            }
        }
    }
}
