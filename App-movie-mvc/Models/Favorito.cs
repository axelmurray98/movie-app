namespace App_movie_mvc.Models
{
    public class Favorito
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario usuario;
        public string Pelicula { get; set; }
        public DateTime Fecha { get; set; }
        public Pelicula? Peliculas { get; set; }
    }
}
