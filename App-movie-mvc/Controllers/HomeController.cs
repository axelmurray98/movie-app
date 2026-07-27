using App_movie_mvc.Data;
using App_movie_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace App_movie_mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MovieDbContext _context;
        private const int PageSize = 8;
      //  private readonly LlmService _llmService;
        public HomeController(ILogger<HomeController> logger, MovieDbContext context) { 
            _logger = logger;
            _context = context;
          
        }
        public async Task<IActionResult> Index(int pagina = 1, string txtBusqueda = "",int generoId = 0)
        {
            if (pagina < 1) pagina = 1;

            var consulta = _context.Pelicula.AsQueryable();
            if (!string.IsNullOrEmpty(txtBusqueda))
            {
                consulta = consulta.Where(p => p.Titulo.Contains(txtBusqueda));
            }
            if(generoId > 0)
            {
                consulta = consulta.Where(p => p.Id == generoId);
            }
            var peliculas = await consulta
               .Skip((pagina - 1) * PageSize)
               .Take(PageSize)
               .ToListAsync();

            var totalPeliculas = await consulta.CountAsync();
            var totalPaginas = (int)Math.Ceiling(totalPeliculas / (double)PageSize);

            if (pagina > totalPaginas && totalPaginas > 0) pagina = totalPaginas;

           

            ViewBag.PaginaActual = pagina;
            ViewBag.TotalPaginas = totalPaginas;
            ViewBag.TotalPeliculas = totalPeliculas;
            ViewBag.TxtBusqueda = txtBusqueda;

            var generos = await _context.Genero.OrderBy(g => g.Descipcion).ToListAsync();
            generos.Insert(0, new Genero { Id = 0,Descipcion = "Todos" });
            ViewBag.generoId = new SelectList(
               generos,
                "Id",
                "Descipcion"
                );

            
            return View(peliculas);
        }
    

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> Details(int id)
        {
            var pelicula = await _context.Pelicula
                .Include(p => p.Genero)
                .FirstOrDefaultAsync(p => p.Id == id);
            return View(pelicula);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

       

        
       
    }
}
