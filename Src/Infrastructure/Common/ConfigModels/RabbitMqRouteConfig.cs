using System;
namespace Infrastructure.Common.ConfigModels
{
	public class RabbitMqRouteConfig
	{
		public string Destination { get; set; } = string.Empty;
		public string Exchange { get; set; } = string.Empty;
		public string Queue { get; set; } = string.Empty;
	}
}

