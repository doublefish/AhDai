using System.Reflection;

namespace Adai.DbContext.Attribute
{
	/// <summary>
	/// 自定义特性
	/// </summary>
	public class CustomAttribute : System.Attribute
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
		/// <summary>
		/// 属性
		/// </summary>
		public PropertyInfo Property { get; set; }
	}
}
