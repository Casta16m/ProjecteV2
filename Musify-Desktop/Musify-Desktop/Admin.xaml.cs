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

        private void SetButtonImage(MusiFy_Library.Button button)
        {
            button.SourceImageButton = new BitmapImage(new Uri("pack://application:,,,/MusiFy-Library;component/Images/tendencia.png", UriKind.Absolute));
        }

    }
}
