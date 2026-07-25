using System.ComponentModel.DataAnnotations;

namespace App_movie_mvc.Models
{
    public class Genero
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Descipcion { get; set; }
        public List<Pelicula>? PeliculasGenero { get; set; }
    }
}
