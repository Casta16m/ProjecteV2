using MusiFy_Lib;
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

namespace Musify_Desktop
{
    /// <summary>
    /// Lógica de interacción para AdminSongAlbum.xaml
    /// </summary>
    public partial class AdminSongAlbum : Window
    {
        string BaseUrlSql = ConfigurationManager.AppSettings["BaseUrlSql"];
        public AdminSongAlbum()
        {
            InitializeComponent();

        }
        /// <summary>
        /// Crea Los Song Album
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btCreateSongAlbumClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var album = txtNomAlbum.Text;
                var data = txtdate.Text;
                var song = txtUidSong.Text;
                string url = $"{BaseUrlSql}Album/AfegirSongAlbum/{album}/{data}/{song}";
                Reports rep = new Reports();






                bool success = await rep.CreateDataWitoutJSON(url);
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
