using com.sun.org.apache.xerces.@internal.xs;
using java.rmi.server;
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
    /// Lógica de interacción para AdminParticipa.xaml
    /// </summary>
    public partial class AdminParticipa : Window
    {
        string BaseUrlSql = ConfigurationManager.AppSettings["BaseUrlSql"];
        public AdminParticipa()
        {
            InitializeComponent();
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += Timer_Tick;
            timer.Start();
            GetAllParticipa();

        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            GetAllParticipa();
        }
        private async void GetAllParticipa()
        {
            string url = $"{BaseUrlSql}Participa";

            Reports reports = new Reports();
            List<Participa> participa = await reports.GetData<Participa>(url);

            // Asegúrate de que las canciones no sean null antes de intentar acceder a sus propiedades
            if (participa != null)
            {
                lvParticipa.ItemsSource = participa;
            }
            else
            {
                // Maneja el caso en que las canciones sean null (por ejemplo, muestra un mensaje de error)
            }
        }
        public async void btCreateParticipaClick(object sender, RoutedEventArgs e)
        {
            try
            {

                string url = $"{BaseUrlSql}Participa";
                Reports rep = new Reports();

                Participa participaToUpdate = new Participa();
                participaToUpdate.UID = txtUid.Text;
                participaToUpdate.NomArtista = txtNomArtista.Text;
                participaToUpdate.NomGrup = txtNomGrup.Text;
                participaToUpdate.NomInstrument = txtNomInstrument.Text;



                string jsonData = JsonConvert.SerializeObject(participaToUpdate);
                JObject jObject = JObject.Parse(jsonData);



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


    }
}
