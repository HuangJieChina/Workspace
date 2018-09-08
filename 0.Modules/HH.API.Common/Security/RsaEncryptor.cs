using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace HH.API.Common
{
    /// <summary>
    /// RSA加密算法
    /// </summary>
    public class RsaEncryptor
    {
        #region 系统自带的公钥和秘钥 ------
        /// <summary>
        /// RSA公钥
        /// </summary>
        public const string PublicKey = "<RSAKeyValue><Modulus>q+bAj+6iQymiucNNTrGOUXaoIR/tL0L2KO4lh/HU7BsKYFJ2F67giRyJpOjGnGItK1ULevMGnamp1eTZJc0XUQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        /// <summary>
        /// RSA私钥
        /// </summary>
        private const string PrivateKey = "<RSAKeyValue><Modulus>q+bAj+6iQymiucNNTrGOUXaoIR/tL0L2KO4lh/HU7BsKYFJ2F67giRyJpOjGnGItK1ULevMGnamp1eTZJc0XUQ==</Modulus><Exponent>AQAB</Exponent><P>1B3uuSZhVX1r5/HDQw9HlAxTf05mZKkz+DrCw1EFoDc=</P><Q>z3bv2YcwH0BvB55n9ideI++DnVBfdXVqNamsHR4M8Lc=</Q><DP>SKYP0x4QDCWuxXwKMneTPmOSXXHOo/9Hq2cEubyQPrU=</DP><DQ>VfpcfDHiZ5E0clvbic/W292vFcrxRKRcV9DxWz/Q7RE=</DQ><InverseQ>NrlATdv8Eja4DPKJ5ZJd/f49BC4w+btRn7x+QACBixg=</InverseQ><D>lm4FXy9eUdXysAtH8LCSsZlbwjkVL8GydtkIgPHQ+Ze1DB/rCCac+S78bl/+h+QmzJuw1n61rKvZF9G8B/QsvQ==</D></RSAKeyValue>";
        #endregion

        #region 公钥和私钥获取 -----------------------
        /// <summary>
        /// 获取 RSA 公钥私钥
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="privateKey"></param>
        public static void RSACreateKey(ref string publicKey, ref string privateKey)
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(512,
                new CspParameters() { KeyContainerName = "a" });

            publicKey = RSA.ToXmlString(false);
            privateKey = RSA.ToXmlString(true);
        }
        #endregion

        #region 加密 ---------------------------------
        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static string RSAEncrypt(string inputValue)
        {
            return RSAEncrypt(PublicKey, inputValue);
        }

        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static string RSAEncrypt(string publicKey, string inputValue)
        {
            try
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                byte[] cipherbytes;
                rsa.FromXmlString(PublicKey);

                cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(inputValue), false);

                return Convert.ToBase64String(cipherbytes);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        #endregion

        #region 解密 ---------------------------------
        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="inputValue">已加密的字符串</param>
        /// <returns></returns>
        public static string RSADecrypt(string inputValue)
        {
            return RSADecrypt(PrivateKey, inputValue);
        }

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <param name="inputValue">已加密的字符串</param>
        /// <returns></returns>
        public static string RSADecrypt(string privateKey, string inputValue)
        {
            try
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                byte[] cipherbytes;
                rsa.FromXmlString(privateKey);
                cipherbytes = rsa.Decrypt(Convert.FromBase64String(inputValue), false);

                return Encoding.UTF8.GetString(cipherbytes);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
        #endregion
    }
}