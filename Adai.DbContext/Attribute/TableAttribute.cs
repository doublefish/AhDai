namespace Adai.DbContext.Attribute
{
	/// <summary>
	/// 表的特性
	/// </summary>
	public class TableAttribute : CustomAttribute
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="name">名称</param>
		public TableAttribute(string name) : base(name)
		{
		}

		/// <summary>
		/// 列的特性
		/// </summary>
		public TableColumnAttribute[] ColumnAttributes { get; set; }
	}
}
