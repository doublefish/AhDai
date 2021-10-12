using System.Security.Cryptography;
using System.Text;

namespace Adai.Security.Utils
{
	/// <summary>
	/// MD5Helper
	/// </summary>
	public static class MD5Helper
	{
		/// <summary>
		/// 加密
		/// </summary>
		/// <param name="original">原文</param>
		/// <param name="encode">编码</param>
		/// <returns></returns>
		public static string Encrypt(string original, Encoding encode = null)
		{
			if (encode == null)
			{
				encode = Encoding.UTF8;
			}
			var buffer = encode.GetBytes(original);
			using var provider = new MD5CryptoServiceProvider();
			var hash = provider.ComputeHash(buffer);
			var builder = new StringBuilder();
			foreach (var b in hash)
			{
				builder.Append(b.ToString("x2"));
			}
			return builder.ToString();
		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="ciphertext">密文</param>
		/// <param name="original">需要验证的数据</param>
		/// <param name="encode"></param>
		/// <returns></returns>
		public static bool Verify(string ciphertext, string original, Encoding encode = null)
		{
			if (encode == null)
			{
				encode = Encoding.UTF8;
			}
			var ciphertext1 = Encrypt(original, encode);
			return string.Compare(ciphertext, ciphertext1) == 0;
		}
	}
}
