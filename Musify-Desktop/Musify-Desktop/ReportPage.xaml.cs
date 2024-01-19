using MusiFy_Lib;
using MusiFy_Library;
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
using System.Windows.Shapes;
using System.Collections.Generic;
using Microsoft.Win32;
using MusiFy_Lib;

namespace Musify_Desktop
{
    /// <summary>
    /// Lógica de interacción para ReportWindow.xaml
    /// </summary>
    public partial class ReportPage : Window{
        
      public ReportPage()
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

        private void btSelectPDF_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                txtPDFFile.Text = saveFileDialog.FileName;
            }
        }
        private void btSign_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtPDFFile.Text))
            {
                MessageBox.Show("Please select a PDF file to sign", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            } try
            {
                var create = new MusiFyApi("http://172.23.1.231:1443/api/Artista/BuscarNom/");
                MessageBox.Show(create.ObtenerDatosAsync().Result);
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
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
