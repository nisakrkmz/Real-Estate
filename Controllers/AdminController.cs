using Microsoft.AspNetCore.Mvc;
using EmlakciSitesi.Models;
using EmlakciSitesi.Data;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace EmlakciSitesi.Controllers
{
    public class AdminController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;

        public AdminController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, AppDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        // Admin Giriş Sayfası
        public IActionResult Login() => View();

        // Admin Giriş İşlemi
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _userManager.FindByEmailAsync(username);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("Admin"))
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Admin");
                }
            }
            ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre.");
            return View();
        }

        // Admin Çıkış İşlemi
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Yeni()
        {
            return View();  // Yeni ilan ekleme formunu gösterecek view'ı döndürüyoruz
        }


        public IActionResult Index()
        {
            var ilanlar = _context.Ilanlar.ToList();  // Veritabanından tüm ilanları alın
            return View(ilanlar);  // İlanları View'a gönderin
        }

        // İlan düzenleme sayfası için GET metodunu oluşturuyoruz.
        public IActionResult Edit(int id)
        {
            var ilan = _context.Ilanlar.FirstOrDefault(i => i.Id == id);  // İlanı id'ye göre bul
            if (ilan == null)
            {
                return NotFound();  // Eğer ilan bulunmazsa hata döndür
            }

            return View(ilan);  // İlanı Edit sayfasına gönder
        }

        // İlan düzenleme işlemi için POST metodunu oluşturuyoruz.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Ilan ilan)
        {
            if (ModelState.IsValid)  // Eğer gelen veri geçerliyse
            {
                _context.Update(ilan);  // Veritabanında güncelleme yap
                _context.SaveChanges();  // Değişiklikleri kaydet
                return RedirectToAction(nameof(Index));  // İlanlar listesine yönlendir
            }
            return View(ilan);  // Eğer geçerli değilse, tekrar Edit sayfasına dön
        }
    }
}
