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
using Newtonsoft.Json.Linq;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
using iText.Signatures;
using iText.Kernel.Pdf;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Tls;
using Org.BouncyCastle.Ocsp;
using Org.BouncyCastle.Utilities;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;


namespace DAMUtils.Socket
{
    /// <summary>
    /// 
    /// </summary>
    class ServerProgram
    {
        // base url
        static string BaseUrlSql = ConfigurationManager.AppSettings["BaseUrlSql"];
        HttpClient client = new HttpClient();

        static int PORT = 1000;
        static private IPAddress address = IPAddress.Parse("127.0.0.1");
        static private TcpListener listener;
        static List<ClientInfo> connectedClients = new List<ClientInfo>();

        // This is the PDF generator that will be used to generate the PDF
        static public PDF.IPDFGenerator PdfGenerator { get; set; } = new PDFGenerator();
        static string pdfFolder = "GeneratedPDFs";

        // Certificate path and password
        static string CertificatePath = ConfigurationManager.AppSettings["CertificatePath"];
        static string CertPassword = ConfigurationManager.AppSettings["CertPassword"];
        static X509Certificate2 certificate2 = new X509Certificate2(CertificatePath, CertPassword);



        /// <summary>
        /// This is the main method of the server program
        /// </summary>
        public static void Main()
        {
            if (BaseUrlSql == null)
            {
                Console.WriteLine("Error: BaseUrlSql is null");
                return;
            } else
            {
                Console.WriteLine("BaseUrlSql: " + BaseUrlSql);
            }


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
                ClientInfo newClient = new ClientInfo(clientTcp);
                connectedClients.Add(newClient);

                // Handle the client in a separate task
                Task.Run(() => HandleClient(newClient));
            }
        }


        /// <summary>
        /// This method handles the client 
        /// </summary>
        /// <param name="client"> a object of type ClientInfo </param>
        /// <returns></returns>
        private static async Task HandleClient(ClientInfo client)
        {
            try
            {
                // Get the network stream reference of the client
                NetworkStream networkStream = client.TcpClient.GetStream();

                while (client.TcpClient.Connected)
                {
                    Console.WriteLine("Waiting for message from client...");
                    var str = Utils.ReadToString(networkStream);

                    if (str.Length > 0)
                    {
                        Console.WriteLine("Message received from client: " + str);

                        // Store information in the client object
                        ObjectPair pair = ObjectPair.Deserialize(str);

                        RSAParameters rsaParameters = JsonConvert.DeserializeObject<RSAParameters>(pair.Obj2.ToString());

                        // Generate the PDF, encrypt it and convert it to a KeyFilePair object String
                        string json = await ProcessPdf(pair.Obj1, rsaParameters);

                        Console.WriteLine("Process finished, object to return: " + json);
                        // Send data to the client
                        var sendBytes = Encoding.ASCII.GetBytes(json);
                        networkStream.Write(sendBytes, 0, sendBytes.Length);

                        Console.WriteLine("-------------------------------------------- One message sent to the client: ");
                    }
                    else
                    {
                        // Si la longitud del mensaje es 0, la conexión puede haberse cerrado por el cliente
                        Console.WriteLine("Connection closed by the client.");
                        break; // Sal del bucle
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in server listener: {ex.Message}");
            }
            finally
            {
                // Puedes realizar acciones de limpieza o cierre aquí si es necesario
            }
        }

        /// <summary>
        /// Generate the PDF, encrypt it and convert it to a KeyFilePair object String
        /// </summary>
        /// <param name="obj2"></param>
        /// <param name="rsaParameters"></param>
        /// <returns></returns>
        private static async Task<string> ProcessPdf(object obj2, RSAParameters rsaParameters)
        {
            try
            {
                // Get the JSON string asynchronously about the requested list
                string requestType = JsonConvert.SerializeObject(obj2).Replace("\"", "");
                Console.WriteLine("Request type: " + requestType);

                Console.WriteLine("ProcessClientRequestAsync");
                // Get the JSON string asynchronously about the requested list
                string jsonString = await ProcessClientRequestAsync(requestType);

                // Create PDF
                byte[] pdfBytes = PdfGenerator.Generate(jsonString);

                // Sign the document
                // byte[] pdfBytesSigned = Sign.SignDocument(certificate2, pdfBytes);
                byte[] pdfBytesSigned = signPdfBytes(pdfBytes);

                // Encrypt PDF and key
                KeyFilePair kfp = Hybrid.Crypt(rsaParameters, pdfBytesSigned);

                // Send data to the client
                string json = kfp.Serialize();

                return json;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating or encrypting PDF: {ex.Message}");
            }

            
            return "";
        }

        private static byte[] signPdfBytes(byte[] pdfBytes)
        {
            // Supongamos que tienes un directorio donde deseas guardar el archivo temporal
            if (!Directory.Exists(pdfFolder))
            {
                Directory.CreateDirectory(pdfFolder);
            }

            // Combina el directorio de trabajo actual con el directorio temporal y el nombre del archivo
            string outputFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pdfFolder, "signed.pdf");
            Console.WriteLine("outputFilePath: " + outputFilePath);
            Sign sign = new Sign();
            sign.InitCertificate(CertificatePath, CertPassword);
            return sign.SignBytes(pdfBytes, outputFilePath);
        }

        /// <summary>
        /// Process the client request and return the JSON string
        /// </summary>
        /// <param name="requestType"></param>
        /// <returns></returns>
        private static async Task<string> ProcessClientRequestAsync(string requestType)
        {
            try
            {
                // Get the list of songs asynchronously
                string jsonString = "no content in the PDF yet";

                if (requestType.Equals("song"))
                {
                    string data = await RequestApiSqlAsync($"{BaseUrlSql}Song");

                    // Deserialize el JSON a una lista de objetos
                    List<Song> songs = JsonConvert.DeserializeObject<List<Song>>(data);

                    // Obtener los nombres de las canciones y concatenarlos en un solo string con saltos de línea
                    string songNames = string.Join(Environment.NewLine, songs.Select(s => s.NomSong));
                    // obtener los nombres sin saltos de linea:
                    // string songNames = string.Join(", ", songs.Select(s => s.NomSong));

                    // Serialize the object to a JSON string
                    jsonString = songNames;
                } 
                else if (requestType.Equals("artista"))
                {
                    string data = await RequestApiSqlAsync($"{BaseUrlSql}Artista/");


                    // Serialize the object to a JSON string
                    jsonString = JsonConvert.SerializeObject(data);
                } 
                else if (requestType.Equals("instrument"))
                {
                    string data = await RequestApiSqlAsync($"{BaseUrlSql}Instrument/");


                    // Serialize the object to a JSON string
                    jsonString = JsonConvert.SerializeObject(data);
                }
                else if (requestType.Equals("album"))
                {
                    string data = await RequestApiSqlAsync($"{BaseUrlSql}Album/");


                    // Serialize the object to a JSON string
                    jsonString = JsonConvert.SerializeObject(data);
                }
                else if (requestType.Equals("grup"))
                {
                    string data = await RequestApiSqlAsync($"{BaseUrlSql}Grup/");


                    // Serialize the object to a JSON string
                    jsonString = JsonConvert.SerializeObject(data);
                }

                return jsonString;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating JSON string: {ex.Message}");
                return null;
            }
        }


        /// <summary>
        /// Call the API and return the JSON string
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static async Task<string> RequestApiSqlAsync(string url)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response = await client.GetAsync(url);

                        if (response.IsSuccessStatusCode)
                        {
                            string json = await response.Content.ReadAsStringAsync();
                            return json;
                        }
                        else
                        {
                            // Manejar el caso de respuesta no exitosa según tus necesidades
                            return "";
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejar excepciones según tus necesidades
                    return "Error en la peticion";
                }
            }


}

    /// <summary>
    /// Class to store information about a song
    /// </summary>
    public class Song
    {
        public string UID { get; set; }
        public DateTime data { get; set; }
        public string NomSong { get; set; }
        public string Genere { get; set; }
        // Otros campos necesarios
    }

    /// <summary>
    /// This class stores information about the client connected to the server
    /// </summary>
    public class ClientInfo
    {
        public TcpClient TcpClient { get; }

        public ClientInfo(TcpClient tcpClient)
        {
            TcpClient = tcpClient;
        }
    }
}

