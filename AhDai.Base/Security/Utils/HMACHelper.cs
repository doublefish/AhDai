using System.Security.Cryptography;
using System.Text;

namespace AhDai.Base.Security.Utils
{
	/// <summary>
	/// HMACHelper
	/// </summary>
	public static class HMACHelper
	{
		/// <summary>
		/// MD5
		/// </summary>
		/// <param name="original">原文</param>
		/// <param name="key">密钥</param>
		/// <param name="hashName">哈希算法名称</param>
		/// <param name="encode">编码</param>
		/// <returns></returns>
		public static string Encrypt(string original, string key, string hashName = HashHalg.MD5, Encoding encode = null)
		{
			if (encode == null)
			{
				encode = Encoding.UTF8;
			}
			var keys = encode.GetBytes(key);
			var buffer = encode.GetBytes(original);
			byte[] hash;
			switch (hashName)
			{
				case HashHalg.MD5:
					{
						using var md5 = new HMACMD5(keys);
						hash = md5.ComputeHash(buffer);
					}
					break;
				case HashHalg.SHA1:
					{
						using var sha1 = new HMACSHA1(keys);
						hash = sha1.ComputeHash(buffer);
					}
					break;
				case HashHalg.SHA256:
					{
						using var sha256 = new HMACSHA256(keys);
						hash = sha256.ComputeHash(buffer);
					}
					break;
				default: goto case HashHalg.MD5;
			}
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
		/// <param name="key">密钥</param>
		/// <param name="hashName">哈希算法名称</param>
		/// <param name="encode"></param>
		/// <returns></returns>
		public static bool Verify(string ciphertext, string original, string key, string hashName = HashHalg.MD5, Encoding encode = null)
		{
			if (encode == null)
			{
				encode = Encoding.UTF8;
			}
			var ciphertext1 = Encrypt(original, key, hashName, encode);
			return string.Compare(ciphertext, ciphertext1) == 0;
		}
	}
}
