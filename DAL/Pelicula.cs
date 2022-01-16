using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL
{
    [Table("Peliculas")]
    public class Peliculas
    {
        [Key]
        public int Id { get; set; }

        public int ExternalId { get; set; }

        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public double PuntuacionPromedio { get; set; }
    }
}