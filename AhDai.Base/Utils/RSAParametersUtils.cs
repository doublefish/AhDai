using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace AhDai.Base.Utils
{
    /// <summary>
    /// RSAParametersUtils
    /// </summary>
    public static class RSAParametersUtils
    {
        /// <summary>
        /// FromXmlString
        /// </summary>
        /// <param name="xmlString"></param>
        public static RSAParameters FromXmlString(string xmlString)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            if (xmlDoc.DocumentElement.Name != "RSAKeyValue") throw new ArgumentException("无效的Xml格式的RSA密钥");

            var parameters = new RSAParameters();
            foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
            {
                switch (node.Name)
                {
                    case "Modulus": parameters.Modulus = string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText); break;
                    case "Exponent": parameters.Exponent = string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText); break;
                    case "D": parameters.D = string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText); break;
                    case "P": parameters.P = string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText); break;
                    case "Q": parameters.Q = string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText); break;
                    case "DP": parameters.DP = string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText); break;
                    case "DQ": parameters.DQ = string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText); break;
                    case "InverseQ": parameters.InverseQ = string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText); break;
                }
            }
            return parameters;
        }

        /// <summary>
        /// ToXmlString
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string ToXmlString(RSAParameters parameters)
        {
            var Modulus = parameters.Modulus != null ? Convert.ToBase64String(parameters.Modulus) : null;
            var Exponent = parameters.Exponent != null ? Convert.ToBase64String(parameters.Exponent) : null;
            var D = parameters.D != null ? Convert.ToBase64String(parameters.D) : null;
            var P = parameters.P != null ? Convert.ToBase64String(parameters.P) : null;
            var Q = parameters.Q != null ? Convert.ToBase64String(parameters.Q) : null;
            var DP = parameters.DP != null ? Convert.ToBase64String(parameters.DP) : null;
            var DQ = parameters.DQ != null ? Convert.ToBase64String(parameters.DQ) : null;
            var InverseQ = parameters.InverseQ != null ? Convert.ToBase64String(parameters.InverseQ) : null;
            return $"<RSAKeyValue><Modulus>{Modulus}</Modulus><Exponent>{Exponent}</Exponent><D>{D}</D><P>{P}</P><Q>{Q}</Q><DP>{DP}</DP><DQ>{DQ}</DQ><InverseQ>{InverseQ}</InverseQ></RSAKeyValue>";
        }

        /// <summary>
        /// FromPemPublicKey
        /// </summary>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public static RSAParameters FromPemPublicKey(string publicKey)
        {
            var buffer = Base64Util.ToBytes(publicKey);
            byte[] modulus, exponent;
            if (buffer.Length == 162)
            {
                modulus = new byte[128];
                exponent = new byte[3];
                Array.Copy(buffer, 29, modulus, 0, 128);
                Array.Copy(buffer, 159, exponent, 0, 3);
            }
            else if (buffer.Length == 294)
            {
                modulus = new byte[256];
                exponent = new byte[3];
                Array.Copy(buffer, 33, modulus, 0, 256);
                Array.Copy(buffer, 291, exponent, 0, 3);
            }
            else
            {
                throw new ArgumentException("公钥格式错误");
            }
            return new RSAParameters()
            {
                Exponent = exponent,
                Modulus = modulus
            };
        }

        /// <summary>
        /// FromPemPrivateKey
        /// </summary>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static RSAParameters FromPemPrivateKey(string privateKey)
        {
            var buffer = Base64Util.ToBytes(privateKey) ?? throw new ArgumentException("私钥转换失败");
            var ms = new MemoryStream(buffer);
            var br = new BinaryReader(ms);
            try
            {
                byte bt = 0;
                ushort twobytes = 0;

                // 读取数据的前两个字节
                twobytes = br.ReadUInt16();
                if (twobytes == 0x8130)
                    br.ReadByte();
                else if (twobytes == 0x8230)
                    br.ReadInt16();
                else
                    throw new ArgumentException("读取 br.ReadUInt16() 时遇到意外值");

                // 读取版本号
                twobytes = br.ReadUInt16();
                if (twobytes != 0x0102)
                    throw new ArgumentException("意外的版本");

                bt = br.ReadByte();
                if (bt != 0x00)
                    throw new Exception("读取 br.ReadByte() 时遇到意外值");

                return new RSAParameters()
                {
                    Modulus = br.ReadBytes(GetIntegerSize(br)),
                    Exponent = br.ReadBytes(GetIntegerSize(br)),
                    D = br.ReadBytes(GetIntegerSize(br)),
                    P = br.ReadBytes(GetIntegerSize(br)),
                    Q = br.ReadBytes(GetIntegerSize(br)),
                    DP = br.ReadBytes(GetIntegerSize(br)),
                    DQ = br.ReadBytes(GetIntegerSize(br)),
                    InverseQ = br.ReadBytes(GetIntegerSize(br))
                };
            }
            finally
            {
                br.Dispose();
                ms.Dispose();
            }
        }

        /// <summary>
        /// 从x509格式的证书中提取公钥
        /// </summary>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public static RSAParameters FromtX509PublicKey(string publicKey)
        {
            var buffer = Base64Util.ToBytes(publicKey);
            using var cert = new X509Certificate(buffer);
            return FromtX509PublicKey(cert);
        }

        /// <summary>
        /// 从x509格式的证书中提取公钥
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns></returns>
        public static RSAParameters FromtX509PublicKey(X509Certificate certificate)
        {
            var rsakey = certificate.GetPublicKey();
            // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------
            var mem = new MemoryStream(rsakey);
            var binr = new BinaryReader(mem);
            try
            {
                var twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();    //advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();   //advance 2 bytes
                else
                    throw new Exception("公钥解析异常");
                twobytes = binr.ReadUInt16();
                byte lowbyte = 0x00;
                byte highbyte = 0x00;
                if (twobytes == 0x8102) //data read as little endian order (actual data order for Integer is 02 81)
                    lowbyte = binr.ReadByte();  // read next bytes which is bytes in modulus
                else if (twobytes == 0x8202)
                {
                    highbyte = binr.ReadByte(); //advance 2 bytes
                    lowbyte = binr.ReadByte();
                }
                else
                    throw new Exception("公钥解析异常");
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };   //reverse byte order since asn.1 key uses big endian
                int modsize = BitConverter.ToInt32(modint, 0);
                int firstbyte = binr.PeekChar();
                if (firstbyte == 0x00)
                {   //if first byte (highest order) of modulus is zero, don't include it
                    binr.ReadByte();    //skip this null byte
                    modsize -= 1;   //reduce modulus buffer size by 1
                }
                byte[] modulus = binr.ReadBytes(modsize);  //read the modulus bytes
                if (binr.ReadByte() != 0x02)            //expect an Integer for the exponent data
                    throw new Exception("公钥解析异常");
                int expbytes = binr.ReadByte();        // should only need one byte for actual exponent data
                byte[] exponent = binr.ReadBytes(expbytes);
                if (binr.PeekChar() != -1)  // if there is unexpected more data, then this is not a valid asn.1 RSAPublicKey
                    throw new Exception("公钥解析异常");
                // ------- create RSACryptoServiceProvider instance and initialize with public key   -----
                return new RSAParameters()
                {
                    Modulus = modulus,
                    Exponent = exponent
                };
            }
            finally
            {
                binr.Close();
            }
        }

        /// <summary>
        /// GetIntegerSize
        /// </summary>
        /// <param name="binaryReader"></param>
        /// <returns></returns>
        public static int GetIntegerSize(BinaryReader binaryReader)
        {
            var bt = binaryReader.ReadByte();
            if (bt != 0x02) //expect integer
            {
                return 0;
            }
            bt = binaryReader.ReadByte();
            int count;
            if (bt == 0x81)
            {
                count = binaryReader.ReadByte(); //data size in next byte
            }
            else if (bt == 0x82)
            {
                var highByte = binaryReader.ReadByte(); //data size in next 2 bytes
                var lowByte = binaryReader.ReadByte();
                var modint = new byte[] { lowByte, highByte, 0x00, 0x00 };
                count = BitConverter.ToInt32(modint, 0);
            }
            else
            {
                count = bt; //we already have the data size
            }
            while (binaryReader.ReadByte() == 0x00)
            {

                count -= 1; //remove high order zeros in data
            }
            binaryReader.BaseStream.Seek(-1, SeekOrigin.Current); //last ReadByte wasn't a removed zero, so back up a byte
            return count;
        }

    }
}
