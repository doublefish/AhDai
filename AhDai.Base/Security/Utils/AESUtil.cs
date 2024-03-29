﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace AhDai.Base.Security.Utils
{
	/// <summary>
	/// AESHelper
	/// </summary>
	public static class AESUtil
	{
		/// <summary>
		/// 加密
		/// </summary>
		/// <param name="original">原文</param>
		/// <param name="key">密钥</param>
		/// <param name="iv">加密矢量：只有在CBC加密模式下才适用</param>
		/// <param name="mode">加密模式</param>
		/// <param name="paddingMode">填充模式</param>
		/// <param name="stringType">返回字符串类型</param>
		/// <param name="encode">编码</param>
		/// <returns></returns>
		public static string RijndaelEncrypt(string original, string key, string iv, CipherMode mode = CipherMode.CBC, PaddingMode paddingMode = PaddingMode.PKCS7, StringType stringType = StringType.Base64, Encoding encode = null)
		{
			encode ??= Encoding.UTF8;
			var buffer = encode.GetBytes(original);
			using var aes = Aes.Create();
			aes.Key = encode.GetBytes(key);
			aes.IV = encode.GetBytes(iv);
			aes.Mode = mode;
			aes.Padding = paddingMode;
			var encryptor = aes.CreateEncryptor();
			var bytes = encryptor.TransformFinalBlock(buffer, 0, buffer.Length);
			return stringType switch
			{
				StringType.Hex => Base.Utils.HexUtil.ToHexString(bytes),
				_ => Convert.ToBase64String(bytes),
			};
		}

		/// <summary>
		/// 解密
		/// </summary>
		/// <param name="ciphertext">密文</param>
		/// <param name="key">密钥</param>
		/// <param name="iv">加密矢量：只有在CBC加密模式下才适用</param>
		/// <param name="mode">加密模式</param>
		/// <param name="paddingMode">填充模式</param>
		/// <param name="stringType">字符串类型</param>
		/// <param name="encode">编码</param>
		/// <returns></returns>
		public static string RijndaelDecrypt(string ciphertext, string key, string iv, CipherMode mode = CipherMode.CBC, PaddingMode paddingMode = PaddingMode.PKCS7, StringType stringType = StringType.Base64, Encoding encode = null)
		{
			encode ??= Encoding.UTF8;
			var buffer = stringType switch
			{
				StringType.Hex => Base.Utils.HexUtil.ToBytes(ciphertext),
				_ => Base.Utils.Base64Util.ToBytes(ciphertext),
			};
			using var aes = Aes.Create();
			aes.Key = encode.GetBytes(key);
			aes.IV = encode.GetBytes(iv);
			aes.Mode = mode;
			aes.Padding = paddingMode;
			var decryptor = aes.CreateDecryptor();
			var cipher = decryptor.TransformFinalBlock(buffer, 0, buffer.Length);
			return encode.GetString(cipher);
		}
	}
}
