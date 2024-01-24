using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusiFy_Lib
{
    public class MusiFyApi
    {
        private readonly HttpClient client;
        private string apiGetURL;


        public MusiFyApi(string apiGetURL)
        {

            this.apiGetURL = apiGetURL ?? throw new ArgumentNullException(nameof(apiGetURL));
            this.client = new HttpClient();

        }

        public async Task<string> ObtenerDatosAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(apiGetURL);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {

                    return $"Error: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {

                return $"Error: {ex.Message}";
            }
           
        }
        public async Task<bool> SendPutRequestAsync(string dataJson)
        {
            try
            {
                var content = new StringContent(dataJson, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(apiGetURL, content);
                

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Aquí puedes manejar la excepción y, por ejemplo, registrarla en un archivo de log
                Debug.WriteLine($"Error en SendPutRequestAsync: {ex.Message}");
                return false;
            }
        }
    }
}

