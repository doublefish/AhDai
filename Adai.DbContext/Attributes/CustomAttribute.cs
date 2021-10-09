using System;

namespace Adai.DbContext.Attributes
{
	/// <summary>
	/// 自定义特性
	/// </summary>
	[AttributeUsage(AttributeTargets.All)]
	public class CustomAttribute : Attribute
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="name">名称</param>
		public CustomAttribute(string name = null)
		{
			Name = name;
		}

		/// <summary>
		/// 名称
		/// </summary>
		public string Name { get; set; }
	}
}
