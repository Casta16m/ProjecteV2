using com.sun.org.apache.xerces.@internal.xs;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftWindowsWPF;
using MusiFy_Lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        public AdminMongoHistorial()
        {
            InitializeComponent();
            GetAllHistorial();
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += Timer_Tick;
            timer.Start();
           
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            GetAllHistorial();
        }
        private async void GetAllHistorial()
        {
            string url = "http://172.17.160.1:5001/MongoApi/v1/Historial";

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
        private async void btCreateHistorialClick(object sender, RoutedEventArgs e)
        {
            try
            {
               
                string url = $"http://192.168.1.41:1443/MongoApi/v1/Historial";
                Reports rep = new Reports();

                Historial historialToUpdate = new Historial();

                
                historialToUpdate.OID= txtuidSong.Text;
                historialToUpdate.mac = txtmac.Text;
                historialToUpdate._ID = txt_ID.Text;


                string jsonData = JsonConvert.SerializeObject(historialToUpdate);

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
        public async  void btUpdateHistorialClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var _ID = txt_ID.Text;

                string url = $"http://192.168.1.41:1443/MongoAPI/v1/Historial{_ID}";
                Reports rep = new Reports();

                Historial historialToUpdate = new Historial();

                historialToUpdate.OID = txtuidSong.Text;
                historialToUpdate.mac = txtmac.Text;
                historialToUpdate._ID = txt_ID.Text;

                Historial historial = new Historial
                {
                   OID = historialToUpdate.OID,
               mac = historialToUpdate.mac,
              _ID =  historialToUpdate._ID 



            };
                string jsonData = JsonConvert.SerializeObject(historialToUpdate);


                bool success = await rep.UpdateData(url, jsonData);
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
        
    }
}
