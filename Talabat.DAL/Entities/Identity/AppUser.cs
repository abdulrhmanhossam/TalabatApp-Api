using Microsoft.AspNetCore.Identity;

namespace Talabat.DAL.Entities.Identity;
public class AppUser : IdentityUser
{
    public string DisplayName { get; set; }
    public Address Address { get; set; }
}

