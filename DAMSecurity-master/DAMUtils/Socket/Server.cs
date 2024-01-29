using DAMSecurityLib.Crypto;
using DAMSecurityLib.Data;
using DAMUtils.PDF;
using DAMUtils.PDF.Fake;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DAMSecurityLib.Crypto;
using DAMSecurityLib.Data;
using iText.Kernel.XMP.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;


namespace DAMUtils.Socket
{
    /**
        * This class is the server program that will receive the 
        * public key from the client and send the encrypted PDF back to the client     
        */
    class ServerProgram
    {
        static int PORT = 1000;
        static private IPAddress address = IPAddress.Any;
        static private TcpListener listener;
        static List<ClientInfo> connectedClients = new List<ClientInfo>();

        static public PDF.IPDFGenerator PdfGenerator { get; set; } = new PDFGenerator();

        static string CertificatePath = "C:\\Users\\Yosse\\source\\..........pfx";
        static string CertPassword = "1234";
        static string pdfFolder = "GeneratedPDFs";


        /**
            * This........................
            */
        public static void Main()
        {
            // Create a relative folder for storing PDFs
            string currentDirectory = Directory.GetCurrentDirectory();
            string pdfFolderPath = Path.Combine(currentDirectory, pdfFolder);

            if (!Directory.Exists(pdfFolderPath))
            {
                Directory.CreateDirectory(pdfFolderPath);
            }

            listener = new TcpListener(address, PORT);
            listener.Start();
            Console.WriteLine("Server started on port " + PORT);

            // Hook into the application closing event to delete the PDF folder
            AppDomain.CurrentDomain.ProcessExit += (s, e) =>
            {
                if (Directory.Exists(pdfFolderPath))
                {
                    Directory.Delete(pdfFolderPath, true);
                    Console.WriteLine($"Deleted PDF folder: {pdfFolderPath}");
                }
            };

            // Continuously accept clients and handle them in separate tasks
            while (true)
            {
                TcpClient clientTcp = listener.AcceptTcpClient();
                Console.WriteLine("Connection established with the client");

                // Store client information in a list
                ClientInfo newClient = new ClientInfo(clientTcp, pdfFolderPath);
                connectedClients.Add(newClient);

                // Handle the client in a separate task
                Task.Run(() => HandleClient(newClient));
            }
        }


        /**
            * This method handles the client 
            * by receiving the public key and the requested list
            */
        private static void HandleClient(ClientInfo client)
        {
            try
            {
                // Get the network stream reference of the client
                NetworkStream networkStream = client.TcpClient.GetStream();

                // Initialize the buffer size
                var str = Utils.ReadToString(networkStream);


                if (str.Length > 0)
                {
                    Console.WriteLine("Message received from client: " + str);

                    // Store information in the client object
                    ObjectPair pair = ObjectPair.Deserialize(str);

                    // generate the pdf
                    byte[] pdfBytes = PdfGenerator.Generate(pair.Obj1);

                    // Encrypt pdf and key
                    KeyFilePair kfp = Hybrid.Crypt((RSAParameters)pair.Obj2, pdfBytes);

                    // Send data to the client
                    var json = kfp.Serialize();
                    var sendBytes = Encoding.UTF8.GetBytes(json);

                    networkStream.Write(sendBytes, 0, sendBytes.Length);
                    networkStream.Flush();

                    // Send the encrypted PDF back to the client
                    // Sender(client);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in server listener: {ex.Message}");
            }
        }


        private static async Task<KeyFilePair?> GenerateAndEncryptPdfAsync(ClientInfo client)
        {
            try
            {
                // Get the JSON string asynchronously about the requested list
                string jsonString = await ProcessClientRequestAsync(client);

                // Create PDF
                // CreatePDF createPDF = new CreatePDF();
                /// createPDF.CreatePdf(jsonString, client.pdfPath);

                // Read PDF as byte[]
                byte[] fileBytes = File.ReadAllBytes(client.pdfPath);

                // Public key byte[] to RSAParameters
                RSAParameters publicKeyRSA = RSACrypt.LoadPublicKey(client.PublicKeyByte);

                // Encrypt PDF with AES and encript AES key with RSA to return a KeyFilePair 
                KeyFilePair keyFilePair = Hybrid.Crypt(publicKeyRSA, fileBytes);


                return keyFilePair;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating or encrypting PDF: {ex.Message}");
            }

            return null;
        }

        private static async Task<string?> ProcessClientRequestAsync(ClientInfo client)
        {
            try
            {
                // Get the list of songs asynchronously
                // Apisql api = new Apisql();
                string jsonString = "no content in the PDF yet";

                if (client.RequestedList.Equals("song"))
                {
                    // List<Song> songs = await api.GetSongs();
                    string songs = "petition not created yet";

                    var data = new
                    {
                        Songs = songs
                    };

                    // Serialize the object to a JSON string
                    // jsonString = JsonSerializer.Serialize(data);

                    // Set the PDF file name
                    client.PdfFileName = "songPDF.pdf";
                }

                client.pdfPath = Path.Combine(client.PdfFolderPath, client.PdfFileName);

                return jsonString;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating JSON string: {ex.Message}");
                return null;
            }
        }

        /**
            * This method sends the Key File Pair to the client
            */
        private static void Sender(ClientInfo client)
        {
            try
            {
                string keyFilePairSentString = client.KeyFilePairSent.Serialize();

                using (NetworkStream networkStream = client.TcpClient.GetStream())
                using (StreamWriter writer = new StreamWriter(networkStream, Encoding.ASCII))
                {
                    writer.WriteLine(keyFilePairSentString);
                    writer.Flush();
                }

                Console.WriteLine("KeyFilePair sent to the client successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending KeyFilePair to the client: {ex.Message}");
            }
        }

    }

    // Class to store client information
    public class ClientInfo
    {
        public TcpClient TcpClient { get; }
        public byte[] PublicKeyByte { get; set; }
        public string PdfFolderPath { get; }

        public KeyFilePair KeyFilePairReceived { get; set; }
        public KeyFilePair KeyFilePairSent { get; set; }

        public ObjectPair ObjectPairReceived { get; set; }



        public string PdfFileName { get; set; }
        public string pdfPath { get; set; }

        public string RequestedList { get; set; }


        public ClientInfo(TcpClient tcpClient, string pdfFolderPath)
        {
            TcpClient = tcpClient;
            PdfFolderPath = pdfFolderPath;
        }
    }
}

