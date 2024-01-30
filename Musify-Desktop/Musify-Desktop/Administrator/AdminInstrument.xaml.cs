using javax.sound.midi;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftWindowsWPF;
using MusiFy_Lib;
using Newtonsoft.Json;
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
using Instrument = MusiFy_Lib.Instrument;

namespace Musify_Desktop
{
    /// <summary>
    /// Lógica de interacción para AdminInstrument.xaml
    /// </summary>
    public partial class AdminInstrument : Window
    {
        string BaseUrlSql = ConfigurationManager.AppSettings["BaseUrlSql"];
        public AdminInstrument()
        {
            InitializeComponent();
            GetAllInstrument();
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            GetAllInstrument();
        }
        private async void GetAllInstrument()
        {
            string url = $"{BaseUrlSql}Instrument/";

            Reports reports = new Reports();
            List<Instrument> Instrument = await reports.GetData<Instrument>(url);

            // Asegúrate de que las canciones no sean null antes de intentar acceder a sus propiedades
            if (Instrument != null)
            {
                lvArtists.ItemsSource = Instrument;
            }
            else
            {
                // Maneja el caso en que las canciones sean null (por ejemplo, muestra un mensaje de error)
            }

        }


        private async void btCreateInstrumentClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var nom = txtNom.Text;
                string url = $"{BaseUrlSql}Instrument/";
                Reports rep = new Reports();

                Instrument instrumentToUpdate = new Instrument();

                instrumentToUpdate.Nom = txtNom.Text;
                instrumentToUpdate.Model = txtModel.Text;


                string jsonData = JsonConvert.SerializeObject(instrumentToUpdate);

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
        private async void btUpdateInstrumentClick(object sender, RoutedEventArgs e)
        {
            try
            {

                string url = $"{BaseUrlSql}Instrument/modificarInstrument";
                Reports rep = new Reports();

                Instrument instrumentToUpdate = new Instrument();

                instrumentToUpdate.Nom = txtNom.Text;
                instrumentToUpdate.Model = txtModel.Text;

                Instrument artist = new Instrument
                {
                    Nom = instrumentToUpdate.Nom,
                    Model = instrumentToUpdate.Model


                };
                string jsonData = JsonConvert.SerializeObject(instrumentToUpdate);


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
        private async void btDeleteInstrumentClick(object sender, RoutedEventArgs e)
        {
            var nom = txtNom.Text;
            string url = $"http://192.168.1.41/api/Instrument/{nom}";
            Reports rep = new Reports();
            Instrument instrumentToUpdate = new Instrument();

            instrumentToUpdate.Nom = txtNom.Text;

            try
            {
                bool success = await rep.DeleteData(url);
                if (success)
                {
                    MessageBox.Show("Canción borrada con éxito.");
                }
                else
                {
                    MessageBox.Show("Hubo un error al borrar la canción.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Se produjo un error al borrar la canción: {ex.Message}");
            }
        }
    }
}
