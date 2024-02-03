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

        /// <summary>
        /// Handles the click event for the "Songs" button. Retrieves a list of songs from a SQL-based API endpoint,
        /// extracts song names, and processes the list of names using the responseProcess function.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnSongs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Songs> songs = await reports.GetData<Songs>(BaseUrlSql + "Song/");

                List<string?> songNames = new List<string>();
                songNames = songs.Select(x => x.nomSong).ToList();

                responseProcess(songNames);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Handles the click event for the "Artists" button. Retrieves a list of artists from a SQL-based API endpoint,
        /// extracts artist names, and processes the list of names using the responseProcess function.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Handles the click event for the "Instruments" button. Retrieves a list of instruments from a SQL-based API endpoint,
        /// extracts instrument names, and processes the list of names using the responseProcess function.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Handles the click event for the "Groups" button. Retrieves a list of groups from a SQL-based API endpoint,
        /// extracts group names, and processes the list of names using the responseProcess function.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Handles the click event for the "Albums" button. Retrieves a list of albums from a SQL-based API endpoint,
        /// extracts album names, and processes the list of names using the responseProcess function.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Processes a list of content, generates a PDF file, and provides options to sign and view the PDF.
        /// </summary>
        /// <param name="content"></param>
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

        /// <summary>
        /// Opens a Save File Dialog to allow the user to choose a location for saving a PDF file. 
        /// If a location is selected, creates a PDF file using the provided content and returns the file path; otherwise, returns null.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
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