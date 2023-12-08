using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace AhDai.Base.Security.Utils
{
	/// <summary>
	/// X509CertificateHelper
	/// </summary>
	public static class X509CertificateUtil
	{
		/// <summary>
		/// 验证证书链
		/// </summary>
		/// <param name="primaryCertificate"></param>
		/// <param name="additionalCertificates">它预计链的顺序为： ...，INTERMEDIATE2，INTERMEDIATE1（INTERMEDIATE2的签名者），CA（INTERMEDIATE1的签名者）</param>
		/// <returns></returns>
		public static bool VerifyCertificate(X509Certificate2 primaryCertificate, IEnumerable<X509Certificate2> additionalCertificates)
		{
			using var chain = new X509Chain();
			foreach (var cert in additionalCertificates)
			{
				chain.ChainPolicy.ExtraStore.Add(cert);
			}

			// You can alter how the chain is built/validated.
			chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
			chain.ChainPolicy.VerificationFlags = X509VerificationFlags.IgnoreWrongUsage;

			// Do the preliminary validation.
			if (!chain.Build(primaryCertificate))
			{
				return false;
			}

			// Make sure we have the same number of elements.
			if (chain.ChainElements.Count != chain.ChainPolicy.ExtraStore.Count + 1)
			{
				return false;
			}

			// Make sure all the thumbprints of the CAs match up.
			// The first one should be 'primaryCert', leading up to the root CA.
			for (var i = 1; i < chain.ChainElements.Count; i++)
			{
				if (chain.ChainElements[i].Certificate.Thumbprint != chain.ChainPolicy.ExtraStore[i - 1].Thumbprint)
				{
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// 验证证书链
		/// </summary>
		/// <param name="primaryCertificate"></param>
		/// <param name="additionalCertificates">它预计链的顺序为： ...，INTERMEDIATE2，INTERMEDIATE1（INTERMEDIATE2的签名者），CA（INTERMEDIATE1的签名者）</param>
		/// <returns></returns>
		public static bool VerifyCertificate(byte[] primaryCertificate, IEnumerable<byte[]> additionalCertificates)
		{
			var additionalCerts = new List<X509Certificate2>();
			foreach (var cert in additionalCertificates.Select(x => new X509Certificate2(x)))
			{
				additionalCerts.Add(cert);
			}
			var primaryCert = new X509Certificate2(primaryCertificate);
			return VerifyCertificate(primaryCert, additionalCerts);
		}
	}
}
