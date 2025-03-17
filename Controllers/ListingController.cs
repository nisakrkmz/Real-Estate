using RealEstate.Data;
using RealEstate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace RealEstate.Controllers;
public class ListingController : Controller
{
    private readonly AppDbContext _context;

    public ListingController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var listings = await _context.Listings.ToListAsync();
        return View(listings);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Listing listing)
    {
        if (ModelState.IsValid)
        {
            _context.Add(listing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(listing);
    }
    
    
    public IActionResult Detail(int id)
    {
        var listing = _context.Listings.FirstOrDefault(i => i.Id == id);  // İlanı veritabanından alıyoruz

        if (listing == null)
        {
            return NotFound();  // İlan bulunamazsa hata döndürüyoruz
        }

        return View(listing);  // Detaylar sayfasına ilanı gönderiyoruz
    }

}
