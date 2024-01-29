using DAMSecurity;
using DAMSecurityLib.Crypto;
using DAMSecurityLib.Data;
using DAMSecurityLib.Exceptions;
using DAMUtils.Socket;
using Org.BouncyCastle.Tls;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DAMSecurity
{
    /// <summary>
    /// This class is the client program that will send the public key to the server and receive the encrypted PDF
    /// </summary>
    class ClientProgram
    {

        // Solicit the certificate and password
        static string CertificatePath = "C:\\Users\\yosse\\Dropbox\\PC\\Desktop\\certificatsDigitals\\yossef_CD.pfx";
        static string CertPassword = "1234";
        static X509Certificate2 Certificate2 = new X509Certificate2(CertificatePath, CertPassword);

        // Create a relative folder for storing PDFs
        static string currentDirectory = Directory.GetCurrentDirectory();
        static string pdfFolderPath = Path.Combine(currentDirectory, "GeneratedPDFs");

        static async Task Main()
        {
            Console.WriteLine("Client program started");
            // Verificar si la carpeta existe, si no, crearla
            if (!Directory.Exists(pdfFolderPath))
            {
                Directory.CreateDirectory(pdfFolderPath);
                Console.WriteLine("PDF folder created: " + pdfFolderPath);
            }

            while (true)
            {                
                try
                {
                    string[] options = ["song", "artista", "instrument", "album", "grup", "extensio", "participa", "artistaGrup", "songAlbum", "historial"];
                    // Solicit the request type
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine("- Welcome to the menu client program -");
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine("| song");
                    Console.WriteLine("| artista");
                    Console.WriteLine("| instrument");
                    Console.WriteLine("| album");
                    Console.WriteLine("| grup");
                    Console.WriteLine("| extensio");
                    Console.WriteLine("| participa");
                    Console.WriteLine("| artistaGrup");
                    Console.WriteLine("| songAlbum");
                    Console.WriteLine("| historial");
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine("Enter the request type: ");
                    string requestType = Console.ReadLine();
                    if(options.Contains(requestType))
                    {
                        Console.WriteLine("Request type accepted: " + requestType);
                        // Send request to the server
                        byte[]? fileDesencrypted = await Process(requestType, Certificate2);

                        if (fileDesencrypted != null)
                        {
                            Console.WriteLine("File desencrypted");

                            // Save the file
                            saveFile(fileDesencrypted, requestType);

                        }
                        else
                        {
                            Console.WriteLine("File not desencrypted");


                        }
                    }
                    else
                    {
                        Console.WriteLine("Request type not valid");
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in client: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// This method saves the file in the GeneratedPDFs folder
        /// </summary>
        /// <param name="fileDesencrypted">File to save</param>
        /// <param name="requestType">Request type to save the file with the correct name</param>
        private static void saveFile(byte[] fileDesencrypted, string requestType)
        {
            byte[] pdfFinal = Sign.SignDocument(Certificate2, fileDesencrypted);

            string pdfPath = Path.Combine(pdfFolderPath, requestType+"PDF.pdf");

            if (File.Exists(pdfPath))
            {
                File.Delete(pdfPath);
            }


            File.WriteAllBytes(pdfPath, pdfFinal);

            Console.WriteLine("File saved to: " + pdfPath);
        }

        /// <summary>
        /// This method sends the Public Key to the server with the request type
        /// </summary>
        /// <param name="reportName">The request type</param>
        /// <param name="certificate">The certificate to encrypt/decrypt</param>
        /// <returns></returns>
        /// <exception cref="IncorrectKeyException"></exception>
        public static async Task<byte[]> Process(string reportName, X509Certificate2 certificate)
        {
            byte[] finalBytes;

            string address = "127.0.0.1";
            int port = 1000;

            using (TcpClient tcpClient = new TcpClient(address, port))
            {
                using (NetworkStream stream = tcpClient.GetStream())
                {
                    Console.WriteLine("Connected to the server.");

                    var publicKey = certificate.GetRSAPublicKey()?.ExportParameters(false);
                    if (publicKey == null)
                    {
                        Console.WriteLine("Public key is invalid");
                        throw new IncorrectKeyException("Public key is invalid");
                    }

                    var objectPair = new ObjectPair(reportName, publicKey);
                    var objectBytes = objectPair.ToBytes();

                    Console.WriteLine("Sending parameters to server...");
                    Console.WriteLine("----------");
                    Console.WriteLine("");
                    Console.WriteLine("Sending json: " + objectPair.Serialize());
                    Console.WriteLine("");
                    Console.WriteLine("----------");

                    // Send parameters to server
                    stream.Write(objectBytes, 0, objectBytes.Length);

                    Console.WriteLine("Waiting for response from server...");

                    // Wait for server response
                    var str = Utils.ReadToString(stream);

                    Console.WriteLine("----------");
                    Console.WriteLine("");
                    Console.WriteLine("Received response from server: " + str);
                    Console.WriteLine("");
                    Console.WriteLine("----------");

                    // Deserialize response and decrypt it
                    KeyFilePair response = KeyFilePair.Deserialize(str);
                    finalBytes = Hybrid.Decrypt(certificate, response);
                }
            }

            // Return decrypted file
            return finalBytes;
        }

    }
}
