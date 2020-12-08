using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace UI
{
    /// <summary>
    /// Класс для простого шифрования текстов.
    /// </summary>
    public class Crypto
    {
        const int LenKey = 8;
        const int LenCon = 6;
        private static string Generyt()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            int a;
            string g = "";
            const string ASG = "+jvdk49+2ghjYJKWPMCDfio3u4/ghJHDkjY720q73hgf7hu23w9axz/74my" +
                "HhjQPqofUS7G83b51045+Q46fbG234HhjFghjUYuifg9WweAYCnj";
            for (int i = 0; i < LenKey; i++)
            {
                a = rnd.Next(0, ASG.Length);
                g += ASG[a];
            }
            return g;
        }
        private static readonly byte[] Salt = Encoding.ASCII.GetBytes("o6806642kbM7c5");

        /// <summary>
        /// Метод шифрования.
        /// </summary>
        public static string Encrypt(string plainText)
        {
            //if (string.IsNullOrEmpty(plainText))
            //    throw new ArgumentNullException(plainText);
            string sharedSecret = Generyt();
            string outStr;                       // Encrypted string to return
            RijndaelManaged aesAlg = null;              // RijndaelManaged object used to encrypt the data.

            try
            {
                // generate the key from the shared secret and the salt
                var key = new Rfc2898DeriveBytes(sharedSecret, Salt);

                // Create a RijndaelManaged object
                aesAlg = new RijndaelManaged();
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);

                // Create a decrypt to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (var msEncrypt = new MemoryStream())
                {
                    // prepends the IV
                    msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
                    msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                    }
                    outStr = Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            // Return the encrypted bytes from the memory stream.
            string ad, ag;
            ad = outStr.Substring(0, outStr.Length - LenCon);
            ag = outStr.Substring(outStr.Length - LenCon, LenCon);
            return ad + sharedSecret + ag;
        }

        /// <summary>
        /// Метод расшифровывания.
        /// </summary>
        public static string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText)) return "";
            if (cipherText.Length < (LenKey + LenCon)) return "";
            string sharedSecret = cipherText.Substring(cipherText.Length - (LenKey + LenCon), LenKey);
            string ad, ag;
            ad = cipherText.Substring(0, cipherText.Length - (LenKey + LenCon));
            ag = cipherText.Substring(cipherText.Length - LenCon, LenCon);
            cipherText = ad + ag;
            // Declare the RijndaelManaged object
            // used to decrypt the data.
            RijndaelManaged aesAlg = null;

            // Declare the string used to hold
            // the decrypted text.
            string plaintext;

            try
            {
                // generate the key from the shared secret and the salt
                var key = new Rfc2898DeriveBytes(sharedSecret, Salt);

                // Create the streams used for decryption.                
                byte[] bytes = Convert.FromBase64String(cipherText);
                using (var msDecrypt = new MemoryStream(bytes))
                {
                    // Create a RijndaelManaged object
                    // with the specified key and IV.
                    aesAlg = new RijndaelManaged();
                    aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                    // Get the initialization vector from the encrypted stream
                    aesAlg.IV = ReadByteArray(msDecrypt);
                    // Create a decrypt to perform the stream transform.
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }

            catch
            {
                return "";
            }

            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg != null) aesAlg.Clear();
            }

            return plaintext;
        }

        private static byte[] ReadByteArray(Stream s)
        {
            var rawLength = new byte[sizeof(int)];
            if (s.Read(rawLength, 0, rawLength.Length) == rawLength.Length)
            {
                var buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
                if (s.Read(buffer, 0, buffer.Length) != buffer.Length)
                {
                    throw new SystemException("Did not read byte array properly");
                }

                return buffer;
            }

            throw new SystemException("Stream did not contain properly formatted byte array");
        }
    }
}