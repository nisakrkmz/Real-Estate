using EmlakciSitesi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmlakciSitesi.Data;
public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Ilan> Ilanlar { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
}


