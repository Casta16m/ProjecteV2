using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using MusiFy_Lib;
using System.Security.Cryptography;
using IOException = System.IO.IOException;
using SocketServidor;
using com.sun.tools.@internal.ws.resources;

class Program
{
    static List<TcpClient> clients = new List<TcpClient>();
    static object lockObj = new object();


    static async Task Main()
    {
        //TcpListener server = new TcpListener(IPAddress.Parse("172.23.1.88"), 55555);
        TcpListener server = new TcpListener(IPAddress.Parse("192.168.100.140"), 55555);
        server.Start();

        Console.WriteLine("Waiting for connections...\n");

        try
        {
            while (true)
            {
                TcpClient client = await server.AcceptTcpClientAsync();

                // Agrega el cliente a la lista de clientes conectados
                lock (lockObj)
                {
                    clients.Add(client);
                }

                // Inicia un nuevo hilo para manejar las comunicaciones con el 
                await Task.Run(() => HandleClient(client));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Server error: {ex.Message}\n");
        }
        finally
        {
            server.Stop();
        }
    }

    static async Task HandleClient(TcpClient client)
    {
        NetworkStream stream = client.GetStream();
        BinaryReader readerbyte = new BinaryReader(stream);
        string clientAddress = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
        string clientPort = ((IPEndPoint)client.Client.RemoteEndPoint).Port.ToString();

        Console.WriteLine($"Client connected {clientAddress}:{clientPort}\n");

        try
        {
            Console.WriteLine("--1:\n");

            byte[] buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

            Console.WriteLine("--2: \n");

            if (bytesRead == null || bytesRead == 0)
            {
                Console.WriteLine("Message was empty\n");

                return;
            }

            byte[] message = buffer.Take(bytesRead).ToArray();

            while (true)
            {

                Console.WriteLine($"Message received {clientAddress}:{clientPort}: {message} \n");

                CreatePDF CreatePDF = new MusiFy_Lib.CreatePDF();
                EncryptPDF EncryptPDF = new MusiFy_Lib.EncryptPDF();

                // Peticio a la API
                // Guardarem el JSON com a List<string> per crear el PDF (Ja veuré com ho faré)

                // Crear el PDF
                var pdfName = Console.ReadLine(); // No ser com acabar-ho de fer perquè no convenç fer-ho així
                List<string> list = new List<string>(); // Aquesta està malament perquè serà el que em retornar-ha la api
                byte[] pdfCreated = CreatePDF.createpdf(list, pdfName);

                Console.WriteLine("PDF created.");

                // Generar la clau AES
                byte[] AESKeyCreated = EncryptPDF.GetBase64EncodedAesKey();

                Console.WriteLine("AES Key created.");

                // Encriptar clau AES
                RSAParameters RSAParameterKey = EncryptPDF.PassByteToRSAParameters(buffer);
                byte[] EncryptedAESKey = EncryptPDF.EncryptAESKey(AESKeyCreated, RSAParameterKey);

                Console.WriteLine("AES key encrypted.");

                // Encriptar PDF
                RSA RSAKey = EncryptPDF.ConvertParametersToRsa(RSAParameterKey);
                byte[] EncryptedPDF = EncryptPDF.encryptPDF(pdfCreated, RSAKey);

                Console.WriteLine("PDF encrypted.");


            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Client message error {clientAddress}:{clientPort}: {ex.Message} \n");
        }
        finally
        {
            // Elimina el cliente de la lista cuando se desconecta
            lock (lockObj)
            {
                clients.Remove(client);
            }

            Console.WriteLine($"The client is disconnected {clientAddress}:{clientPort} \n");
            client.Close();
        }
    }
}
