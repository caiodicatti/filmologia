using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using filmologia_api.Entities;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using filmologia_api.Application.Interface;

namespace filmologia_api.Application
{
    public class AppMovie : IAppMovie
    {
        public Object Search(string query, bool include_adult, string apiKey)
        {
            RestClient client;
            RestRequest request;
            IRestResponse response;

            string baseUrl = "https://api.themoviedb.org/";
            client = new RestClient(baseUrl);
            request = new RestRequest($"3/search/movie?api_key={apiKey}&=&language=pt-BR&query={query}&page=1&include_adult={include_adult}", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("content-type", "application/json");
            //requisição
            response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = JObject.Parse(response.Content)["results"];
                List<Movie> filmes = JsonConvert.DeserializeObject<List<Movie>>(json.ToString());
                
                if(filmes.Count() > 0)
                {
                   foreach(var filme in filmes)
                    {
                        filme.poster_path = "https://www.themoviedb.org/t/p/w300_and_h450_bestv2/" + filme.poster_path;
                        filme.backdrop_path = "https://www.themoviedb.org/t/p/w300_and_h450_bestv2/" + filme.backdrop_path;
                    }
                }
                else
                {
                    Warning aviso = new Warning();
                    aviso.message = "Nenhum filme foi encontrado.";
                    return aviso;
                }
                //string filmes = JsonConvert.DeserializeObject(json.ToString()).ToString();
                //Console.WriteLine(usuarios[0].email);
                //Console.WriteLine(usuarios[0].avatar);
                return filmes;
            }
            else
            {
                Error error = new Error();
                error.success = false;
                error.statusCode = 502;
                error.type = "Falha na requisição";
                error.message = "Algo deu errado na busca de filmes.";
                return error;
            }
        }

        public Object Detail(int idMovie, string apiKey)
        {
            RestClient client;
            RestRequest request;
            IRestResponse response;

            string baseUrl = "https://api.themoviedb.org/";
            client = new RestClient(baseUrl);
            request = new RestRequest($"3/movie/{idMovie}?api_key={apiKey}&language=pt-BR", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("content-type", "application/json");
            //requisição
            response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = JObject.Parse(response.Content);
                MovieDetail filme = JsonConvert.DeserializeObject<MovieDetail>(json.ToString());

                filme.poster_path = "https://www.themoviedb.org/t/p/w300_and_h450_bestv2/" + filme.poster_path;
                filme.backdrop_path = "https://www.themoviedb.org/t/p/w300_and_h450_bestv2/" + filme.backdrop_path;

                if (filme.production_companies.Count() > 0)
                {                 
                    foreach (var companies in filme.production_companies)
                    {
                        if (companies.logo_path != null || companies.logo_path != "")
                        {
                            companies.logo_path = "https://www.themoviedb.org/t/p/w300_and_h450_bestv2/" + companies.logo_path;
                        }
                    }
                    
                }            

                return filme;
            }
            else
            {
                Error error = new Error();
                error.success = false;
                error.statusCode = 502;
                error.type = "Falha na requisição";
                error.message = "Algo deu errado na busca de filmes.";
                return error;
            }
        }
    }
}
