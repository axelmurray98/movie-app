
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App_movie_mvc.Models;
using App_movie_mvc.Data;

public class GeneroController : Controller
{
    private readonly MovieDbContext _context;

    public GeneroController(MovieDbContext context)
    {
        _context = context;
    }

    // GET: GENEROS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Genero.ToListAsync());
    }

    // GET: GENEROS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var genero = await _context.Genero
            .FirstOrDefaultAsync(m => m.Id == id);
        if (genero == null)
        {
            return NotFound();
        }

        return View(genero);
    }

    // GET: GENEROS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: GENEROS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Descipcion,PeliculasGenero")] Genero genero)
    {
        if (ModelState.IsValid)
        {
            _context.Add(genero);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(genero);
    }

    // GET: GENEROS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var genero = await _context.Genero.FindAsync(id);
        if (genero == null)
        {
            return NotFound();
        }
        return View(genero);
    }

    // POST: GENEROS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Descipcion,PeliculasGenero")] Genero genero)
    {
        if (id != genero.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(genero);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GeneroExists(genero.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(genero);
    }

    // GET: GENEROS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var genero = await _context.Genero
            .FirstOrDefaultAsync(m => m.Id == id);
        if (genero == null)
        {
            return NotFound();
        }

        return View(genero);
    }

    // POST: GENEROS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var genero = await _context.Genero.FindAsync(id);
        if (genero != null)
        {
            _context.Genero.Remove(genero);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool GeneroExists(int? id)
    {
        return _context.Genero.Any(e => e.Id == id);
    }
}
