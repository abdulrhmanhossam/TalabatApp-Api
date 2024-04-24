using Microsoft.AspNetCore.Mvc;
using Talabat.API.Errors;
using Talabat.API.Helpers;
using Talabat.API.Interfaces;
using Talabat.API.Repository;
using Talabat.DAL.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// to set config to return errors
builder.Services.Configure<ApiBehaviorOptions>( options =>
{
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        var errors = actionContext.ModelState
            .Where(m => m.Value.Errors.Count > 0)
            .SelectMany(m => m.Value.Errors)
            .Select(e => e.ErrorMessage).ToArray();

        var responseMessage = new ApiValidationErrorResponse()
        {
            Errors = errors
        };
        return new BadRequestObjectResult(responseMessage);
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
