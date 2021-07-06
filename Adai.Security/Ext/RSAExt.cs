using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace Adai.Security.Ext
{
	/// <summary>
	/// RSAExt
	/// </summary>
	public static class RSAExt
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
			ciphertext += "====".Substring(0, ciphertext.Length % 4);
			var buffer = Base64Helper.ToBytes(ciphertext);
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
			using (var inputStream = new MemoryStream(data))
			{
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
			using (MemoryStream inputStream = new MemoryStream(data), outputStream = new MemoryStream())
			{
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
			return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent><P>{2}</P><Q>{3}</Q><DP>{4}</DP><DQ>{5}</DQ><InverseQ>{6}</InverseQ><D>{7}</D></RSAKeyValue>",
				  parameters.Modulus != null ? Convert.ToBase64String(parameters.Modulus) : null,
				  parameters.Exponent != null ? Convert.ToBase64String(parameters.Exponent) : null,
				  parameters.P != null ? Convert.ToBase64String(parameters.P) : null,
				  parameters.Q != null ? Convert.ToBase64String(parameters.Q) : null,
				  parameters.DP != null ? Convert.ToBase64String(parameters.DP) : null,
				  parameters.DQ != null ? Convert.ToBase64String(parameters.DQ) : null,
				  parameters.InverseQ != null ? Convert.ToBase64String(parameters.InverseQ) : null,
				  parameters.D != null ? Convert.ToBase64String(parameters.D) : null);
		}


	}
}
