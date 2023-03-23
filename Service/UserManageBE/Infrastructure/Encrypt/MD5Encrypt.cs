using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Encrypt
{
    /// <summary>
    ///   <br />
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// longpv 3/22/2023 created
    /// </Modified>
    public class MD5Encrypt : IMD5Encrypt
    {
        /// <summary>Encrypts the password.</summary>
        /// <param name="password">The password.</param>
        /// <returns>
        ///   Trả về đoạn mã MD5 sau khi encode
        /// </returns>
        /// <Modified>
        /// Name Date Comments
        /// longpv 3/22/2023 created
        /// </Modified>
        public string EncryptPassword(string password)
        {
            //encoded đoạn password input
            MD5 md5 = new MD5CryptoServiceProvider();
            var originalBytes = Encoding.Default.GetBytes(password);
            var encodedBytes = md5.ComputeHash(originalBytes);
            return BitConverter.ToString(encodedBytes).Replace("-", "");
        }
    }
}
