﻿using System;
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

            while (true)
            {
                string input = Console.ReadLine();

                if (input == "exit")
                {
                    // Permite que el cliente cierre la conexión de manera ordenada
                    break;
                }
                else if (input == "clear")
                {
                    Console.Clear();
                }
                else if (string.IsNullOrEmpty(input))
                {
                    // Ignora las líneas en blanco
                }
                else
                {
                    SendMessage(input);
                }
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

    static void SendMessage(string message)
    {
        writer.WriteLine(message);
        writer.Flush();
    }
}
