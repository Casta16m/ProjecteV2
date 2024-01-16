using System;
using System.Collections.Generic;
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
    }
}

