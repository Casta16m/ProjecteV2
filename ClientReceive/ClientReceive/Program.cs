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

    static void Main()
    {
        client = new TcpClient();

        try
        {
            ConnectToServer();

            string clientAddress = ((System.Net.IPEndPoint)client.Client.LocalEndPoint).Address.ToString();
            Console.WriteLine($"Cliente ({clientAddress}) conectado al servidor de chat.");

            // Obtener el flujo de red del servidor
            NetworkStream stream = client.GetStream();

            // Iniciar un hilo para la lectura de datos del servidor
            Thread receiveThread = new Thread(() => ReceiveMessages(stream));
            receiveThread.Start();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error de conexión: {ex.Message}");
        }
    }

    static void ConnectToServer()
    {
        client.Connect("localhost", 41305);
        stream = client.GetStream();
        reader = new StreamReader(stream, Encoding.ASCII);
    }

    static void ReceiveMessage()
    {

    }
}
