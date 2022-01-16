﻿using System.ComponentModel.DataAnnotations;

namespace CarteleraLibrary.Models
{
    public class PeliculaDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        public int ExternalId { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Campo obligatorio"), MaxLength(4000, ErrorMessage = "La {0} no puede exceder de {1} caracteres ")]
        public string Descripcion { get; set; }

        [Range(0.0, 10.0, ErrorMessage = "El rango permitido del campo {0} es de {1} a {2}")]
        public double PuntuacionPromedio { get; set; }

        public int Puntuacion()
        {
            return ((int)PuntuacionPromedio);
        }
        public string ImagenPath { get; set; }

        public string FullPoster()
        {
            return ImagenPath == null || ImagenPath == "dummy" ? "/movie.jpg" : "https://image.tmdb.org/t/p/w500/" + ImagenPath;
        }


    }
}
