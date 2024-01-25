using MusiFy_Lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Musify_Desktop
{
    /// <summary>
    /// Lógica de interacción para AdminSongxaml.xaml
    /// </summary>
    public partial class AdminSongxaml : Window
    {
        public AdminSongxaml()
        {
            InitializeComponent();
            btGetAllSongs();
        }

        private async void btGetSongClick(object sender, RoutedEventArgs e)
        {
            var uid = txtUID.Text;

            string url = $"http://172.23.1.231:1443/api/Song/BuscarUID/{uid}";


            Reports reports = new Reports();
            Songs song = await reports.GetSingleData<Songs>(url);
            if (song != null)
            {
                txtNomcanço.Text = song.NomSong;
               // txtCançoOriginal.Text = song.SongOriginal;
                txtGenere.Text = song.Genere;
            }

        }
        private async void btDeleteSongClick(object sender, RoutedEventArgs e)
        {
            var uid = txtUID.Text;
            string url = $"http://172.23.1.231:1443/api/Song/{uid}";
            Reports rep = new Reports();
            Songs songToUpdate = new Songs();

            songToUpdate.UID = txtUID.Text;

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
        private async void btUpdateSongClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var uid = txtUID.Text;
                string url = $"http://172.23.1.231:1443/api/Song/modificarSong/";
                Reports rep = new Reports();

                Songs songToUpdate = new Songs();

                songToUpdate.UID = txtUID.Text;
                songToUpdate.NomSong = txtNomcanço.Text;
               // songToUpdate.SongOriginal = txtCançoOriginal.Text;
                songToUpdate.Genere = txtGenere.Text;

                songToUpdate.data = DateTime.Now;
                Songs song = new Songs
                {
                    NomSong = songToUpdate.NomSong,
                    SongOriginal = songToUpdate.SongOriginal,
                    Genere = songToUpdate.Genere,

                };
                string jsonData = JsonConvert.SerializeObject(songToUpdate);
               

                bool success = await rep.UpdateData(url, jsonData);
                if (success == true)
                {
                    // El objeto se actualizó correctamente
                }
                else
                {
                    // Hubo un error al actualizar el objeto
                    MessageBox.Show("Hubo un error al actualizar la canción. Por favor, inténtalo de nuevo." );

                }
            }
            catch (Exception ex)
            {
                // Aquí puedes manejar la excepción y mostrar un mensaje al usuario
                MessageBox.Show($"Se produjo un error: {ex.Message}");
            }
        }
        private async void btGetAllSongs()
        {
            string url = "http://172.23.1.231:1443/api/Song/";

            Reports reports = new Reports();
            List<Songs> songs = await reports.GetData<Songs>(url);

            // Asegúrate de que las canciones no sean null antes de intentar acceder a sus propiedades
            if (songs != null)
            {
                lvSongs.ItemsSource = songs;
            }
            else
            {
                // Maneja el caso en que las canciones sean null (por ejemplo, muestra un mensaje de error)
            }
        }

        private void lvSongs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

