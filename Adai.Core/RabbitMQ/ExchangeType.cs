namespace Adai.Core.RabbitMQ
{
	/// <summary>
	/// 交换器类型
	/// </summary>
	public enum ExchangeType
	{
		/// <summary>
		/// fanout
		/// </summary>
		Fanout,
		/// <summary>
		/// direct
		/// </summary>
		Direct,
		/// <summary>
		/// topic
		/// </summary>
		Topic,
		/// <summary>
		/// headers
		/// </summary>
		Headers,
		/// <summary>
		/// x-delayed-message
		/// </summary>
		Delayed
	}
}
