using Microsoft.AspNetCore.Identity;

namespace Talabat.DAL.Entities.Identity;
public class AppIdentityDbContextSeed
{
    public static async Task SeedUsersAysnc(UserManager<AppUser> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new AppUser
            {
                DisplayName = "Abdulrahman Hossam",
                UserName = "abdulrahman",
                Email = "abdulrahmn398@outlook.com",
                PhoneNumber = "01004591659",
                Address = new Address
                {
                    FirstName = "Abdulrahman",
                    LastName = "Hossam",
                    Country = "Egypt",
                    City = "Giza",
                    Street = "October El-Hosary"
                }
            };
            await userManager.CreateAsync(user, "Pa$$w0rd");
        }
    }    
}