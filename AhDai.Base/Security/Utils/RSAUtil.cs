using AhDai.Base.Security.Extensions;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AhDai.Base.Security.Utils
{
    /// <summary>
    /// RSAHelper
    /// </summary>
    public static class RSAUtil
    {
        #region xml

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="original">原文</param>
        /// <param name="key">公钥/私钥</param>
        /// <param name="stringType">返回字符串类型</param>
        /// <param name="encode">编码</param>
        /// <returns></returns>
        public static string EncryptByXml(string original, string key, StringType stringType = StringType.Base64, Encoding encode = null)
        {
            encode ??= Encoding.UTF8;
            using var rsa = new RSACryptoServiceProvider();
            RSAExtensions.FromXmlString(rsa, key);
            var bytes = rsa.Encrypt(original, encode);
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
        /// <param name="key">公钥/私钥</param>
        /// <param name="stringType">密文字符串类型</param>
        /// <param name="encode">编码</param>
        /// <returns></returns>
        public static string DecryptByXml(string ciphertext, string key, StringType stringType = StringType.Base64, Encoding encode = null)
        {
            encode ??= Encoding.UTF8;
            using var rsa = new RSACryptoServiceProvider();
            RSAExtensions.FromXmlString(rsa, key);
            byte[] bytes;
            switch (stringType)
            {
                case StringType.Hex:
                    bytes = Base.Utils.HexUtil.ToBytes(ciphertext);
                    bytes = rsa.Decrypt(bytes);
                    break;
                default:
                    bytes = rsa.Decrypt(ciphertext);
                    break;
            }
            return encode.GetString(bytes);
        }

        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="original">原文</param>
        /// <param name="privateKey">私钥</param>
        /// <param name="hashName">哈希算法名称</param>
        /// <param name="stringType">返回字符串类型</param>
        /// <param name="encode">编码</param>
        /// <returns></returns>
        public static string SignByXml(string original, string privateKey, string hashName = HashHalg.MD5, StringType stringType = StringType.Base64, Encoding encode = null)
        {
            encode ??= Encoding.UTF8;
            var buffer = encode.GetBytes(original);
            using var rsa = new RSACryptoServiceProvider();
            RSAExtensions.FromXmlString(rsa, privateKey);
            var bytes = rsa.SignData(buffer, hashName);
            return stringType switch
            {
                StringType.Hex => Base.Utils.HexUtil.ToHexString(bytes),
                _ => Convert.ToBase64String(bytes),
            };
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="sign">签名</param>
        /// <param name="data">需要验证的数据</param>
        /// <param name="publicKey">公钥</param>
        /// <param name="hashName">哈希算法名称</param>
        /// <param name="stringType">签名字符串类型</param>
        /// <param name="encode">编码</param>
        /// <returns></returns>
        public static bool VerifyByXml(string sign, string data, string publicKey, string hashName = HashHalg.MD5, StringType stringType = StringType.Base64, Encoding encode = null)
        {
            encode ??= Encoding.UTF8;
            var buffer = encode.GetBytes(data);
            var signature = stringType switch
            {
                StringType.Hex => Base.Utils.HexUtil.ToBytes(sign),
                _ => Base.Utils.Base64Util.ToBytes(sign),
            };
            using var rsa = new RSACryptoServiceProvider();
            RSAExtensions.FromXmlString(rsa, publicKey);
            return rsa.VerifyData(buffer, hashName, signature);
        }

        #endregion

        #region pkcs8

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="original">原文</param>
        /// <param name="publicKey">公钥</param>
        /// <param name="stringType">返回字符串类型</param>
        /// <param name="encode">编码</param>
        /// <returns></returns>
        public static string EncryptByPKCS8(string original, string publicKey, StringType stringType = StringType.Base64, Encoding encode = null)
        {
            encode ??= Encoding.UTF8;
            //var buffer = encode.GetBytes(original);
            using var rsa = new RSACryptoServiceProvider();
            rsa.FromPKCS8PublicKey(publicKey);

            var bytes = rsa.Encrypt(original, encode);
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
        /// <param name="privateKey">私钥</param>
        /// <param name="stringType">密文字符串类型</param>
        /// <param name="encode">编码</param>
        /// <returns></returns>
        public static string DecryptByPKCS8(string ciphertext, string privateKey, StringType stringType = StringType.Base64, Encoding encode = null)
        {
            encode ??= Encoding.UTF8;
            using var rsa = new RSACryptoServiceProvider();
            rsa.FromPKCS8PrivateKey(privateKey);
            byte[] bytes;
            switch (stringType)
            {
                case StringType.Hex:
                    bytes = Base.Utils.HexUtil.ToBytes(ciphertext);
                    bytes = rsa.Decrypt(bytes);
                    break;
                case StringType.Base64:
                default:
                    bytes = rsa.Decrypt(ciphertext);
                    break;
            }
            return encode.GetString(bytes);
        }

        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="original">原文</param>
        /// <param name="privateKey">私钥</param>
        /// <param name="hashName">哈希算法名称</param>
        /// <param name="stringType">返回字符串类型</param>
        /// <param name="encode">编码</param>
        /// <returns></returns>
        public static string SignByPKCS8(string original, string privateKey, string hashName = HashHalg.MD5, StringType stringType = StringType.Base64, Encoding encode = null)
        {
            encode ??= Encoding.UTF8;
            var buffer = encode.GetBytes(original);
            using var rsa = new RSACryptoServiceProvider();
            rsa.FromPKCS8PrivateKey(privateKey);
            var bytes = rsa.SignData(buffer, hashName);
            return stringType switch
            {
                StringType.Hex => Base.Utils.HexUtil.ToHexString(bytes),
                _ => Convert.ToBase64String(bytes),
            };
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="sign">签名</param>
        /// <param name="data">需要验证的数据</param>
        /// <param name="publicKey">公钥</param>
        /// <param name="hashName">哈希算法名称</param>
        /// <param name="stringType">签名字符串类型</param>
        /// <param name="encode">编码</param>
        /// <returns></returns>
        public static bool VerifyByPKCS8(string sign, string data, string publicKey, string hashName = HashHalg.MD5, StringType stringType = StringType.Base64, Encoding encode = null)
        {
            encode ??= Encoding.UTF8;
            var buffer = encode.GetBytes(data);
            var signature = stringType switch
            {
                StringType.Hex => Base.Utils.HexUtil.ToBytes(sign),
                _ => Base.Utils.Base64Util.ToBytes(sign),
            };
            using var rsa = new RSACryptoServiceProvider();
            rsa.FromPKCS8PublicKey(publicKey);
            return rsa.VerifyData(buffer, hashName, signature);
        }

        #endregion

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="sign">签名</param>
        /// <param name="data">需要验证的数据</param>
        /// <param name="cert">公钥证书</param>
        /// <param name="hashName">哈希算法名称</param>
        /// <param name="stringType">签名字符串类型</param>
        /// <param name="encode">编码</param>
        /// <returns></returns>
        public static bool VerifyByX509Certificate(string sign, string data, X509Certificate cert, string hashName = HashHalg.MD5, StringType stringType = StringType.Base64, Encoding encode = null)
        {
            encode ??= Encoding.UTF8;
            var buffer = encode.GetBytes(data);
            var signature = stringType switch
            {
                StringType.Hex => Base.Utils.HexUtil.ToBytes(sign),
                _ => Base.Utils.Base64Util.ToBytes(sign),
            };
            using var rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(GetRSAParameters(cert));
            return rsa.VerifyData(buffer, hashName, signature);
        }

        /// <summary>
        /// 转换公钥PEM
        /// </summary>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public static RSAParameters ConvertPKCS8PublicKey(string publicKey)
        {
            var buffer = Base.Utils.Base64Util.ToBytes(publicKey);
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
        /// 转换私钥PKCS8
        /// </summary>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static RSAParameters ConvertPKCS8PrivateKey(string privateKey)
        {
            var buffer = Base.Utils.Base64Util.ToBytes(privateKey) ?? throw new ArgumentException("私钥转换失败");
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
        /// 转换公钥X509
        /// </summary>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public static RSAParameters ConvertX509PublicKey(string publicKey)
        {
            var buffer = Base.Utils.Base64Util.ToBytes(publicKey);
            using var cert = new X509Certificate(buffer);
            return GetRSAParameters(cert);
        }

        /// <summary>
        /// 从x509格式的证书中,提取公钥,并转换成xml格式
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns></returns>
        public static RSAParameters GetRSAParameters(X509Certificate certificate)
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
                int expbytes = (int)binr.ReadByte();        // should only need one byte for actual exponent data
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
            if (bt != 0x02)//expect integer
            {
                return 0;
            }
            bt = binaryReader.ReadByte();
            int count;
            if (bt == 0x81)
            {
                count = binaryReader.ReadByte();//data size in next byte
            }
            else if (bt == 0x82)
            {
                var highByte = binaryReader.ReadByte();//data size in next 2 bytes
                var lowByte = binaryReader.ReadByte();
                var modint = new byte[] { lowByte, highByte, 0x00, 0x00 };
                count = BitConverter.ToInt32(modint, 0);
            }
            else
            {
                count = bt;//we already have the data size
            }
            while (binaryReader.ReadByte() == 0x00)
            {
                //remove high order zeros in data
                count -= 1;
            }
            binaryReader.BaseStream.Seek(-1, SeekOrigin.Current);//last ReadByte wasn't a removed zero, so back up a byte
            return count;
        }





        /// <summary>
        /// FromPKCS8PublicKey
        /// </summary>
        /// <param name="rsa"></param>
        /// <param name="pkcs8String"></param>
        /// <returns></returns>
        public static void FromPKCS8PublicKey(this RSA rsa, string pkcs8String)
        {
            var parameters = ConvertPKCS8PublicKey(pkcs8String);
            rsa.ImportParameters(parameters);
        }

        /// <summary>
        /// FromPKCS8PrivateKey
        /// </summary>
        /// <param name="rsa"></param>
        /// <param name="pkcs8String"></param>
        /// <returns></returns>
        public static void FromPKCS8PrivateKey(this RSA rsa, string pkcs8String)
        {
            var parameters = ConvertPKCS8PrivateKey(pkcs8String);
            rsa.ImportParameters(parameters);
        }
    }
}
