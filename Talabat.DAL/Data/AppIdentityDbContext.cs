using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Talabat.DAL.Entities.Identity;

namespace Talabat.DAL.Data;
public class AppIdentityDbContext : IdentityDbContext<AppUser>
{
    public AppIdentityDbContext()
    {
        
    }
    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options): base(options)
    {
        
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();

        var connecation = configuration
            .GetSection("ConnectionStrings")
            .GetSection("IdentityConnection").Value;

        optionsBuilder.UseSqlServer(connecation);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppIdentityDbContext).Assembly);
    } 
}