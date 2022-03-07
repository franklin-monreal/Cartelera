using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cartelera.Data
{
    [Table("Peliculas")]
    public class Pelicula
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        public int ExternalId { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Descripcion { get; set; }

        public string ImagenPath { get; set; }

        [Range(0.0, 10.0, ErrorMessage = "El rango permitido es de 1 a 10")]
        public double PuntuacionPromedio { get; set; }
    }
}