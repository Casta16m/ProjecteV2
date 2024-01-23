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
        private void btSign_Click(object sender, RoutedEventArgs e)
        {

            string outputFilePath = System.IO.Path.GetFullPath(@"..\..\..\..\..\MusiFy-Desktop\MusiFy_Lib\SignedPDF\Signed");
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
                sign.InitCertificate(this.txtPfxFile.Text, this.txtPfxPassword.Text);
                sign.SignPdf(this.txtPDFFile.Text, $"{outputFilePath}{outputFileName}", this.chkShowSignature.IsChecked == true);
                MessageBox.Show("Signed pdf was generated");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Couldn't sign pdf file");
                MessageBox.Show(ex.ToString());
            }

        }

        private void txtPfxFile_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtPfxPassword_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtOutFile_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

       


    }
}
