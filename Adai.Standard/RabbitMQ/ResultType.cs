namespace Adai.Standard.RabbitMQ
{
	/// <summary>
	/// 处理结果类型
	/// </summary>
	public enum ResultType
	{
		/// <summary>
		/// 成功
		/// </summary>
		Success = 1,
		/// <summary>
		/// 失败（把消息从队列中移除）
		/// </summary>
		Fail = 2,
		/// <summary>
		/// 重试（把消息追加到队尾）
		/// </summary>
		Retry = 3,
		/// <summary>
		/// 异常
		/// </summary>
		Exception = 9
	}
}
