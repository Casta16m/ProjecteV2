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
    public partial class pdfSigner : UserControl
    {

        CreatePDF crearPDF = new CreatePDF();
        public pdfSigner()
        {
            InitializeComponent();
        }

        private void btSelectPfx_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                txtPfxFile.Text = fileDialog.FileName;
            }
        }
        private void btSelectPdf_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                txtPDFFile.Text = saveFileDialog.FileName;
            }
        }

        private void btSelectOutPDF_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                txtPDFFile.Text = saveFileDialog.FileName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSign_Click(object sender, RoutedEventArgs e)
        {

            string outputFilePath = System.IO.Path.Combine(@"..\..\..\..\MusiFy_Lib\signedpdf\Signed");
            string outputFileName = System.IO.Path.GetFileName(txtPDFFile.Text);

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
                sign.SignPdf(this.txtPDFFile.Text, $"{outputFilePath}{outputFileName}", this.chkShowSignature.IsChecked == true);
                MessageBox.Show("Signed pdf was generated");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.StackTrace);
                MessageBox.Show("Couldn't sign pdf file");
                MessageBox.Show(ex.ToString());
            }

        }
    }
}
