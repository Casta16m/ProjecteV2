using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MusiFy_Lib
{
    public class EncryptPDF
    {
        /// <summary>
        /// Save the public key from a certificate to a file
        /// </summary>
        /// <param name="pfxFilename"></param>
        /// <param name="pfxPassword"></param>
        /// <param name="publicKeyFile"></param>
        public static void SavePublicKey(string pfxFilename, string pfxPassword, string publicKeyFile)
        {
            X509Certificate2 certificate = new X509Certificate2(pfxFilename, pfxPassword);

            RSA? publicKey = certificate.GetRSAPublicKey();
            if (publicKey != null)
            {
                SavePublicKey(publicKey, publicKeyFile);
            }
        }

        /// <summary>
        /// Load the public key from a file
        /// </summary>
        /// <param name="publicKeyFile"></param>
        /// <returns></returns>
        public static RSA LoadPublicKey(string publicKeyFile)
        {
            RSAParameters publicKeyParams = new RSAParameters();
            using (StreamReader reader = new StreamReader(publicKeyFile))
            {
                string? line = reader.ReadLine();
                if (line != null)
                    publicKeyParams.Modulus = Convert.FromBase64String(line);
                line = reader.ReadLine();
                if (line != null)
                    publicKeyParams.Exponent = Convert.FromBase64String(line);
            }

            RSA rsa = RSA.Create();
            rsa.ImportParameters(publicKeyParams);
            return rsa;
        }

        /// <summary>
        /// Save the public key to a file
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="publicKeyFile"></param>
        public static void SavePublicKey(RSA publicKey, string publicKeyFile)
        {
            RSAParameters publickeyParams = publicKey.ExportParameters(false);
            using (StreamWriter writer = new StreamWriter(publicKeyFile))
            {
                byte[]? modulus = publickeyParams.Modulus;
                if (modulus != null)
                    writer.WriteLine(Convert.ToBase64String(modulus));
                byte[]? exponent = publickeyParams.Exponent;
                if (exponent != null)
                    writer.WriteLine(Convert.ToBase64String(exponent));
            }
        }

        /// <summary>
        /// Encrypt the AES key with the public key
        /// </summary>
        /// <param name="aesKey"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public static byte[] EncryptAESKey(byte[] aesKey, RSAParameters publicKey)
        {
            using (RSA rsa = RSA.Create())
            {
                rsa.ImportParameters(publicKey);
                byte[] encryptedAesKey = rsa.Encrypt(aesKey, RSAEncryptionPadding.OaepSHA256);
                return encryptedAesKey;
            }

        }

        /// <summary>
        /// Decrypt the AES key with the private key
        /// </summary>
        /// <param name="encrypteKey"></param>
        /// <param name="certificate"></param>
        /// <returns></returns>
        public static byte[] DecryptAESKeyWithPrivateKey(byte[] encrypteKey, X509Certificate2 certificate)
        {
            using (RSA? rsa = certificate.GetRSAPrivateKey())
            {
                byte[] aesKey;
                if (rsa != null)
                    aesKey = rsa.Decrypt(encrypteKey, RSAEncryptionPadding.OaepSHA256);
                else
                    aesKey = new byte[0];
                return aesKey;

            }

        }

        /// <summary>
        /// Encrypt the PDF content directly using RSA
        /// </summary>
        /// <param name="pdfBytes"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public static byte[] encryptPDF(byte[] pdfBytes, RSA publicKey)
        {
            // Encrypt the PDF content directly using RSA
            byte[] encryptedPDF = publicKey.Encrypt(pdfBytes, RSAEncryptionPadding.OaepSHA256);
            return encryptedPDF;
        }

        /// <summary>
        /// Decrypt the PDF content directly using RSA
        /// </summary>
        /// <returns></returns>
        public static byte[] GenerateAesKeyBytes()
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.GenerateKey();
                return aesAlg.Key;
            }
        }

        /// <summary>
        /// Generate a random AES key
        /// </summary>
        /// <returns></returns>
        public static byte[] GetBase64EncodedAesKey()
        {
            byte[] aesKey = GenerateAesKeyBytes();
            return aesKey;
            //return Convert.ToBase64String(aesKey);
        }
    }
}
