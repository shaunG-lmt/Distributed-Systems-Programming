using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace DistSysACW.Models
{
    public static class Hashing
    {
        public static string SHA1(string Message)
        {
            byte[] asciiByteMessage = System.Text.Encoding.ASCII.GetBytes(Message);

            using (SHA1 sha1Provider = new SHA1CryptoServiceProvider())
            {
                byte[] sha1ByteMessage = sha1Provider.ComputeHash(asciiByteMessage);
                return ByteArrayToHexString(sha1ByteMessage);
            }
        }

        public static string SHA256(string Message)
        {
            byte[] asciiByteMessage = System.Text.Encoding.ASCII.GetBytes(Message);

            using (SHA256 sha256Provider = new SHA256CryptoServiceProvider())
            {
                byte[] sha256ByteMessage = sha256Provider.ComputeHash(asciiByteMessage);
                return ByteArrayToHexString(sha256ByteMessage);
            }
        }
        // Converts bytes into hexadecimal digits
        static string ByteArrayToHexString(byte[] byteArray)
        {
            string hexString = "";
            if (null != byteArray)
            {
                foreach (byte b in byteArray)
                {
                    hexString += b.ToString("x2");
                }
            }
            return hexString;
        }
    }
}
