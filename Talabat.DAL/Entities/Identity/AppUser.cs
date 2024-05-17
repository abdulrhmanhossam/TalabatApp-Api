using Microsoft.AspNetCore.Identity;

namespace Talabat.DAL.Entities.Identity;
public class AppUser : IdentityUser
{
    public string DisplayName { get; set; }
    public Addsress Address { get; set; }
}

