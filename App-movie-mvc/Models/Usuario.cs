using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace App_movie_mvc.Models
{
    public class Usuario : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }
        public string ImagenPerfilurl { get; set; }

        public List<Favorito> UsuarioFavorito { get; set; }
        public List<Review> UsuarioReviews { get; set; }

    }
    public class UsuarioViewModel
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [PasswordPropertyText]
        public string Clave { get; set; }
        [PasswordPropertyText]
        public string ConfirmarClave { get; set; }
        
    }
}
