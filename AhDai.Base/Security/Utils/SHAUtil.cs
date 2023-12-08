using System.Security.Cryptography;
using System.Text;

namespace AhDai.Base.Security.Utils
{
	/// <summary>
	/// SHAHelper
	/// </summary>
	public static class SHAUtil
	{
		/// <summary>
		/// 加密
		/// </summary>
		/// <param name="original">原文</param>
		/// <param name="hashName">哈希算法名称</param>
		/// <param name="encode">编码</param>
		/// <returns></returns>
		public static string Encrypt(string original, string hashName = HashHalg.SHA1, Encoding encode = null)
		{
			if (encode == null)
			{
				encode = Encoding.UTF8;
			}
			var buffer = encode.GetBytes(original);
			using var hashAlgorithm = HashAlgorithm.Create(hashName);
			var hash = hashAlgorithm.ComputeHash(buffer);
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
		/// <param name="hashName">哈希算法名称</param>
		/// <param name="encode"></param>
		/// <returns></returns>
		public static bool Verify(string ciphertext, string original, string hashName = HashHalg.SHA1, Encoding encode = null)
		{
			if (encode == null)
			{
				encode = Encoding.UTF8;
			}
			var ciphertext1 = Encrypt(original, hashName, encode);
			return string.Compare(ciphertext, ciphertext1) == 0;
		}
	}
}
