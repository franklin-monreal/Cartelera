namespace CarteleraLibrary.Models
{
    public class PeliculaApiDto
    {
        public int id { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public double vote_average { get; set; }

        public string poster_path { get; set; }



        public int Puntuacion()
        {
            return ((int)vote_average);
        }

        public string FullPoster()
        {
            return "https://image.tmdb.org/t/p/w500/" + poster_path;
        }
    }
}
