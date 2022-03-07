using AutoMapper;
using Cartelera.Data;

namespace Cartelera.DTOs
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Pelicula, PeliculaDto>();
            CreateMap<PeliculaDto, Pelicula>();

        }
    }
}
