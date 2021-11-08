﻿using System;

namespace Adai.DbContext.Attributes
{
	/// <summary>
	/// 表的特性
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
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
		/// 库名
		/// </summary>
		public string Schema { get; set; }

		/// <summary>
		/// 列的特性
		/// </summary>
		public ColumnAttribute[] ColumnAttributes { get; set; }
	}
}
