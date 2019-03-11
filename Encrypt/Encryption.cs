using NETCore.Encrypt;

namespace Amm.AspNetCore.Encrypt
{
    /// <summary>
    ///   加密类
    /// </summary>
    public static class Encryption
    {
        /// <summary>
        ///   加密
        /// </summary>
        /// <returns></returns>
        public static string DesEncrypt(this string text, string sKey)
        {
            return EncryptProvider.DESEncrypt(text, sKey);
        }

        /// <summary>
        ///  解密
        /// </summary>
        /// <returns></returns>
        public static string Decrypt(this string text, string sKey)
        {
            return EncryptProvider.DESDecrypt(text, sKey);
        }
    }
}
