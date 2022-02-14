using System.Text;

namespace AhDai.Base.Utils
{
	/// <summary>
	/// TextHelper
	/// </summary>
	public static class TextHelper
	{
		/// <summary>
		/// GetEncoding
		/// </summary>
		/// <param name="charSet"></param>
		/// <returns></returns>
		public static Encoding GetEncoding(string charSet)
		{
			switch (charSet.ToLower())
			{
				case "":
				case "utf8":
				case "utf-8":
					return Encoding.UTF8;
				default: return Encoding.GetEncoding(charSet);
			}
		}
	}
}
