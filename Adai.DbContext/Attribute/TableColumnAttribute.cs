namespace Adai.DbContext.Attribute
{
	/// <summary>
	/// 表里的列的特性
	/// </summary>
	public class TableColumnAttribute : CustomAttribute
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="name">名称</param>
		/// <param name="type">类型</param>
		public TableColumnAttribute(string name, ColumnType type = ColumnType.Normal) : base(name)
		{
			Type = type;
		}

		/// <summary>
		/// 扩展字段
		/// </summary>
		public ColumnType Type { get; set; }
	}
}
