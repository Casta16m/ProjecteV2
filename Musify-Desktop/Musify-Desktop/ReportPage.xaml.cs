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

namespace Musify_Desktop
{
    /// <summary>
    /// Lógica de interacción para ReportWindow.xaml
    /// </summary>
    public partial class ReportPage : Page
    {


        static MusiFy_Lib.CreatePDF? crearPDF = new MusiFy_Lib.CreatePDF();
        public ReportPage()
        {
            InitializeComponent();

        }



        public void ReportButton_Click(object sender, RoutedEventArgs e)
        {
            crearPDF.createPDF();
        }


        

       
    }
}
