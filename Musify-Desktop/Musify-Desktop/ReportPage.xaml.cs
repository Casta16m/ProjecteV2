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
using iText.IO.Font;


namespace Musify_Desktop
{
    /// <summary>
    /// Lógica de interacción para ReportWindow.xaml
    /// </summary>
    public partial class ReportPage : Window
    {

        MusiFy_Lib.Reports reports = new MusiFy_Lib.Reports();
        private CreatePDF crearPDF = new CreatePDF();
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

                responseProcess(songNames);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void BtnArtists_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                List<Artist> artists = await reports.GetData<Artist>(BaseUrlSql + "Artista/");

                List<string?> artisNames = new List<string>();
                artisNames = artists.Select(x => x.NomArtista).ToList();

                responseProcess(artisNames);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void BtnInstruments_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Instrument> instruments = await reports.GetData<Instrument>(BaseUrlSql + "Instrument/");

                List<string?> instrumentNames = new List<string>();
                instrumentNames = instruments.Select(x => x.Nom).ToList();
                responseProcess(instrumentNames);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void BtnGrups_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Grups> grups = await reports.GetData<Grups>(BaseUrlSql + "Grup/");

                List<string?> grupNames = new List<string>();
                grupNames = grups.Select(x => x.NomGrup).ToList();
                
                responseProcess(grupNames);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void BtnAlbums_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Album> albums = await reports.GetData<Album>(BaseUrlSql + "Album/");

                List<string?> albumNames = new List<string>();
                albumNames = albums.Select(x => x.NomAlbum).ToList();
                
                responseProcess(albumNames);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void responseProcess(List<string?> content)
        {
            MessageBox.Show("Select the place where you want to save the pdf \n Remind to put .pdf in the namefile");
            string pdfPath = CreatePDFAndOpenFileDialog(content);

            // ask if they want to sign the pdf
            MessageBoxResult result = MessageBox.Show("Do you want to sign the pdf?", "Sign PDF", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    pdfSigner ventanaPdfSigner = new pdfSigner(pdfPath);
                    ventanaPdfSigner.ShowDialog();
                    break;
                case MessageBoxResult.No:
                    MessageBox.Show("All fine");
                    break;
            }

            MessageBoxResult resultOpen = MessageBox.Show("Do you want to see the pdf?", "See PDF", MessageBoxButton.YesNo);
            switch (resultOpen)
            {
                case MessageBoxResult.Yes:
                    // show the pdf
                    Paginacio ventanaPaginacio = new Paginacio(pdfPath);
                    ventanaPaginacio.ShowDialog();

                    break;
                case MessageBoxResult.No:
                    MessageBox.Show("All fine");
                    break;
            }

        }



        private string? CreatePDFAndOpenFileDialog(List<string?> content)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                MessageBox.Show("PDF created" + filePath);
                crearPDF.createPDF(content, filePath);
                return filePath;
            }
            return null;
        }

        private void pdfSigner_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}