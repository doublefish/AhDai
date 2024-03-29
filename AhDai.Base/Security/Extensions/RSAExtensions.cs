﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace AhDai.Base.Security.Extensions
{
	/// <summary>
	/// RSAExt
	/// </summary>
	public static class RSAExtensions
	{
		/// <summary>
		/// 加密
		/// </summary>
		/// <param name="rsa"></param>
		/// <param name="original">原文</param>
		/// <param name="encode">编码</param>
		/// <returns></returns>
		public static byte[] Encrypt(this RSACryptoServiceProvider rsa, string original, Encoding encode = null)
		{
			if (encode == null)
			{
				encode = Encoding.UTF8;
			}
			var buffer = encode.GetBytes(original);
			return rsa.Encrypt(buffer);
		}

		/// <summary>
		/// 解密
		/// </summary>
		/// <param name="rsa"></param>
		/// <param name="ciphertext">密文</param>
		/// <returns></returns>
		public static byte[] Decrypt(this RSACryptoServiceProvider rsa, string ciphertext)
		{
			var length = ciphertext.Length % 4;
			ciphertext += "===="[..length];
			var buffer = Base.Utils.Base64Util.ToBytes(ciphertext);
			return rsa.Decrypt(buffer);
		}

		/// <summary>
		/// 分段加密
		/// </summary>
		/// <param name="rsa"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static byte[] Encrypt(this RSACryptoServiceProvider rsa, byte[] data)
		{
			var bufferSize = rsa.KeySize / 8 - 11;
			var buffer = new byte[bufferSize];
			var outputStream = new MemoryStream();
			using var inputStream = new MemoryStream(data);
			while (true)
			{
				//分段加密
				var readSize = inputStream.Read(buffer, 0, bufferSize);
				if (readSize <= 0)
				{
					break;
				}

				var temp = new byte[readSize];
				Array.Copy(buffer, 0, temp, 0, readSize);
				var bytes = rsa.Encrypt(temp, false);
				outputStream.Write(bytes, 0, bytes.Length);
			}
			return outputStream.ToArray();
		}

		/// <summary>
		/// 分段解密
		/// </summary>
		/// <param name="rsa"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static byte[] Decrypt(this RSACryptoServiceProvider rsa, byte[] data)
		{
			var bufferSize = rsa.KeySize / 8;
			var buffer = new byte[bufferSize];
#if NET5_0_OR_GREATER
			using MemoryStream inputStream = new(data), outputStream = new();
#else
			using MemoryStream inputStream = new MemoryStream(data), outputStream = new MemoryStream();
#endif

			while (true)
			{
				//分段加密
				var readSize = inputStream.Read(buffer, 0, bufferSize);
				if (readSize <= 0)
				{
					break;
				}

				var temp = new byte[readSize];
				Array.Copy(buffer, 0, temp, 0, readSize);
				var bytes = rsa.Decrypt(temp, false);
				outputStream.Write(bytes, 0, bytes.Length);
			}
			return outputStream.ToArray();
		}

		/// <summary>
		/// FromXmlString
		/// </summary>
		/// <param name="rsa"></param>
		/// <param name="xmlString"></param>
		public static void FromXmlString(this RSA rsa, string xmlString)
		{
			var xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(xmlString);

			if (xmlDoc.DocumentElement.Name != "RSAKeyValue")
			{
				throw new ArgumentException("Invalid XML RSA key.");
			}

			var parameters = new RSAParameters();
			foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
			{
				switch (node.Name)
				{
					case "Modulus": parameters.Modulus = string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText); break;
					case "Exponent": parameters.Exponent = string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText); break;
					case "P": parameters.P = string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText); break;
					case "Q": parameters.Q = string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText); break;
					case "DP": parameters.DP = string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText); break;
					case "DQ": parameters.DQ = string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText); break;
					case "InverseQ": parameters.InverseQ = string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText); break;
					case "D": parameters.D = string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText); break;
				}
			}
			rsa.ImportParameters(parameters);
		}

		/// <summary>
		/// ToXmlString
		/// </summary>
		/// <param name="rsa"></param>
		/// <param name="includePrivateParameters"></param>
		/// <returns></returns>
		public static string ToXmlString(this RSA rsa, bool includePrivateParameters)
		{
			var parameters = rsa.ExportParameters(includePrivateParameters);
			return parameters.ToXmlString();
		}

		/// <summary>
		/// ToXmlString
		/// </summary>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static string ToXmlString(this RSAParameters parameters)
		{
			var Modulus = parameters.Modulus != null ? Convert.ToBase64String(parameters.Modulus) : null;
			var Exponent = parameters.Exponent != null ? Convert.ToBase64String(parameters.Exponent) : null;
			var P = parameters.P != null ? Convert.ToBase64String(parameters.P) : null;
			var Q = parameters.Q != null ? Convert.ToBase64String(parameters.Q) : null;
			var DP = parameters.DP != null ? Convert.ToBase64String(parameters.DP) : null;
			var DQ = parameters.DQ != null ? Convert.ToBase64String(parameters.DQ) : null;
			var InverseQ = parameters.InverseQ != null ? Convert.ToBase64String(parameters.InverseQ) : null;
			var D = parameters.D != null ? Convert.ToBase64String(parameters.D) : null;
			return $"<RSAKeyValue><Modulus>{Modulus}</Modulus><Exponent>{Exponent}</Exponent><P>{P}</P><Q>{Q}</Q><DP>{DP}</DP><DQ>{DQ}</DQ><InverseQ>{InverseQ}</InverseQ><D>{D}</D></RSAKeyValue>";
		}
	}
}
