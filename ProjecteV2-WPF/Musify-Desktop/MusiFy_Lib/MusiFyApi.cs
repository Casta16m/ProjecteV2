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

        /// <summary>
        /// Asynchronously retrieves data from a specified API endpoint using an HTTP GET request.
        /// Returns the content as a string if the request is successful, or an error message if there's an issue.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Asynchronously sends a PUT request to a specified API endpoint with provided JSON data.
        /// Returns true if the request is successful; otherwise, returns false.
        /// </summary>
        /// <param name="dataJson">JSON data to be included in the PUT request.</param>
        /// <returns>A Task representing the asynchronous operation with a boolean indicating the success of the request.</returns>
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

        /// <summary>
        /// Asynchronously sends a POST request to a specified API endpoint with provided JSON data.
        /// Returns true if the request is successful; otherwise, returns false.
        /// </summary>
        /// <param name="dataJson">JSON data to be included in the POST request.</param>
        /// <returns>A Task representing the asynchronous operation with a boolean indicating the success of the request.</returns>
        public async Task<bool> SendPostRequestAsync(string dataJson)
        {
            try
            {
                var content = new StringContent(dataJson, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiGetURL, content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Aquí puedes manejar la excepción y, por ejemplo, registrarla en un archivo de log
                Debug.WriteLine($"Error en SendPostRequestAsync: {ex.Message}");
                return false;
            }
        }
    }
}

