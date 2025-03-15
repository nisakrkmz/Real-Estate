using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class IlanController : Controller
{
    private readonly AppDbContext _context;

    public IlanController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var ilanlar = await _context.Ilanlar.ToListAsync();
        return View(ilanlar);
    }

    public IActionResult Yeni()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Yeni(Ilan ilan)
    {
        if (ModelState.IsValid)
        {
            _context.Add(ilan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));  // İlan ekledikten sonra ana sayfaya yönlendir
        }
        return View(ilan);
    }

}
