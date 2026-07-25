using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App_movie_mvc.Models
{
    public class Pelicula
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]

        public string Titulo { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaLanzamiento { get; set; }
        [Range(1,500)]
        public int MinutosDuracion { get; set; }
        [Required]
        [StringLength(100)]
        public string Sinopsis { get; set; }
        [Url]
        [Required]
        public string PosteUrlPortada { get; set; }
        [NotMapped]
        public int AvgRating { get; set; }
        public List<Favorito>? UsuarioFavoritos { get; set; }
        public int PlataformaId { get; set; }
        public Plataforma? Plataforma {get; set; }
        public int GeneroId { get; set; }
        public Genero? Genero { get; set; }
        // A película puede tener múltiples reviews (relación uno-a-muchos)
        public List<Review>? Reviews { get; set; }
    }
}
