using System.Text;

namespace Adai.Utilities
{
	/// <summary>
	/// TextHelper
	/// </summary>
	public static class TextHelper
	{
		/// <summary>
		/// GetEncoding
		/// </summary>
		/// <param name="charset"></param>
		/// <returns></returns>
		public static Encoding GetEncoding(string charset)
		{
			switch (charset.ToLower())
			{
				case "":
				case "utf8":
				case "utf-8":
					return Encoding.UTF8;
				default: return Encoding.GetEncoding(charset);
			}
		}
	}
}
