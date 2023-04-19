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
			services.ConfigureMediatr();
			return services;
        }
		private static IServiceCollection ConfigureMediatr(this IServiceCollection services)
		{
			services
				.AddMediatR(cfg => 
					cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
			return services;
		}
	}
}

