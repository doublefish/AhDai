namespace Adai.Standard.RabbitMQ
{
	/// <summary>
	/// RabbitMQExtensions
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// GetString
		/// </summary>
		/// <param name="exchangeType"></param>
		/// <returns></returns>
		public static string GetString(this ExchangeType exchangeType)
		{
			return exchangeType switch
			{
				ExchangeType.Fanout => "fanout",
				ExchangeType.Direct => "direct",
				ExchangeType.Topic => "topic",
				ExchangeType.Headers => "headers",
				ExchangeType.Delayed => "x-delayed-message",
				_ => "",
			};
		}
	}
}
