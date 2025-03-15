using EmlakciSitesi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmlakciSitesi.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        // Ana Sayfa için Index Metodu
        public async Task<IActionResult> Index()
        {
            var ilanlar = await _context.Ilanlar.ToListAsync();  // Veritabanından ilanları çekiyoruz
            return View(ilanlar);  // Bu view'i ana sayfada kullanacağız
        }

        // Hakkımızda Sayfası
        public IActionResult About()
        {
            return View();  // Hakkımızda sayfasına yönlendirme
        }
    }
}
