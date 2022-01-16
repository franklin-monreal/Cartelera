using AutoMapper;

namespace CarteleraApi.DTO
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
