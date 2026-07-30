using Microsoft.AspNetCore.Mvc;
using App_movie_mvc.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace App_movie_mvc.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, ILogger<UsuarioController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public  IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel usuario )
        {
            if (ModelState.IsValid)
            {

                var resultado = await _signInManager.PasswordSignInAsync(usuario.Email, usuario.Password, usuario.Recordame, lockoutOnFailure: false);
                if (resultado.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Inicio de session invalido");
                }
            }
            
            return View(usuario);
        }

        // GET: /Usuario/Registro
        public IActionResult Registro()
        {
            return View();
        }

        // POST: /Usuario/Registro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(RegistroViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new Usuario
            {
                UserName = model.Email,
                Email = model.Email,
                Nombre = model.Nombre,
                Apellido = model.Apellido,
                ImagenPerfilurl = "Default-profil.png"
            };

            var result = await _userManager.CreateAsync(user, model.Clave);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var err in result.Errors)
                ModelState.AddModelError(string.Empty, err.Description);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> MiPerfil()
        {
            var UsuarioActual = await _userManager.GetUserAsync(User);
           
            var usuarioVm = new MiPerfilViewModel
            {
                Nombre = UsuarioActual.Nombre,
                Apellido = UsuarioActual.Apellido,
                Email = UsuarioActual.Email,
                ImagenUrlPerfil = UsuarioActual.ImagenPerfilurl
            };
            return View(usuarioVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> MiPerfil(MiPerfilViewModel usuarioVm)
        {
            if (ModelState.IsValid)
            {
                var UsuarioActual = await _userManager.GetUserAsync(User);

                UsuarioActual.Nombre = usuarioVm.Nombre;
                UsuarioActual.Apellido = usuarioVm.Apellido;

                var resultado = await _userManager.UpdateAsync(UsuarioActual);
                if (resultado.Succeeded)
                {
                    ViewBag.Mensaje = "Perfil Actualizado con exito";
                    return View(usuarioVm);
                }
                else
                {
                    foreach (var error in resultado.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            // Si el modelo no es válido o la actualización falló, devolver la vista con el modelo
            return View(usuarioVm);
        }
    }
}
