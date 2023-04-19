using System.Reflection;
using Microsoft.OpenApi.Models;
namespace WebApi.Common.Extensions.Configuration;
public static class SwaggerConfiguration
{
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var version = Assembly.GetEntryAssembly()?.GetName()?.Version?.ToString();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "WorkingGood.Communication",
                Description = $"Assembly version: {version} \nEnvironment: {environment}"
            });
        });
        return services;
    }
}