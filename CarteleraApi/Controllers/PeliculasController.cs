using AutoMapper;
using CarteleraApi.DTO;
using CarteleraApi.Utilidades;
using Microsoft.AspNetCore.Mvc;
namespace CarteleraApi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class PeliculasController : ControllerBase
    {

        private readonly IMapper _mapper;
        private MyContext db = new MyContext();
        private readonly ILogger<PeliculasController> _logger;


        public PeliculasController(ILogger<PeliculasController> logger, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet(Name = "GetPeliculas")]
        public IEnumerable<PeliculaDto> GetPeliculas(int plataforma)
        {
            List<PeliculaDto> peliculas = db.Peliculas.ToList().Select(e => _mapper.Map<PeliculaDto>(e)).ToList();

            List<PeliculaDto> peliculasCifradas = new List<PeliculaDto>();

            foreach (PeliculaDto item in peliculas)
            {
                item.Descripcion = plataforma == 1 ? item.Descripcion : ClassSeguridad.cifrar(item.Descripcion);
                peliculasCifradas.Add(item);
            }

            return peliculasCifradas;
        }

        [HttpPost(Name = "CreatePelicula")]
        public PeliculaDto CreatePelicula(PeliculaDto pelicula, int plataforma)
        {
            pelicula.Descripcion = plataforma == 1 ? pelicula.Descripcion : ClassSeguridad.descifrar(pelicula.Descripcion);
            db.Peliculas.Add(_mapper.Map<Pelicula>(pelicula));
            db.SaveChanges();
            return _mapper.Map<PeliculaDto>(db.Peliculas.OrderBy(r => r.Id).Last(r => r.Titulo == pelicula.Titulo));

        }
    }
}