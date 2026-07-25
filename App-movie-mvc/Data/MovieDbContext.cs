using App_movie_mvc.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App_movie_mvc.Data
{
    public class MovieDbContext : IdentityDbContext<Usuario>
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options ) : base(options)
        {

        }
        public DbSet<Pelicula> Pelicula { get; set; }
        public DbSet<Genero> Genero { get; set; }
        public DbSet<Plataforma> Plataformas { get; set; }  
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Favorito> Favoritos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar explícitamente la relación entre Pelicula y Review
            // Una película puede tener muchas reviews; cada review depende de Pelicula via PeliculaId
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Pelicula)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.PeliculaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
