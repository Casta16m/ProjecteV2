﻿using com.sun.corba.se.impl.protocol.giopmsgheaders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
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



            return dataList ;
          

        }



      
        




      





    }

   public class Album
    {
        public string? NomAlbum{ get; set; }
        public string? ArtistaNom { get; set; }
    }



    public class Artist
    {
        public string? NomArtista { get; set; }
        public int AnyNaixement { get; set; }
        
    
    }

}