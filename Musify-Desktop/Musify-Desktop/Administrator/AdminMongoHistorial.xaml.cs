using com.sun.org.apache.xerces.@internal.xs;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftWindowsWPF;
using MusiFy_Lib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
    public partial class AdminMongoHistorial : Window

    {
        string baseUrlMongoApi = ConfigurationManager.AppSettings["BaseUrlMongoAPI"];
        public AdminMongoHistorial()
        {
            InitializeComponent();
            GetAllHistorial();
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += Timer_Tick;
            timer.Start();

        }
        /// <summary>
        /// Temporizador de 5 segons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            GetAllHistorial();
        }

        /// <summary>
        /// Busca todo el Historial
        /// </summary>
        private async void GetAllHistorial()
        {
            string url = $"{baseUrlMongoApi}Historial";

            Reports reports = new Reports();
            List<Historial> historial = await reports.GetData<Historial>(url);

            // Asegúrate de que las canciones no sean null antes de intentar acceder a sus propiedades
            if (historial != null)
            {
                lvHistorial.ItemsSource = historial;
            }
            else
            {
                // Maneja el caso en que las canciones sean null (por ejemplo, muestra un mensaje de error)
            }

        }
        /// <summary>
        /// Crea un apartado de el Historial
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btCreateHistorialClick(object sender, RoutedEventArgs e)
        {
            try
            {

                string url = $"{baseUrlMongoApi}Historial";
                Reports rep = new Reports();

                Historial historialToUpdate = new Historial();

                historialToUpdate.data = DateTime.Now;

                historialToUpdate.mac = txtmac.Text;
                historialToUpdate.uidSong = txtuidSong.Text;



                string jsonData = JsonConvert.SerializeObject(historialToUpdate);



                // Parse the JSON into a JObject
                JObject jObject = JObject.Parse(jsonData);

                // Remove the _ID property
                jObject.Property("_ID").Remove();

                // Convert the JObject back into a JSON string
                jsonData = jObject.ToString();


                bool success = await rep.CreateData(url, jsonData);
                if (success == true)
                {
                    // El objeto se actualizó correctamente
                }
                else
                {
                    // Hubo un error al actualizar el objeto
                    MessageBox.Show("Hubo un error al actualizar la canción. Por favor, inténtalo de nuevo.");
                }

            }
            catch (Exception ex)
            {
                // Aquí puedes manejar la excepción y mostrar un mensaje al usuario
                MessageBox.Show($"Se produjo un error: {ex.Message}");
            }
        }
        /// <summary>
        /// Edita un historial
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btUpdateHistorialClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var _ID = txt_ID.Text;

                string url = $"{baseUrlMongoApi}Historial/{_ID}";
                Reports rep = new Reports();

                Historial historialToUpdate = new Historial();
                historialToUpdate._ID = txt_ID.Text;
                historialToUpdate.mac = txtmac.Text;
                historialToUpdate.uidSong = txtuidSong.Text;
                historialToUpdate.data = DateTime.Now;



                Historial historial = new Historial
                {
                    _ID = historialToUpdate._ID,
                    mac = historialToUpdate.mac,
                    uidSong = historialToUpdate.uidSong,
                    data = historialToUpdate.data





                };
                string jsonData = JsonConvert.SerializeObject(historialToUpdate);


                bool success = await rep.UpdateData(url, jsonData);
                if (success == true)
                {
                    // El objeto se actualizó correctamente
                    MessageBox.Show("S'ha actualizat l'historial");
                }
                else
                {
                    // Hubo un error al actualizar el objeto
                    MessageBox.Show("Hubo un error al actualizar la canción. Por favor, inténtalo de nuevo.");

                }
            }
            catch (Exception ex)
            {
                // Aquí puedes manejar la excepción y mostrar un mensaje al usuario
                MessageBox.Show($"Se produjo un error: {ex.Message}");
            }
        }

    }
}
