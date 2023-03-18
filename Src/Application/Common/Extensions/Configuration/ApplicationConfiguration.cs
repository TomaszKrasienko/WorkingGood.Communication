using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Common.Extensions.Configuration
{
	public static class ApplicationConfiguration
	{
		public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
			services
				.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
			
			return services;
        }
	}
}

