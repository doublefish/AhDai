using System.Collections;
using System.IO;

namespace Adai.Core.WebApi.Models
{
	/// <summary>
	/// NameSorter
	/// </summary>
	public class NameSorter : IComparer
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

			return xInfo.FullName.CompareTo(yInfo.FullName);
		}
	}
}
