using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftWindowsWPF;
using MusiFy_Lib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using org.w3c.dom.css;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Threading;

namespace Musify_Desktop
{
    /// <summary>
    /// Lógica de interacción para AdminSongxaml.xaml
    /// </summary>
    public partial class AdminSongxaml : Window
    {
        string BaseUrlSql = ConfigurationManager.AppSettings["BaseUrlSql"];
        public AdminSongxaml()
        {
            InitializeComponent();
            btGetAllSongs();
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            btGetAllSongs();
        }

        private async void btGetSongClick(object sender, RoutedEventArgs e)
        {
            var uid = txtUID.Text;

            string url = $"{BaseUrlSql}BuscarUID/{uid}";


            Reports reports = new Reports();
            Songs song = await reports.GetSingleData<Songs>(url);
            if (song != null)
            {
                txtNomcanço.Text = song.nomSong;
                // txtCançoOriginal.Text = song.SongOriginal;
                txtGenere.Text = song.genere;
            }

        }
        private async void btCreateSongClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var extension = txtExtensio.Text;
                string url = $"{BaseUrlSql}Song/crearSong/{extension}";
                Reports rep = new Reports();

                Songs songToUpdate = new Songs();

                songToUpdate.nomSong = txtNomcanço.Text;

                songToUpdate.songOriginal = txtCançoOriginal.Text;
                if (songToUpdate.songOriginal == "")
                {
                    songToUpdate.songOriginal = null;
                }
                songToUpdate.genere = txtGenere.Text;


                Songs song = new Songs
                {
                    nomSong = songToUpdate.nomSong,
                    songOriginal = songToUpdate.songOriginal,
                    genere = songToUpdate.genere


                };
                string jsonData = JsonConvert.SerializeObject(songToUpdate);
                // Parse the JSON into a JObject
                JObject jObject = JObject.Parse(jsonData);



                jObject.Property("UID").Remove();
                jObject.Property("data").Remove();
                jObject.Property("album").Remove();
                jObject.Property("extensio").Remove();
                jObject.Property("participa").Remove();
                jObject.Property("llista").Remove();
                jObject.Property("songs").Remove();




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
                songToUpdate.nomSong = txtNomcanço.Text;
                // songToUpdate.SongOriginal = txtCançoOriginal.Text;
                songToUpdate.genere = txtGenere.Text;

                songToUpdate.data = DateTime.Now;
                Songs song = new Songs
                {
                    nomSong = songToUpdate.nomSong,
                    songOriginal = songToUpdate.songOriginal,
                    genere = songToUpdate.genere,

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
                    MessageBox.Show("Hubo un error al actualizar la canción. Por favor, inténtalo de nuevo.");

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
            string url = $"{BaseUrlSql}Song";

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

    }
}

