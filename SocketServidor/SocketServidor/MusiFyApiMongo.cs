using MusiFy_Lib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SocketServidor
{
    public class MusiFyApiMongo
    {
        string apiUrl = ConfigurationManager.AppSettings["ApiUrl"];
        HttpClient client = new HttpClient();

        public async Task<JObject> RealitzarPeticioAPI(string apiUrl, string _url)
        {
            var URL = apiUrl + _url;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(URL);
                    response.EnsureSuccessStatusCode();
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    return JObject.Parse(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la solicitud a la API ({URL}): {ex.Message}");
                return null;
            }
        }

    }
}
