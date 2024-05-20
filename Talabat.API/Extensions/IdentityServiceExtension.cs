using Microsoft.AspNetCore.Identity;
using Talabat.DAL.Data;
using Talabat.DAL.Entities.Identity;

namespace Talabat.API.Extensions;
public static class IdentityServiceExtension
{
    public static IServiceCollection AddIdentityServiceExtension(this IServiceCollection services)
    {
        services.AddIdentity<AppUser, IdentityRole>(options =>
        { 
            
        }).AddEntityFrameworkStores<AppIdentityDbContext>();
        
        services.AddAuthentication();

        return services;
    }
}