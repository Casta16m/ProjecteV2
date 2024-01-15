using DAMSecurityLib.Crypto;
using iText.Kernel.Pdf.Xobject;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

class Program
{
    static TcpClient client;
    static NetworkStream stream;
    static BinaryWriter binaryWriter;

    static void Main()
    {
        client = new TcpClient();

        try
        {
            ConnectToServer();

            string clientAddress = ((System.Net.IPEndPoint)client.Client.LocalEndPoint).Address.ToString();
            Console.WriteLine($"Client ({clientAddress}) is connected to server.");

            string pdfFilePath = Console.ReadLine();

            string publicKey = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(pdfFilePath) && publicKey != null)
            {
                if (File.Exists(pdfFilePath))
                {

                        byte[] encryptedPDF;

                        encryptedPDF = EncryptFunction(pdfFilePath, publicKey);

                        SendMessage(encryptedPDF);

                }
                else
                {
                    Console.WriteLine("The path doesn't exist.");
                }
            }
            else
            {
                Console.WriteLine("Some of the fields are empty.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Connection error: {ex.Message}");
        }
        finally
        {
            client.Close();
        }
    }

    static void ConnectToServer()
    {
        const string serverAddress = "localhost";
        const int serverPort = 49152; // Em posaba que aquest port no s'utilitzaba

        client.Connect(serverAddress, serverPort);
        stream = client.GetStream();
        binaryWriter = new BinaryWriter(stream);
    }

    static byte[] EncryptFunction(string pdfFile, string publicKey)
    {
        try
        {
            string publicKeyFilePath = publicKey;
            RSA _publicKey = RSACrypt.LoadPublicKey(publicKeyFilePath);

            string pdfFilePath = pdfFile; // Ruta del PDF que s'enviarà
            byte[] pdfBytes = File.ReadAllBytes(pdfFilePath);

            // Xifrar el PDF amb la clau publica
            byte[] encryptedPDF = RSACrypt.EncryptPDF(pdfBytes, _publicKey);

            return encryptedPDF;

        }
        catch (IOException ex)
        {
            Console.WriteLine($"Read error: {ex.Message}");
            return null;
        }
    }

    static void SendMessage(byte[] encryptedPDF)
    {
        try
        {
            binaryWriter.Write(encryptedPDF);
            binaryWriter.Flush();

        } catch (IOException ex)
        {
            Console.WriteLine($"Error de lectura: {ex.Message}");
        }
    }
}
