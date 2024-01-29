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


namespace DAMUtils.Socket
{
    /**
        * This class is the server program that will receive the 
        * public key from the client and send the encrypted PDF back to the client     
        */
    class ServerProgram
    {
        static int PORT = 1000;
        static private IPAddress address = IPAddress.Parse("127.0.0.1");
        static private TcpListener listener;
        static List<ClientInfo> connectedClients = new List<ClientInfo>();

        // This is the PDF generator that will be used to generate the PDF
        static public PDF.IPDFGenerator PdfGenerator { get; set; } = new PDFGenerator();
        static string pdfFolder = "GeneratedPDFs";

        // Certificate path and password
        static string CertificatePath = "C:\\Users\\yosse\\Dropbox\\PC\\Desktop\\certificatsDigitals\\yossef_CD.pfx";
        static string CertPassword = "1234";
        static X509Certificate2 certificate2 = new X509Certificate2(CertificatePath, CertPassword);



        /// <summary>
        /// This is the main method of the server program
        /// </summary>
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
                byte[] pdfBytesSigned = Sign.SignDocument(certificate2, pdfBytes);

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
                    // ApiSong apiSong = new ApiSong();
                    // List<Song> songs = await api.GetSongs();
                    string songs = "petition songs not created yet";

                    var data = new
                    {
                        Songs = songs
                    };

                    // Serialize the object to a JSON string
                    jsonString = JsonConvert.SerializeObject(data);
                } 
                else if (requestType.Equals("artista"))
                {
                    // ApiArtist apiArtist = new ApiArtist();
                    // List<Artist> artists = await api.GetArtists();
                    string artists = "petition artists not created yet";

                    var data = new
                    {
                        Artists = artists
                    };

                    // Serialize the object to a JSON string
                    jsonString = JsonConvert.SerializeObject(data);
                } 
                else if (requestType.Equals("instrument"))
                {
                    // ApiInstrument apiInstrument = new ApiInstrument();
                    // List<Instrument> instruments = await api.GetInstruments();
                    string instruments = "petition instruments not created yet";

                    var data = new
                    {
                        Instruments = instruments
                    };

                    // Serialize the object to a JSON string
                    jsonString = JsonConvert.SerializeObject(data);
                }
                else if (requestType.Equals("album"))
                {
                    // ApiAlbum apiAlbum = new ApiAlbum();
                    // List<Album> albums = await api.GetAlbums();
                    string albums = "petition albums not created yet";

                    var data = new
                    {
                        Albums = albums
                    };

                    // Serialize the object to a JSON string
                    jsonString = JsonConvert.SerializeObject(data);
                }
                else if (requestType.Equals("grup"))
                {
                    // ApiGroup apiGroup = new ApiGroup();
                    // List<Group> groups = await api.GetGroups();
                    string groups = "petition groups not created yet";

                    var data = new
                    {
                        Groups = groups
                    };

                    // Serialize the object to a JSON string
                    jsonString = JsonConvert.SerializeObject(data);
                }
                else if (requestType.Equals("extensio"))
                {
                    // ApiExtension apiExtension = new ApiExtension();
                    // List<Extension> extensions = await api.GetExtensions();
                    string extensions = "petition extensions not created yet";

                    var data = new
                    {
                        Extensions = extensions
                    };

                    // Serialize the object to a JSON string
                    jsonString = JsonConvert.SerializeObject(data);
                }
                else if (requestType.Equals("participa"))
                {
                    // ApiParticipation apiParticipation = new ApiParticipation();
                    // List<Participation> participations = await api.GetParticipations();
                    string participations = "petition participations not created yet";

                    var data = new
                    {
                        Participations = participations
                    };

                    // Serialize the object to a JSON string
                    jsonString = JsonConvert.SerializeObject(data);
                }
                else if (requestType.Equals("artistaGrup"))
                {
                    // ApiArtistGroup apiArtistGroup = new ApiArtistGroup();
                    // List<ArtistGroup> artistGroups = await api.GetArtistGroups();
                    string artistGroups = "petition artist groups not created yet";

                    var data = new
                    {
                        ArtistGroups = artistGroups
                    };

                    // Serialize the object to a JSON string
                    jsonString = JsonConvert.SerializeObject(data);
                }
                else if (requestType.Equals("songAlbum"))
                {
                    // ApiSongAlbum apiSongAlbum = new ApiSongAlbum();
                    // List<SongAlbum> songAlbums = await api.GetSongAlbums();
                    string songAlbums = "petition song albums not created yet";

                    var data = new
                    {
                        SongAlbums = songAlbums
                    };

                    // Serialize the object to a JSON string
                    jsonString = JsonConvert.SerializeObject(data);
                }
                else if (requestType.Equals("historial"))
                {
                    // ApiHistory apiHistory = new ApiHistory();
                    // List<History> histories = await api.GetHistories();
                    string histories = "petition histories not created yet";

                    var data = new
                    {
                        Histories = histories
                    };

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

