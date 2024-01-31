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
        /// Obtiene los datos de la API y los deserializa en una lista de objetos
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiUrl"></param>
        /// <returns></returns>
        public async Task<List<T>?> GetData<T>(string apiUrl)
        {

            MusiFyApi apiCall = new MusiFyApi(apiUrl);
            string apiResponse = await apiCall.ObtenerDatosAsync();
            Console.WriteLine(apiResponse);
            List<T> dataList = JsonSerializer.Deserialize<List<T>>(apiResponse);

            return dataList;

        }
        public async Task<T?> GetDataOne<T>(string apiUrl)
        {
            MusiFyApi apiCall = new MusiFyApi(apiUrl);
            string apiResponse = await apiCall.ObtenerDatosAsync();
            Console.WriteLine(apiResponse);
            T data = JsonSerializer.Deserialize<T>(apiResponse);

            return data;
        }


        /// <summary>
        /// Obtiene los datos de la API y los deserializa en un objeto
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiUrl"></param>
        /// <returns></returns>
        public async Task<T?> GetSingleData<T>(string apiUrl)
        {
            MusiFyApi apiCall = new MusiFyApi(apiUrl);
            string apiResponse = await apiCall.ObtenerDatosAsync();

            T data = JsonSerializer.Deserialize<T>(apiResponse);

            return data;
        }

        /// <summary>
        /// Actualiza los datos de la API
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <param name="data"></param>
        /// <returns></returns>
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
        /// Elimina los datos de la API
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
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
        /// Crea los datos de la API
        /// </summary>
        /// <param name="apiurl"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> CreateData(string apiurl, string data)
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
        public async Task<bool> CreateDataWitoutJSON(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PutAsync(url, null);
                return response.IsSuccessStatusCode;
            }
        }
    }

    public class Album
    {
        public string? NomAlbum { get; set; }
        public string? data { get; set; }
        public string? UIDSong { get; set; }
        public string? SongObj { get; set; }
    }


    public class Grups
    {
        public string? NomGrup { get; set; }
        public object? artistes { get; set; }
        public object participa { get; set; }

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
        public string? UID { get; set; }
        public DateTime? data { get; set; }
        public string? nomSong { get; set; }
        public object? songObj { get; set; }
        public string? songOriginal { get; set; }
        public string? genere { get; set; }
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

}
public class Historial
{
    public string _ID { get; set; }
    public string mac { get; set; }
    public DateTime data { get; set; }

    public string uidSong { get; set; }
}
public class Participa
{
    public string UID { get; set; }
    public string NomArtista { get; set; }

    public string NomGrup { get; set; }
    public string NomInstrument { get; set; }

}
