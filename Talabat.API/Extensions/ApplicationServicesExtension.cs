using Microsoft.AspNetCore.Mvc;
using Talabat.API.Errors;
using Talabat.API.Helpers;
using Talabat.BLL.Interfaces;
using Talabat.BLL.Repository;
using Talabat.BLL.Services;

namespace Talabat.API.Extensions;
public static class ApplicationServicesExtension
{
    public static IServiceCollection AddApplicationServicesExtension(this IServiceCollection services)
    {
        services.AddScoped(typeof(ITokenService), typeof(TokenService));
        services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddAutoMapper(typeof(MappingProfiles));
        // to set config to return errors
        services.Configure<ApiBehaviorOptions>( options =>
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
        return services;
    }
}