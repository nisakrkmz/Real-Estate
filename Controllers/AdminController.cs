using Microsoft.AspNetCore.Mvc;
using RealEstate.Models;
using RealEstate.Data;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace RealEstate.Controllers
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
        public IActionResult Create()
        {
            return View(new Listing()); // Model boş olsa bile bir nesne gönderiyoruz
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Listing listing)
        {
            if (ModelState.IsValid)
            {
                _context.Listings.Add(listing);  // Yeni ilanı ekle
                _context.SaveChanges();  // Değişiklikleri kaydet
                return RedirectToAction("Index");  // İlanlar listesini göster
            }

            return View(listing);
        }
        public IActionResult Index()
        {
            var listings = _context.Listings.ToList();  // Veritabanından tüm ilanları alın
            return View(listings);  // İlanları View'a gönderin
        }

        public IActionResult Edit(int id)
        {
            var listing = _context.Listings.FirstOrDefault(i => i.Id == id);  // İlanı id'ye göre bul
            if (listing == null)
            {
                return NotFound();  // Eğer ilan bulunmazsa hata döndür
            }

            return View(listing);  // İlanı Edit sayfasına gönder
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();  // Id parametresi yoksa hata dön
            }

            var listing = _context.Listings.FirstOrDefault(i => i.Id == id);
            if (listing == null)
            {
                return NotFound();  // İlan bulunamazsa hata döndür
            }

            _context.Listings.Remove(listing);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }



    }
}
