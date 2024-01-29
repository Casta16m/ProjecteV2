using javax.swing.plaf;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftWindowsWPF;
using MusiFy_Lib;
using Newtonsoft.Json;
using Org.BouncyCastle.Utilities;
using sun.security.action;
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
    /// Lógica de interacción para AdminArtists.xaml
    /// </summary>
    public partial class AdminArtists : Window
    {
        public AdminArtists()
        {
            InitializeComponent();
            GetAllArtists();
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += Timer_Tick;
            timer.Start();

        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            GetAllArtists();
        }
        private async void GetAllArtists()
        {
            string url = "http://172.23.1.231:1443/api/Artista/";

            Reports reports = new Reports();
            List<Artist> artist = await reports.GetData<Artist>(url);

            // Asegúrate de que las canciones no sean null antes de intentar acceder a sus propiedades
            if (artist != null)
            {
                lvArtists.ItemsSource = artist;
            }
            else
            {
                // Maneja el caso en que las canciones sean null (por ejemplo, muestra un mensaje de error)
            }

        }

        private async void btCreateArtistClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var nom = txtNom.Text;
                string url = $"http://172.23.1.231:1443/api/Artista/";
                Reports rep = new Reports();

                Artist artistToUpdate = new Artist();

                artistToUpdate.NomArtista = txtNom.Text;

                int anyNaixament;
                if (int.TryParse(txtAnyNeixament.Text, out anyNaixament))
                {
                    artistToUpdate.AnyNaixement = anyNaixament;
                }
                else
                {
                    MessageBox.Show("El año de nacimiento introducido no es válido. Por favor, inténtalo de nuevo.");
                    return;
                }

                string jsonData = JsonConvert.SerializeObject(artistToUpdate);

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

        private async void btUpdateArtistClick(object sender, RoutedEventArgs e)
        {
            try
            {

                string url = $"http://172.23.1.231:1443/api/Artista/modificarArtista";
                Reports rep = new Reports();

                Artist artistToUpdate = new Artist();

                artistToUpdate.NomArtista = txtNom.Text;
                int anyNaixament;
                if (int.TryParse(txtAnyNeixament.Text, out anyNaixament))
                {
                    artistToUpdate.AnyNaixement = anyNaixament;
                }
                else
                {
                    MessageBox.Show("El año de nacimiento introducido no es válido. Por favor, inténtalo de nuevo.");
                    return;
                }

                Artist artist = new Artist
                {
                    NomArtista = artistToUpdate.NomArtista,
                    AnyNaixement = artistToUpdate.AnyNaixement


                };
                string jsonData = JsonConvert.SerializeObject(artistToUpdate);


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

        private async void btDeleteArtistClick(object sender, RoutedEventArgs e)
        {
            var nom = txtNom.Text;
            string url = $"http://172.23.1.231:1443/api/Artista/{nom}";
            Reports rep = new Reports();
            Artist artistToUpdate = new Artist();

            artistToUpdate.NomArtista = txtNom.Text;

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

        private void lvArtists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
