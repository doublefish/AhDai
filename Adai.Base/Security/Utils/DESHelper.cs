using System;
using System.Security.Cryptography;
using System.Text;

namespace Adai.Base.Security.Utils
{
	/// <summary>
	/// DESExt
	/// </summary>
	public static class DESHelper
	{
		/// <summary>
		/// 加密
		/// </summary>
		/// <param name="original">原文</param>
		/// <param name="key">密钥</param>
		/// <param name="mode">运算模式</param>
		/// <param name="iv">解密矢量：只有在CBC解密模式下才适用</param>
		/// <param name="encode">编码</param>
		/// <returns></returns>
		public static string TripleEncrypt(string original, string key, string iv = null, CipherMode mode = CipherMode.CBC, Encoding encode = null)
		{
			if (encode == null)
			{
				encode = Encoding.UTF8;
			}
			var buffer = encode.GetBytes(original);
			using var des = new TripleDESCryptoServiceProvider()
			{
				Key = encode.GetBytes(key),
				Mode = mode,
				Padding = PaddingMode.PKCS7
			};
			if (des.Mode == CipherMode.CBC && !string.IsNullOrEmpty(iv))
			{
				des.IV = encode.GetBytes(iv);
			}
			var encryptor = des.CreateEncryptor();
			var cipher = encryptor.TransformFinalBlock(buffer, 0, buffer.Length);
			return Convert.ToBase64String(cipher);
		}

		/// <summary>
		/// 解密
		/// </summary>
		/// <param name="ciphertext">密文</param>
		/// <param name="key">密钥</param>
		/// <param name="mode">运算模式</param>
		/// <param name="iv">解密矢量：只有在CBC解密模式下才适用</param>
		/// <param name="encode">编码</param>
		/// <returns></returns>
		public static string TripleDecrypt(string ciphertext, string key, string iv = null, CipherMode mode = CipherMode.CBC, Encoding encode = null)
		{
			if (encode == null)
			{
				encode = Encoding.UTF8;
			}
			var buffer = Base.Utils.Base64Helper.ToBytes(ciphertext);
			using var des = new TripleDESCryptoServiceProvider()
			{
				Key = encode.GetBytes(key),
				Mode = mode
			};
			if (des.Mode == CipherMode.CBC && !string.IsNullOrEmpty(iv))
			{
				des.IV = encode.GetBytes(iv);
			}
			var decryptor = des.CreateDecryptor();
			var cipher = decryptor.TransformFinalBlock(buffer, 0, buffer.Length);
			return encode.GetString(cipher);
		}
	}
}
