using iText.Kernel.Pdf.Xobject;
using System;
using System.IO;
using System.Net.Sockets;
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
            Console.WriteLine($"Cliente ({clientAddress}) conectado al servidor de chat.");

            string pdfFilePath = Console.ReadLine();

            string password = Console.ReadLine();

            if (pdfFilePath != null)
            {
                // SendMessage(password, pdfFilePath);
                SendMessage(password, pdfFilePath);

                
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
        client.Connect("127.0.0.1", 12345);
        stream = client.GetStream();
        reader = new StreamReader(stream, Encoding.ASCII);
        writer = new StreamWriter(stream, Encoding.ASCII);
    }

    static void SendMessage(string password, string outFile)
    {
        DAMSecurityLib.Crypto.Sign sign;
        try
        {
            sign = new DAMSecurityLib.Crypto.Sign();
            sign.EncryptPdf(password, outFile);

            writer.WriteLine(outFile);
            writer.Flush();

        } catch (IOException ex) {
            Console.WriteLine($"Error de lectura: {ex.Message}");
        }
    }
}
