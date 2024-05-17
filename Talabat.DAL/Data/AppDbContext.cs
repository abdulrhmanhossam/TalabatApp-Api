using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Talabat.DAL.Entities;

namespace Talabat.DAL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();

        var connecation = configuration
            .GetSection("ConnectionStrings")
            .GetSection("DefaultConnection").Value;

        optionsBuilder.UseSqlServer(connecation);
    }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        } 
    }
}
