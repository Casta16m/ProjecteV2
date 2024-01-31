using com.sun.corba.se.impl.protocol.giopmsgheaders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data;
using javax.xml.crypto;
namespace MusiFy_Lib
{
    public class Reports
    {

        /// <summary>
        /// Asynchronously retrieves a list of data from a specified API endpoint and deserializes it into a generic List.
        /// </summary>
        /// <typeparam name="T">Type of data to be deserialized.</typeparam>
        /// <param name="apiUrl">URL of the API endpoint.</param>
        /// <returns>A Task representing the asynchronous operation with a List of deserialized data.</returns>
        public async Task<List<T>?> GetData<T>(string apiUrl)
        {

            MusiFyApi apiCall = new MusiFyApi(apiUrl);
            string apiResponse = await apiCall.ObtenerDatosAsync();

            List<T> dataList = JsonSerializer.Deserialize<List<T>>(apiResponse);

            return dataList;

        }

        /// <summary>
        /// Asynchronously retrieves a single piece of data from a specified API endpoint and deserializes it into a generic type.
        /// </summary>
        /// <typeparam name="T">Type of data to be deserialized.</typeparam>
        /// <param name="apiUrl">URL of the API endpoint.</param>
        /// <returns>A Task representing the asynchronous operation with a deserialized data.</returns>
        public async Task<T?> GetSingleData<T>(string apiUrl)
        {
            MusiFyApi apiCall = new MusiFyApi(apiUrl);
            string apiResponse = await apiCall.ObtenerDatosAsync();

            T data = JsonSerializer.Deserialize<T>(apiResponse);

            return data;
        }

        /// <summary>
        /// Asynchronously updates data on a specified API endpoint using a PUT request.
        /// </summary>
        /// <param name="apiUrl">URL of the API endpoint.</param>
        /// <param name="data">JSON data to be included in the PUT request.</param>
        /// <returns>A Task representing the asynchronous operation with a boolean indicating the success of the update.</returns>
        public async Task<bool> UpdateData(string apiUrl, string data)
        {
            try
            {
                MusiFyApi apiCall = new MusiFyApi(apiUrl);
                
                bool success = await apiCall.SendPutRequestAsync(data);

                return success;
            }
            catch (Exception ex)
            {
                // Aquí puedes manejar la excepción, por ejemplo, imprimiendo el mensaje de error
                Console.WriteLine($"Ocurrió un error al actualizar los datos: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Asynchronously sends a DELETE request to a specified URL and returns true if the request is successful (HTTP status code 2xx);
        /// otherwise, returns false. Logs errors to the console.
        /// </summary>
        /// <param name="url">URL of the resource to be deleted.</param>
        /// <returns>A Task representing the asynchronous operation with a boolean indicating the success of the delete operation.</returns>
        public async Task<bool> DeleteData(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.DeleteAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception in DeleteData: {ex.Message}");
                    return false;
                }
            }
        }

        /// <summary>
        /// Asynchronously sends a POST request to a specified API endpoint with the provided JSON data.
        /// Returns true if the request is successful (HTTP status code 2xx); otherwise, returns false.
        /// Logs errors to the console.
        /// </summary>
        /// <param name="apiurl">URL of the API endpoint.</param>
        /// <param name="data">JSON data to be included in the POST request.</param>
        /// <returns>A Task representing the asynchronous operation with a boolean indicating the success of the create operation.</returns>
        public async Task<bool>CreateData(string apiurl, string data)
        {
            try
            {
                MusiFyApi apiCall = new MusiFyApi(apiurl);

                bool success = await apiCall.SendPostRequestAsync(data);

                return success;
            }
            catch (Exception ex)
            {
                // Aquí puedes manejar la excepción, por ejemplo, imprimiendo el mensaje de error
                Console.WriteLine($"Ocurrió un error al actualizar los datos: {ex.Message}");
                return false;
            }
        }
    }

    public class Album
    {
        public string? NomAlbum { get; set; }
        public string? data { get; set; }
        public string? UIDSong {  get; set; }
        public string? SongObj { get; set; }
    }

    public class Grups
    {
        public string? NomGrup { get; set; }
        public object? artistes { get; set; }
        public object? participa { get; set; }
    }

    public class Artist
    {
        public string? NomArtista { get; set; }
        public int AnyNaixement { get; set; }
        public object? grups { get; set; }
        public object? participa { get; set; }
    }

    public class Instrument
    {
        public string? Nom { get; set; }
        public string? Model { get; set; }
        public object? participa { get; set; }

    }
  
    public class Songs
    {
        public string UID { get; set; }
        public DateTime? data { get; set; }
        public string? NomSong { get; set; }
        public object? SongObj { get; set; }
        public string? SongOriginal { get; set; }
        public string? Genere { get; set; }
        public object? album { get; set; }
        public object? llista { get; set; }
        public object? participa { get; set; }
        public object? extensio { get; set; }
        public object? songs { get; set; }
    }

    public class Llista
    {
        public string Nom { get; set; }
        public string ID_MAC { get; set; }
        public List<object> songs { get; set; }
    }
    public class Grup
    {
        public string NomGrup { get; set; }

    }
    public class Historial
    {
        public string _ID { get; set; }
        public string mac { get; set; }
        public DateTime data { get; set; }
       
        public string OID { get; set; }
    }

}
