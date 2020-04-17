using System;
using System.Security.Cryptography;

namespace lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            string Message = Console.ReadLine();

            // Converted text to byte for checking hash (by char/byte)
            byte[] asciiByteMessage = System.Text.Encoding.ASCII.GetBytes(Message);
            using (SHA1 sha1Provider = new SHA1CryptoServiceProvider())
            {
                byte[] sha1ByteMessage = sha1Provider.ComputeHash(asciiByteMessage);
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

            byte[] encryptedByteMessage;
            byte[] decryptedByteMessage;

            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                try
                {
                    encryptedByteMessage = RSAEncrypt(asciiByteMessage, RSA.ExportParameters(false));
                    Console.Write("Encrypted message: ");
                    Console.WriteLine(ByteArrayToHexString(encryptedByteMessage));

                    decryptedByteMessage = RSADecrypt(encryptedByteMessage, RSA.ExportParameters(true));
                    Console.Write("Decrypted message: ");
                    Console.WriteLine(System.Text.Encoding.ASCII.GetString(decryptedByteMessage));
                }
                catch (CryptographicException e)
                {
                    Console.WriteLine("error: " + e.Message);
                };
            }

            static byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo)
            {
                try
                {
                    byte[] encryptedData;
                    using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                    {
                        RSA.ImportParameters(RSAKeyInfo);
                        encryptedData = RSA.Encrypt(DataToEncrypt, false);
                    }
                    return encryptedData;
                }
                catch (CryptographicException e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }

            static byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo)
            {
                try
                {
                    byte[] decryptedData;
                    using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                    {
                        RSA.ImportParameters(RSAKeyInfo);
                        decryptedData = RSA.Decrypt(DataToDecrypt, false);
                    }
                    return decryptedData;
                }
                catch (CryptographicException e)
                {
                    Console.WriteLine(e.ToString());
                    return null;
                }
            }
        }
    }
}
