using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static List<TcpClient> clients = new List<TcpClient>();
    static object lockObj = new object();

    static async Task Main()
    {
        TcpListener server = new TcpListener(IPAddress.Any, 49152);
        Console.WriteLine($"La IP és: ");
        server.Start();
        Console.WriteLine("Servidor en ejecución...");

        while (true)
        {
            TcpClient client = await server.AcceptTcpClientAsync();
            Task.Run(() => HandleClient(client));
        }
    }

    static void HandleClient(TcpClient client)
    {
        lock (lockObj)
        {
            clients.Add(client);
        }

        NetworkStream stream = client.GetStream();
        string clientAddress = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
        string clientPort = ((IPEndPoint)client.Client.RemoteEndPoint).Port.ToString();

        Console.WriteLine($"Nuevo cliente conectado desde {clientAddress}:{clientPort}");

        try
        {
            byte[] buffer = new byte[1024];
            int bytesRead;

            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Mensaje recibido de {clientAddress}:{clientPort}: {message}");

                // Reenviar el mensaje al otro cliente (puedes personalizar esta lógica según tus necesidades)
                ReenviarMensajeAlOtroCliente(client, message);
            }
        }
        catch
        {
            Console.WriteLine($"Cliente desconectado desde {clientAddress}:{clientPort}");
        }
        finally
        {
            lock (lockObj)
            {
                clients.Remove(client);
            }

            client.Close();
        }
    }

    static void ReenviarMensajeAlOtroCliente(TcpClient emisor, string mensaje)
    {
        lock (lockObj)
        {
            foreach (var receptor in clients)
            {
                if (receptor != emisor)
                {
                    NetworkStream receptorStream = receptor.GetStream();
                    byte[] buffer = Encoding.UTF8.GetBytes(mensaje);
                    receptorStream.Write(buffer, 0, buffer.Length);
                }
            }
        }
    }
}
