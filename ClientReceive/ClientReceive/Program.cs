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

            Thread receiveThread = new Thread(ReceiveMessages);
            receiveThread.Start();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error de conexión: {ex.Message}");
        }
    }

    static void ConnectToServer()
    {
        client.Connect("127.0.0.1", 12345);
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
                    

                    //Console.Write(message);
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
}
