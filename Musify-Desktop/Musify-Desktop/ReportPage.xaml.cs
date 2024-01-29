using MusiFy_Lib;
using MusiFy_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using System.Configuration;


namespace Musify_Desktop
{
    /// <summary>
    /// Lógica de interacción para ReportWindow.xaml
    /// </summary>
    public partial class ReportPage : Window
    {

        MusiFy_Lib.Reports reports = new MusiFy_Lib.Reports();
        private CreatePDF crearPDF = new CreatePDF();
        pdfSigner PDFSigner = new pdfSigner();
        string BaseUrlSql = ConfigurationManager.AppSettings["BaseUrlSql"];

        public ReportPage()
        {
            InitializeComponent();

        }

        private async void BtnSongs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Songs> songs = await reports.GetData<Songs>(BaseUrlSql + "Song/");

                List<string?> songNames = new List<string>();
                songNames = songs.Select(x => x.NomSong).ToList();
                CreatePDFAndOpenFileDialog(songNames);

                signAPDF.Visibility = Visibility.Visible;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                signAPDF.Visibility = Visibility.Hidden;
            }
        }

        private async void BtnArtists_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                List<Artist> artists = await reports.GetData<Artist>(BaseUrlSql + "Artista/");

                List<string?> artisNames = new List<string>();
                artisNames = artists.Select(x => x.NomArtista).ToList();
                CreatePDFAndOpenFileDialog(artisNames);

                signAPDF.Visibility = Visibility.Visible;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                signAPDF.Visibility = Visibility.Hidden;
            }
        }

        private async void BtnInstruments_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Instrument> instruments = await reports.GetData<Instrument>(BaseUrlSql + "Instrument/");

                List<string?> instrumentNames = new List<string>();
                instrumentNames = instruments.Select(x => x.Nom).ToList();
                CreatePDFAndOpenFileDialog(instrumentNames);

                signAPDF.Visibility = Visibility.Visible;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                signAPDF.Visibility = Visibility.Hidden;
            }
        }

        private async void BtnGrups_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Grups> grups = await reports.GetData<Grups>(BaseUrlSql + "Grup/");

                List<string?> grupNames = new List<string>();
                grupNames = grups.Select(x => x.NomGrup).ToList();
                CreatePDFAndOpenFileDialog(grupNames);

                signAPDF.Visibility = Visibility.Visible;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                signAPDF.Visibility = Visibility.Hidden;
            }
        }

        private async void BtnAlbums_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Album> albums = await reports.GetData<Album>(BaseUrlSql + "Album/");

                List<string?> albumNames = new List<string>();
                albumNames = albums.Select(x => x.NomAlbum).ToList();
                CreatePDFAndOpenFileDialog(albumNames);

                signAPDF.Visibility = Visibility.Visible;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                signAPDF.Visibility = Visibility.Hidden;
            }
        }

        private async void CreatePDFAndOpenFileDialog
            (List<string?> content)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                crearPDF.createPDF(content, saveFileDialog.FileName);
            }
        }

        private void pdfSigner_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}