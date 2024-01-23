using java.lang;
using Microsoft.Win32;
using MusiFy_Lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MusiFy_Library
{
    /// <summary>
    /// Lógica de interacción para UpdateSongs.xaml
    /// </summary>
    public partial class UpdateSongs : UserControl
    {
        public UpdateSongs()
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
                txtCançoOriginal.Text = song.Genere;
                txtGenere.Text = song.Genere;
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

    }

}


