using Adai.DbContext.Attributes;

namespace Adai.DbContext.Test.Models
{
	/// <summary>
	/// Test
	/// </summary>
	[Table("Test")]
	internal class Test
	{
		[Column("Id")]
		public int Id { get; set; }

		[Column("Code")]
		public string Code { get; set; }

		[Column("Name")]
		public string Name { get; set; }

		[Column("Age")]
		public int Age { get; set; }

		[Column("CreateTime")]
		public DateTime CreateTime { get; set; }

		[Column("UpdateTime")]
		public DateTime? UpdateTime { get; set; }

		[Column("Note")]
		public string? Note { get; set; }

		[Column("C1")]
		public int? C1 { get; set; }

		[Column("C2")]
		public decimal C2 { get; set; }

		[Column("C3")]
		public string C3 { get; set; }

	}
}
