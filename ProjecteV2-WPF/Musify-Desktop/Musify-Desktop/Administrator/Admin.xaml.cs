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
            SetButtonImage(participaButton);
            SetButtonImage(artistGroupButton);
            SetButtonImage(songAlbumbutton);

        }
        /// <summary>
        /// Este boto va a la pestaña songs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSongs_Click(object sender, RoutedEventArgs e)
        {
            AdminSongxaml adminSong = new AdminSongxaml();
            adminSong.Show();
        }
        /// <summary>
        /// Este boto va a la pestaña Artists
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnArtists_Click(object sender, RoutedEventArgs e)
        {
            AdminArtists adminArtists = new AdminArtists();
            adminArtists.Show();
        }

        /// <summary>
        /// Este boto va a la pestaña Instruments
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnInstruments_Click(object sender, RoutedEventArgs e)
        {
            AdminInstrument adminInstruments = new AdminInstrument();
            adminInstruments.Show();
        }
        /// <summary>
        /// Este boto va a la pestaña Group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGroups_Click(object sender, RoutedEventArgs e)
        {
            AdminGrup adminGroups = new AdminGrup();
            adminGroups.Show();
        }


        /// <summary>
        /// Este boto va a la pestaña Participa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnParticipa_Click(object sender, RoutedEventArgs e)
        {
            AdminParticipa adminParticipa = new AdminParticipa();
            adminParticipa.Show();
        }
        /// <summary>
        /// Este boto va a la pestaña ArtistGroup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnArtistGroup_Click(object sender, RoutedEventArgs e)
        {
            AdminArtistaGrup adminArtistGroup = new AdminArtistaGrup();
            adminArtistGroup.Show();
        }
        /// <summary>
        /// Este boto va a la pestaña Song Album
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSongAlbum_Click(object sender, RoutedEventArgs e)
        {
            AdminSongAlbum adminSongAlbum = new AdminSongAlbum();
            adminSongAlbum.Show();
        }
        /// <summary>
        /// Este boto va a la pestaña Historial
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMongoHistorial_Click(object sender, RoutedEventArgs e)
        {
            AdminMongoHistorial adminMongoHistorial = new AdminMongoHistorial();
            adminMongoHistorial.Show();
        }
        /// <summary>
        /// Este boto va a la pestaña Mongo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMongoSong_Click(object sender, RoutedEventArgs e)
        {
            AdminMongoSong adminMongoSong = new AdminMongoSong();
            adminMongoSong.Show();
        }

       /// <summary>
       /// Possa la imatge a el boto
       /// </summary>
       /// <param name="button"></param>
        private void SetButtonImage(MusiFy_Library.Button button)
        {
            string imgPath = "pack://application:,,,/MusiFy-Library;component/Images/report.png";
            // button.SourceImageButton = new BitmapImage(new Uri(imgPath, UriKind.Absolute));
        }

    }
}