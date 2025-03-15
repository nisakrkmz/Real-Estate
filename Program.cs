using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Veritabanı bağlantısını ekle
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EmlakciDb")));

// Configure the HTTP request pipeline.
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // Üretim ortamında hata yönetimi
    app.UseExceptionHandler("/Home/Error");
    // HTTP Strict Transport Security (HSTS) kullanımı sadece üretim ortamında
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Kullanıcı kimlik doğrulaması için yetkilendirme middleware'i
app.UseAuthorization();

// Varsayılan route'u yapılandır
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Ilan}/{action=Index}/{id?}");

app.Run();
