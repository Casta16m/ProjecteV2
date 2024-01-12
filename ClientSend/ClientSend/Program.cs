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
    static StreamReader reader;
    static StreamWriter writer;

    static void Main()
    {
        client = new TcpClient();

        try
        {
            ConnectToServer();

            string clientAddress = ((System.Net.IPEndPoint)client.Client.LocalEndPoint).Address.ToString();
            Console.WriteLine($"Client ({clientAddress}) conectado al servidor de chat.");

            string pdfFilePath = Console.ReadLine();

            string publicKey = Console.ReadLine();
            RSAParameters _publicKey = DeserializeRSAParameters(publicKey);

            if (!string.IsNullOrWhiteSpace(pdfFilePath) && publicKey != null)
            {
                if (File.Exists(pdfFilePath))
                {
                    if (IsPdfFile(pdfFilePath))
                    {
                        SendMessage(pdfFilePath, _publicKey);
                    }
                    else
                    {
                        Console.WriteLine("The file is not a PDF.");
                    }
                }
                else
                {
                    Console.WriteLine("The path dont exist.");
                }
            }
            else
            {
                Console.WriteLine("Either field is empty.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error de conexión: {ex.Message}");
        }
        finally
        {
            client.Close();
        }
    }

    static void ConnectToServer()
    {
        const string serverAddress = "localhost";
        const int serverPort = 12345; // Haig de saber quin es el port

        client.Connect(serverAddress, serverPort);
        stream = client.GetStream();
        reader = new StreamReader(stream, Encoding.ASCII);
        writer = new StreamWriter(stream, Encoding.ASCII);
    }

    static void SendMessage(string pdfFile, RSAParameters _publicKey)
    {
        DAMSecurityLib.Crypto.Sign sign;
        try
        {
            sign = new DAMSecurityLib.Crypto.Sign();
            byte[] encryptedPDF = sign.EncryptPDF(pdfFile, _publicKey);

            writer.WriteLine(encryptedPDF);
            writer.Flush();

        } catch (IOException ex) {
            Console.WriteLine($"Error de lectura: {ex.Message}");
        }
    }

    static RSAParameters DeserializeRSAParameters(string json)
    {
        return JsonConvert.DeserializeObject<RSAParameters>(json);
    }

    static bool IsPdfFile(string filePath)
    {
        try
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                // Verificar si el archivo comienza con la firma PDF
                byte[] signature = new byte[4];
                fileStream.Read(signature, 0, 4);
                return Encoding.ASCII.GetString(signature) == "%PDF";
            }
        }
        catch (IOException)
        {
            // Manejar la excepción si hay problemas al leer el archivo
            return false;
        }
    }
}
