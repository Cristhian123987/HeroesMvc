using HeroesMvc.Data;
using HeroesMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HeroesMvc.Controllers;

public class HomeController : Controller
{
    private readonly HeroesDbContext _context;

    public HomeController(HeroesDbContext context)
    {
        _context = context;
    }

    // Página de inicio con los enlaces a cada prueba (para tomar las capturas).
    public async Task<IActionResult> Index()
    {
        try
        {
            ViewBag.Ciudades = await _context.Ciudades
                .OrderBy(c => c.Id)
                .Select(c => new EnlacePrueba(c.Id, c.Nombre))
                .ToListAsync();

            ViewBag.Cuarteles = await _context.Cuarteles
                .OrderBy(c => c.Id)
                .Select(c => new EnlacePrueba(c.Id, c.Nombre))
                .ToListAsync();
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
        }

        return View();
    }

    public IActionResult Error() => Content("Ocurrió un error.");
}
