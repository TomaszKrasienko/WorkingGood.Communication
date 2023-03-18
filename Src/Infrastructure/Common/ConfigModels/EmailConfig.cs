using System;
namespace Infrastructure.Common.ConfigModels
{
	public class EmailConfig
	{
		public string Login { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public string Server { get; set; } = string.Empty;
		public int Port { get; set; }
	}
}

