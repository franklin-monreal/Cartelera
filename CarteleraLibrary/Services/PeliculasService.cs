using CarteleraDomainServices.Models;
using CarteleraDomainServices.Utilidades;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace CarteleraDomainServices.Controllers
{
    public class PeliculasService
    {
        private readonly ILogger<PeliculasService> _logger;

        public PeliculasService(ILogger<PeliculasService> logger)
        {
            _logger = logger;
        }
        public async Task<List<PeliculaDto>> GetPeliculas(int plataforma)
        {
            List<PeliculaDto> peliculas = new();
            List<PeliculaDto> peliculasDescifradas = new();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5287");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = new();
                    response = await client.GetAsync("/Peliculas?plataforma=" + plataforma).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        string result = "";
                        result = response.Content.ReadAsStringAsync().Result;
                        peliculas = JsonConvert.DeserializeObject<List<PeliculaDto>>(result).ToList();

                    }
                }


                foreach (PeliculaDto item in peliculas)
                {
                    item.Descripcion = plataforma == 1 ? item.Descripcion : ClassSeguridad.descifrar(item.Descripcion);
                    peliculasDescifradas.Add(item);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(message: e.Message);
                throw new Exception(e.Message);
            }
            return peliculasDescifradas;


        }

        public async Task<List<PeliculaApiDto>> GetPeliculasPopularesApi()
        {
            List<PeliculaApiDto> peliculas = new();
            try
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
                string apiKey = "b6e8b6bd4afe15625b542f4569c930d0";
                string lenguage = "es-MX";
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = new();
                response = await client.GetAsync("movie/popular?api_key=" + apiKey + "&lenguage=" + lenguage).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject(result) as JObject;
                    var lista = data["results"].Children();

                    foreach (var pelicula in lista)
                    {
                        PeliculaApiDto pel = new();
                        pel.id = pelicula["id"].Value<int>();
                        pel.original_title = pelicula["title"].Value<string>();
                        pel.overview = pelicula["overview"].Value<string>();
                        pel.vote_average = pelicula["vote_average"].Value<double>();
                        pel.poster_path = pelicula["poster_path"].Value<string>();
                        peliculas.Add(pel);
                    }

                }
            }

            catch (Exception e)
            {
                _logger.LogError(message: e.Message);
                throw new Exception(e.Message);
            }

            return peliculas;


        }

        public async Task<List<PeliculaApiDto>> GetPeliculasSearchApi(String query)
        {
            List<PeliculaApiDto> peliculas = new();
            try
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
                string apiKey = "b6e8b6bd4afe15625b542f4569c930d0";
                string lenguage = "es-MX";
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = new();
                response = await client.GetAsync("search/movie?api_key=" + apiKey + "&query=" + query).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var data = (JObject)JsonConvert.DeserializeObject(result);
                    var lista = data["results"].Children();

                    foreach (var pelicula in lista)
                    {
                        PeliculaApiDto pel = new();
                        pel.id = pelicula["id"].Value<int>();
                        pel.original_title = pelicula["title"].Value<string>();
                        pel.overview = pelicula["overview"].Value<string>();
                        pel.vote_average = pelicula["vote_average"].Value<double>();
                        pel.poster_path = pelicula["poster_path"].Value<string>();

                        peliculas.Add(pel);
                    }

                }
            }
            catch (Exception e)
            {
                _logger.LogError(message: e.Message);
                throw new Exception(e.Message);
            }

            return peliculas;


        }

        public async Task<PeliculaDto> CreatePelicula(PeliculaDto pelicula, int plataforma)
        {

            try
            {
                using var client = new HttpClient();
                pelicula.ImagenPath = pelicula.ImagenPath == null ? "dummy" : pelicula.ImagenPath;
                pelicula.Descripcion = plataforma == 1 ? pelicula.Descripcion : ClassSeguridad.cifrar(pelicula.Descripcion);
                var myContent = JsonConvert.SerializeObject(pelicula);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                client.BaseAddress = new Uri("http://localhost:5287");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = new();
                response = await client.PostAsync("/Peliculas?plataforma=" + plataforma, byteContent).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    string result = "";
                    result = response.Content.ReadAsStringAsync().Result;
                    pelicula = JsonConvert.DeserializeObject<PeliculaDto>(result);

                }
            }

            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception(e.Message);
            }

            return pelicula;


        }


    }
}
