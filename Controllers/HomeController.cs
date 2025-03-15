using EmlakciSitesi.Data;
using Microsoft.AspNetCore.Mvc;
namespace EmlakciSitesi.Controllers;

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
