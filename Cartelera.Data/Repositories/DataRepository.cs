namespace Cartelera.Data.Repositories
{
    public class DataRepository
    {
        private readonly MyContext db = new();



        public IEnumerable<Pelicula> GetPeliculas()
        {
            List<Pelicula> peliculas = db.Peliculas.ToList();

            return peliculas;
        }

        public Pelicula CreatePelicula(Pelicula pelicula)
        {
            db.Peliculas.Add(pelicula);
            db.SaveChanges();
            return db.Peliculas.OrderBy(r => r.Id).Last(r => r.Titulo == pelicula.Titulo);

        }
    }
}
