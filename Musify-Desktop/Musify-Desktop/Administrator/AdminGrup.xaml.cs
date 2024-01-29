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
    /// Lógica de interacción para AdminGrup.xaml
    /// </summary>
    public partial class AdminGrup : Window
    {
        public AdminGrup()
        {
            InitializeComponent();
            GetAllGrup();
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            GetAllGrup();
        }
        private async void GetAllGrup()
        {
            string url = "http://192.168.1.41:1443/api/Grup/";

            Reports reports = new Reports();
            List<Grup> grup = await reports.GetData<Grup>(url);

            // Asegúrate de que las canciones no sean null antes de intentar acceder a sus propiedades
            if (grup != null)
            {
                lvArtists.ItemsSource = grup;
            }
            else
            {
                // Maneja el caso en que las canciones sean null (por ejemplo, muestra un mensaje de error)
            }

        }
        private async void btCreateGrupClick(object sender, RoutedEventArgs e)
        {
            try
            {

                string url = $"http://192.168.1.41:1443/api/Grup/";
                Reports rep = new Reports();

                Grup grupToUpdate = new Grup();

                grupToUpdate.NomGrup = txtNom.Text;

                string jsonData = JsonConvert.SerializeObject(grupToUpdate);

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


        private void lvArtists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
