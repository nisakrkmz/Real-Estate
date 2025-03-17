using Microsoft.AspNetCore.Identity;

namespace RealEstate.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Admin ya da başka özel kullanıcı rolleri eklemek isterseniz burada özellikler tanımlayabilirsiniz
        public string FullName { get; set; }
    }
}
