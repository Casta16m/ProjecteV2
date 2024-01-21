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
            Console.WriteLine($"Client ({clientAddress}) \r\nconnected to chat server.");

            Thread receiveThread = new Thread(ReceiveMessages);
            receiveThread.Start();

            while (true)
            {
                Console.WriteLine($"Write the order:\n");
                string input = Console.ReadLine();

                if (input == "exit")
                {
                    break;
                }
                else if (input == "clear")
                {
                    Console.Clear();
                }
                else if (input == "list")
                {
                    byte[] publicKey = GetPublicKey();

                    SendMessage(publicKey);

                    Console.WriteLine("Missatge Enviat\n");
                }
                else if (string.IsNullOrEmpty(input))
                {
                    // Ignora las líneas en blanco
                }
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
        //client.Connect("172.23.1.88", 55555);
        client.Connect("192.168.100.140", 55555);
        stream = client.GetStream();
        reader = new StreamReader(stream, Encoding.ASCII);
        writer = new StreamWriter(stream, Encoding.ASCII);
    }

    static void ReceiveMessages()
    {
        try
        {
            while (true)
            {
                string message = ReadMessage(reader);
                if (message != null)
                {
                    Console.Write(message);
                    // Procesa el mensaje recibido
                }
                else
                {
                    // Se produjo un error en la lectura del mensaje
                    break;
                }
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error de lectura: {ex.Message}");
        }
    }

    static void SendMessage(byte[] message)
    {
        writer.WriteLine(message);
        writer.Flush();
    }

    static string ReadMessage(StreamReader reader)
    {
        try
        {
            string receivedMessage = reader.ReadLine();
            if (receivedMessage != null)
            {
                // Reemplazar saltos de línea para que se muestren correctamente en la consola
                receivedMessage = receivedMessage.Replace("\n", Environment.NewLine);
            }
            return receivedMessage;
        }
        catch (IOException ex)
        {
            // Maneja el error de lectura, como una desconexión inesperada del cliente.
            Console.WriteLine($"Error de lectura: {ex.Message}");
            return null;
        }
    }

    static byte[] GetPublicKey()
    {

        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            string publicKey = rsa.ToXmlString(false);

            byte[] publicKeyBytes = Encoding.UTF8.GetBytes(publicKey);

            return publicKeyBytes;
        }
    }
}
