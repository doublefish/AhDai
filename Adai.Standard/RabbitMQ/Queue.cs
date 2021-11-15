using System.Collections.Generic;

namespace Adai.Standard.RabbitMQ
{
	/// <summary>
	/// 队列
	/// </summary>
	public class Queue
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="name"></param>
		public Queue(string name)
		{
			Name = name;
		}

		/// <summary>
		/// 名称
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 持久的（Durable/Transient）默认：true
		/// </summary>
		public bool Durable { get; set; } = true;
		/// <summary>
		/// 独占
		/// </summary>
		public bool Exclusive { get; set; }
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
