using System.ComponentModel.DataAnnotations;
namespace RealEstate.Models;
public class Listing
{
    [Key]
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? District { get; set; }
    public string? ImageUrl { get; set; }
}
