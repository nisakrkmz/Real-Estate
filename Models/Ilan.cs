using System.ComponentModel.DataAnnotations;
namespace EmlakciSitesi.Models;
public class Ilan
{
    [Key]
    public int Id { get; set; }
    public string? Baslik { get; set; }
    public string? Aciklama { get; set; }
    public decimal Fiyat { get; set; }
    public string? Sehir { get; set; }
    public string? ResimUrl { get; set; }
}
