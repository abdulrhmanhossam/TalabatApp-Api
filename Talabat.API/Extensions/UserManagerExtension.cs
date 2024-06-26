using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Talabat.DAL.Entities.Identity;

namespace Talabat.API.Extensions;
public static class UserManagerExtension
{
    public static async Task<AppUser> FindUserWithAddressByEmailAsync( this UserManager<AppUser> userManager,
        ClaimsPrincipal User)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);

        var user = await userManager.Users
            .Include(u => u.Address)
            .SingleOrDefaultAsync(u => u.Email == email);

        return user;
    }
}
