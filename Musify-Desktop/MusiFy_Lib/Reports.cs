using com.sun.corba.se.impl.protocol.giopmsgheaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusiFy_Lib
{
     public class Reports
    {





        public async Task<string> GetArtists()
        {  

            MusiFyApi apiCall = new MusiFyApi("http://172.23.1.231:1443/api/Artista");
            string apiResponse = await apiCall.ObtenerDatosAsync();

            return apiResponse;

        }

        public void GetAlbums()
        {


        }





    }

    



}
