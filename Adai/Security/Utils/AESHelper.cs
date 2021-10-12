using System;
using System.Security.Cryptography;
using System.Text;

namespace Adai.Security.Utils
{
	/// <summary>
	/// AESHelper
	/// </summary>
	public static class AESHelper
	{
		/// <summary>
		/// 加密
		/// </summary>
		/// <param name="original">原文</param>
		/// <param name="key">密钥</param>
		/// <param name="iv">解密矢量：只有在CBC解密模式下才适用</param>
		/// <param name="paddingMode">填充模式</param>
		/// <param name="stringType">返回字符串类型</param>
		/// <param name="encode">编码</param>
		/// <returns></returns>
		public static string RijndaelEncrypt(string original, string key, string iv, PaddingMode paddingMode = PaddingMode.PKCS7, StringType stringType = StringType.Base64, Encoding encode = null)
		{
			if (encode == null)
			{
				encode = Encoding.UTF8;
			}
			var buffer = encode.GetBytes(original);
			using var rm = new RijndaelManaged()
			{
				Key = encode.GetBytes(key),
				IV = encode.GetBytes(iv),
				Mode = CipherMode.CBC,
				Padding = paddingMode
			};
			var encryptor = rm.CreateEncryptor();
			var bytes = encryptor.TransformFinalBlock(buffer, 0, buffer.Length);
			return stringType switch
			{
				StringType.Hex => Adai.Utils.HexHelper.ToHexString(bytes),
				_ => Convert.ToBase64String(bytes),
			};
		}

		/// <summary>
		/// 解密
		/// </summary>
		/// <param name="ciphertext">密文</param>
		/// <param name="key">密钥</param>
		/// <param name="iv">解密矢量：只有在CBC解密模式下才适用</param>
		/// <param name="paddingMode">填充模式</param>
		/// <param name="stringType">字符串类型</param>
		/// <param name="encode">编码</param>
		/// <returns></returns>
		public static string RijndaelDecrypt(string ciphertext, string key, string iv, PaddingMode paddingMode = PaddingMode.PKCS7, StringType stringType = StringType.Base64, Encoding encode = null)
		{
			if (encode == null)
			{
				encode = Encoding.UTF8;
			}

			var buffer = stringType switch
			{
				StringType.Hex => Adai.Utils.HexHelper.ToBytes(ciphertext),
				_ => Adai.Utils.Base64Helper.ToBytes(ciphertext),
			};
			using var rm = new RijndaelManaged()
			{
				Key = encode.GetBytes(key),
				IV = encode.GetBytes(iv),
				Mode = CipherMode.CBC,
				Padding = paddingMode
			};
			var decryptor = rm.CreateDecryptor();
			var cipher = decryptor.TransformFinalBlock(buffer, 0, buffer.Length);
			return encode.GetString(cipher);
		}
	}
}
