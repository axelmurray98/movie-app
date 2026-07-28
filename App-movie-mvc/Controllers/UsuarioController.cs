using Microsoft.AspNetCore.Mvc;

namespace App_movie_mvc.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string user )
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registro(string user)
        {
            return View();
        }
        public IActionResult Logout()
        {
            return View();
        }

    }
}
