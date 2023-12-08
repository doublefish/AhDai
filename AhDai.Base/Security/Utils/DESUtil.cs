using System;
using System.Security.Cryptography;
using System.Text;

namespace AhDai.Base.Security.Utils
{
	/// <summary>
	/// DESExt
	/// </summary>
	public static class DESUtil
	{
		/// <summary>
		/// 加密
		/// </summary>
		/// <param name="original">原文</param>
		/// <param name="key">密钥</param>
		/// <param name="iv">加密矢量：只有在CBC加密模式下才适用</param>
		/// <param name="mode">运算模式</param>
		/// <param name="paddingMode">填充模式</param>
		/// <param name="encode">编码</param>
		/// <returns></returns>
		public static string TripleEncrypt(string original, string key, string iv = null, CipherMode mode = CipherMode.CBC, PaddingMode paddingMode = PaddingMode.PKCS7, Encoding encode = null)
		{
			if (encode == null)
			{
				encode = Encoding.UTF8;
			}
			var buffer = encode.GetBytes(original);
			using var des = TripleDES.Create();
			des.Key = encode.GetBytes(key);
			if (mode == CipherMode.CBC && !string.IsNullOrEmpty(iv))
			{
				des.IV = encode.GetBytes(iv);
			}
			des.Mode = mode;
			des.Padding = paddingMode;
			var encryptor = des.CreateEncryptor();
			var cipher = encryptor.TransformFinalBlock(buffer, 0, buffer.Length);
			return Convert.ToBase64String(cipher);
		}

		/// <summary>
		/// 解密
		/// </summary>
		/// <param name="ciphertext">密文</param>
		/// <param name="key">密钥</param>
		/// <param name="iv">加密矢量：只有在CBC加密模式下才适用</param>
		/// <param name="mode">运算模式</param>
		/// <param name="paddingMode">填充模式</param>
		/// <param name="encode">编码</param>
		/// <returns></returns>
		public static string TripleDecrypt(string ciphertext, string key, string iv = null, CipherMode mode = CipherMode.CBC, PaddingMode paddingMode = PaddingMode.PKCS7, Encoding encode = null)
		{
			if (encode == null)
			{
				encode = Encoding.UTF8;
			}
			var buffer = Base.Utils.Base64Util.ToBytes(ciphertext);
			using var des = TripleDES.Create();
			des.Key = encode.GetBytes(key);
			if (mode == CipherMode.CBC && !string.IsNullOrEmpty(iv))
			{
				des.IV = encode.GetBytes(iv);
			}
			des.Mode = mode;
			des.Padding = paddingMode;
			var decryptor = des.CreateDecryptor();
			var cipher = decryptor.TransformFinalBlock(buffer, 0, buffer.Length);
			return encode.GetString(cipher);
		}
	}
}
