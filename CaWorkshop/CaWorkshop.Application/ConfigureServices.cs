
using System.Reflection;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

namespace CaWorkshop.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        return services;
    }
}
