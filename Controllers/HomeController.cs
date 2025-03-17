using RealEstate.Data;
using Microsoft.AspNetCore.Mvc;
namespace RealEstate.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    // Ana Sayfa için Index Metodu
    public IActionResult Index()
    {
        return View();
    }

    // Hakkımızda Sayfası
    public IActionResult About()
    {
        return View();  // Hakkımızda sayfasına yönlendirme
    }
}
