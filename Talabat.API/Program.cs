using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.API.Extensions;
using Talabat.API.Middlewares;
using Talabat.DAL.Data;
using Talabat.DAL.Entities.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddDbContext<AppIdentityDbContext>();

builder.Services.AddSingleton<IConnectionMultiplexer>(s => 
{ 
    var connection = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"));
    return ConnectionMultiplexer.Connect(connection);
});

builder.Services.AddIdentityServiceExtension(builder.Configuration);

builder.Services.AddApplicationServicesExtension();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var identityContext = services.GetRequiredService<AppIdentityDbContext>();
    await identityContext.Database.MigrateAsync();

    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    await AppIdentityDbContextSeed.SeedUsersAysnc(userManager);
}
catch (Exception ex)
{
    var logger = services.GetService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}

app.Run();
