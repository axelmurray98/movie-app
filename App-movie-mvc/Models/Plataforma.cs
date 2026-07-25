using System.ComponentModel.DataAnnotations;

namespace App_movie_mvc.Models
{
    public class Plataforma
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        [Url]
        public string Url { get; set; }
        public string UrlLogo { get; set; }
        public List<Pelicula>? PeliculaPlataforma { get; set; }
    }
}
