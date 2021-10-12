﻿namespace Adai.Standard.Models
{
	/// <summary>
	/// RabbitMqConfig
	/// </summary>
	public class RabbitMqConfig
	{
		/// <summary>
		/// Host
		/// </summary>
		public string Host { get; set; }
		/// <summary>
		/// VHost
		/// </summary>
		public string VirtualHost { get; set; }
		/// <summary>
		/// Port
		/// </summary>
		public int Port { get; set; }
		/// <summary>
		/// Username
		/// </summary>
		public string Username { get; set; }
		/// <summary>
		/// Password
		/// </summary>
		public string Password { get; set; }
	}
}