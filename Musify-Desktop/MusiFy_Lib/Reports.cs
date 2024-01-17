using com.sun.corba.se.impl.protocol.giopmsgheaders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MusiFy_Lib
{
    public class Reports
    {





        public async Task<Artist> GetArtists()
        {

            MusiFyApi apiCall = new MusiFyApi("http://172.23.1.231:1443/api/Artista");
            string apiResponse = await apiCall.ObtenerDatosAsync();

            Artist  artist = JsonSerializer.Deserialize<Artist>(apiResponse);

            return artist;

        }





        public void GetAlbums()
        {


        }





    }


    public class Artist
    {
        public string NomArtista { get; set; }
        public int AnyNaixement { get; set; }
        
    


    }

}
