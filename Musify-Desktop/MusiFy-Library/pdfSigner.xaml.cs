using Microsoft.Win32;
using MusiFy_Lib;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MusiFy_Library
{
    /// <summary>
    /// Lógica de interacción para pdfSigner.xaml
    /// </summary>
    public partial class pdfSigner : Window
    {

        CreatePDF crearPDF = new CreatePDF();
        private string pdfFilePath;

        public pdfSigner(string pdfFile)
        {
            InitializeComponent();
            pdfFilePath = pdfFile;
            txtPDFFile.Text = pdfFilePath;
        }

        /// <summary>
        /// Selecct pfx file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSelectPfx_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                txtPfxFile.Text = fileDialog.FileName;
            }
        }

        /// <summary>
        /// Select pdf file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSelectPdf_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                txtPDFFile.Text = saveFileDialog.FileName;
            }
        }

        /// <summary>
        /// Select output pdf file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSelectOutPDF_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                txtPDFFile.Text = saveFileDialog.FileName;
            }
        }

        /// <summary>
        /// Event handler for the "Sign" button click. Signs the selected PDF file using a digital certificate,
        /// creates a signed PDF file, and displays a message indicating the result.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void btSign_Click(object sender, RoutedEventArgs e)
        {
            string filePath = this.txtPDFFile.Text;
            // añade signed al nombre del archivo
            string newFilePath = pdfFilePath.Insert(filePath.Length - 4, "signed");


            if (string.IsNullOrEmpty(this.txtPDFFile.Text))
            {
                MessageBox.Show("Please select a PDF file to sign", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Sign sign;

            try
            {
                sign = new Sign();
                sign.InitCertificate(this.txtPfxFile.Text, this.txtPfxPassword.Password);
                // sign.SignPdf(this.txtPDFFile.Text, $"{outputFilePath}{outputFileName}", this.chkShowSignature.IsChecked == true);
                sign.SignPdf(filePath, newFilePath, this.chkShowSignature.IsChecked == true);
                MessageBox.Show($"Signed pdf was generated and save it in: {newFilePath}"); 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.StackTrace);
                MessageBox.Show("Couldn't sign pdf file");
                MessageBox.Show(ex.ToString());
            }

        }

        /// <summary>
        /// Exit pdf signer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
