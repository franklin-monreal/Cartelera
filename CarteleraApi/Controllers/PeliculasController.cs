using AutoMapper;
using Cartelera.Data;
using Cartelera.Data.Repositories;
using Cartelera.DTOs;
using CarteleraApi.Utilidades;
using Microsoft.AspNetCore.Mvc;
namespace CarteleraApi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class PeliculasController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly ILogger<PeliculasController> _logger;
        private readonly DataRepository dataRepository = new();


        public PeliculasController(ILogger<PeliculasController> logger, IMapper mapper)
        {

            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet(Name = "GetPeliculas")]
        public IEnumerable<PeliculaDto> GetPeliculas(int plataforma)
        {
            List<PeliculaDto> peliculasCifradas = new();

            try
            {
                List<PeliculaDto> peliculas = dataRepository.GetPeliculas().ToList().Select(e => _mapper.Map<PeliculaDto>(e)).ToList();

                foreach (PeliculaDto item in peliculas)
                {
                    item.Descripcion = plataforma == 1 ? item.Descripcion : ClassSeguridad.cifrar(item.Descripcion);
                    peliculasCifradas.Add(item);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception(e.Message);
            }



            return peliculasCifradas;
        }

        [HttpPost(Name = "CreatePelicula")]
        public PeliculaDto CreatePelicula(PeliculaDto pelicula, int plataforma)
        {
            PeliculaDto peliculaReturn;
            try
            {

                pelicula.Descripcion = plataforma == 1 ? pelicula.Descripcion : ClassSeguridad.descifrar(pelicula.Descripcion);

                peliculaReturn = _mapper.Map<PeliculaDto>(dataRepository.CreatePelicula(_mapper.Map<Pelicula>(pelicula)));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception(e.Message);
            }

            return peliculaReturn;

        }
    }
}