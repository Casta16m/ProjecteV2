using com.sun.security.ntlm;
using java.lang;

using MusiFy_Library;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Musify_Desktop
{
    /// <summary>
    /// Lógica de interacción para Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();

            
            SetButtonImage(songButton);
            SetButtonImage(artistButton);
            SetButtonImage(instrumentButton);
            SetButtonImage(groupButton);
            SetButtonImage(extensionButton);
            SetButtonImage(participaButton);
            SetButtonImage(artistGroupButton);
            SetButtonImage(songAlbumbutton);
            
        }

       private void BtnSongs_Click(object sender, RoutedEventArgs e)
        {
            AdminSongxaml adminSong = new AdminSongxaml();
            adminSong.Show();
        }


        private void BtnArtists_Click(object sender, RoutedEventArgs e)
        {
            AdminArtists adminArtists = new AdminArtists();
            adminArtists.Show();
        }


        private void BtnInstruments_Click(object sender, RoutedEventArgs e)
        {
            AdminInstrument adminInstruments = new AdminInstrument();
            adminInstruments.Show();
        }

        private void BtnGroups_Click(object sender, RoutedEventArgs e)
        {
            AdminGrup adminGroups = new AdminGrup();
            adminGroups.Show();
        }

        

        private void BtnParticipa_Click(object sender, RoutedEventArgs e)
        {
            AdminParticipa adminParticipa = new AdminParticipa();
            adminParticipa.Show();
        }

        private void BtnArtistGroup_Click(object sender, RoutedEventArgs e)
        {
            AdminArtistaGrup adminArtistGroup = new AdminArtistaGrup();
            adminArtistGroup.Show();
        }

        private void BtnSongAlbum_Click(object sender, RoutedEventArgs e)
        {
            AdminSongAlbum adminSongAlbum = new AdminSongAlbum();
            adminSongAlbum.Show();
        }
        private void BtnMongoHistorial_Click(object sender, RoutedEventArgs e)
        {
            AdminMongoHistorial adminMongoHistorial = new AdminMongoHistorial();
            adminMongoHistorial.Show();
        }

        
        private void SetButtonImage(MusiFy_Library.Button button)
        {
            button.SourceImageButton = new BitmapImage(new Uri("pack://application:,,,/MusiFy-Library;component/Images/tendencia.png", UriKind.Absolute));
        }

    }
}
