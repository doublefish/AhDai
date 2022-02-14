using System.Collections.Generic;

namespace AhDai.Core.RabbitMQ
{
	/// <summary>
	/// 交换器
	/// </summary>
	public class Exchange
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="name"></param>
		/// <param name="type"></param>
		public Exchange(string name, ExchangeType type = ExchangeType.Direct)
		{
			Name = name;
			Type = type;
		}

		/// <summary>
		/// 名称
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 类型
		/// </summary>
		public ExchangeType Type { get; set; }
		/// <summary>
		/// 持久的（Durable/Transient）默认：true
		/// </summary>
		public bool Durable { get; set; } = true;
		/// <summary>
		/// 自动删除
		/// </summary>
		public bool AutoDelete { get; set; }
		/// <summary>
		/// 参数（无效）
		/// </summary>
		public IDictionary<string, object> Arguments { get; set; }
	}
}
