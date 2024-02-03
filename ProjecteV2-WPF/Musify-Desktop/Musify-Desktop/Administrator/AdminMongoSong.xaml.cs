using com.sun.org.apache.xerces.@internal.xs;
using javax.swing.plaf;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftWindowsWPF;
using Microsoft.Win32;
using MusiFy_Lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Musify_Desktop
{

    /// <summary>
    /// Lógica de interacción para AdminMongoHistorial.xaml
    /// </summary>
    public partial class AdminMongoSong : Window
    {
        private readonly HttpClient _httpClient;
        string _baseUrl = ConfigurationManager.AppSettings["BaseUrlFixersAPI"];
        static string fileName = "";
        static string filePath = "";
        
        public AdminMongoSong()
        {
            InitializeComponent();
            _httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(30) };
          
        }

    
        /// <summary>
        /// Seleciona un Audio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSelectAudio(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".mp3";
            dlg.Filter = "Audio Files (*.mp3)|*.mp3";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                filePath = dlg.FileName;
                // gets the name of the path 
                string[] pathArray = filePath.Split('\\');
                fileName = pathArray[pathArray.Length - 1];
                audioname.Text = fileName;
            }
        }
        /// <summary>
        /// Actualiza un audio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btUploadAudio(object sender, RoutedEventArgs e)
        {
            if (filePath != "" && txt_uid.Text != "")
            {
                MessageBox.Show("Subiendo canción");
                PostSongAudio(txt_uid.Text, filePath);
                MessageBox.Show("Fuincion ejecutada correctamente");
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ninguna canción");
            
            }
        }
        /// <summary>
        /// Crea un audio
        /// </summary>
        /// <param name="songUid"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async Task<bool> PostSongAudio(string songUid, string filePath)
        {
            try
            {
                using (var formData = new MultipartFormDataContent())
                using (var fileStream = File.OpenRead(filePath))
                {
                    formData.Add(new StringContent(songUid), "Uid");

                    // Agrega el archivo al formulario
                    formData.Add(new StreamContent(fileStream), "Audio", System.IO.Path.GetFileName(filePath));

                    // Realiza la llamada POST
                    var response = await _httpClient.PostAsync($"{_baseUrl}Song", formData);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Respuesta exitosa");
                        MessageBox.Show("Canción subida correctamente");
                        // Puedes hacer algo con la respuesta JSON, si es necesario
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"Respuesta no exitosa: {response.StatusCode}");
                        MessageBox.Show($"Respuesta no exitosa: {response.StatusCode} {response.Content} {response.RequestMessage}");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al realizar la solicitud: {ex.Message}");
                MessageBox.Show($"Error al realizar la solicitud: {ex.Message}");
            }

            return false;
        }

        /// <summary>
        /// Obtener un Audio
        /// </summary>
        /// <param name="songUid"></param>
        /// <returns></returns>
        public async Task<string> GetSongAudio(string songUid)
        {
            try
            {
                // Realiza la llamada GET
                var response = await _httpClient.GetAsync($"{_baseUrl}Song");

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Get songs mongo exitosamente");
                }
                else
                {
                    Console.WriteLine($"Respuesta no exitosa: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al realizar la solicitud: {ex.Message}");
            }

            return "undefined";
        }


    }
}
