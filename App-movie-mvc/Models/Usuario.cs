using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace App_movie_mvc.Models
{
    public class Usuario : IdentityUser
    {
        [Required (ErrorMessage = "Ingresa Un Nombre")]
        [StringLength(100)]

        public string Nombre { get; set; }
        [Required(ErrorMessage = "Ingresa Un Nombre")]
        [StringLength(100)]
        public string Apellido { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }
        public string ImagenPerfilurl { get; set; }

        public List<Favorito> UsuarioFavorito { get; set; }
        public List<Review> UsuarioReviews { get; set; }

    }
    public class RegistroViewModel
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }
       
        [EmailAddress(ErrorMessage = "Ingresa un Email Valido")]
        [Required(ErrorMessage = "Ingresa un correo valido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Ingresa una contraseña valida")]
        [DataType(DataType.Password)]
        public string Clave { get; set; }
        [Required(ErrorMessage = "Debes Confirmar la Clave")]
        [Compare("Clave",ErrorMessage = "Las CLaves no coinciden")]
        [DataType(DataType.Password)]
        public string ConfirmarClave { get; set; }
        
    }
    public class LoginViewModel
    {
        [EmailAddress(ErrorMessage = "Ingresa un Email Valido")]
        [Required(ErrorMessage = "Ingresa un correo valido")]
        public string Email {get; set;}
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Ingresa una contraseña valida")]
        public string Password {get; set;}
        public bool Recordame { get; set;}
    }
    public class MiPerfilViewModel
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string? Email { get; set; }
        public IFormFile? ImagenPerfil { get; set; }
        public string? ImagenUrlPerfil { get; set; }
    }
}
