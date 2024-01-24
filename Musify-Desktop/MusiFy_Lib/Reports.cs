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
namespace MusiFy_Lib
{
    public class Reports
    {

        /// <summary>
        /// Method to get the artists from the API
        /// </summary>
        /// <returns></returns>
        /// 




        public async Task<List<T>?> GetData<T>(string apiUrl)
        {


            MusiFyApi apiCall = new MusiFyApi(apiUrl);
            string apiResponse = await apiCall.ObtenerDatosAsync();

            List<T> dataList = JsonSerializer.Deserialize<List<T>>(apiResponse);




            return dataList;


        }
        public async Task<T?> GetSingleData<T>(string apiUrl)
        {
            MusiFyApi apiCall = new MusiFyApi(apiUrl);
            string apiResponse = await apiCall.ObtenerDatosAsync();

            T data = JsonSerializer.Deserialize<T>(apiResponse);

            return data;
        }
        public async Task<bool> UpdateData<T>(string apiUrl, T data)
        {
            MusiFyApi apiCall = new MusiFyApi(apiUrl);
            string dataJson = JsonSerializer.Serialize(data);
            bool success = await apiCall.SendPutRequestAsync(dataJson);

            return success;
        }















    }

    public class Album
    {
        public string? NomAlbum { get; set; }
        public string? data { get; set; }
        public string? UIDSong {  get; set; }
        public string? SongObj { get; set; }
        
       
    }



    public class Artist
    {
        public string? NomArtista { get; set; }
        public int AnyNaixement { get; set; }


    }
  
    public class Songs
    {
        public string UID { get; set; }
        public string? data { get; set; }
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
}
