using System.Collections;
using System.IO;

namespace Adai.WebApi.Models
{
	/// <summary>
	/// TypeSorter
	/// </summary>
	public class TypeSorter : IComparer
	{
		/// <summary>
		/// Compare
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public int Compare(object x, object y)
		{
			if (x == null && y == null)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			var xInfo = new FileInfo(x.ToString());
			var yInfo = new FileInfo(y.ToString());

			return xInfo.Extension.CompareTo(yInfo.Extension);
		}
	}
}
