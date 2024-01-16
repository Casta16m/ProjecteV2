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
            CreateReportButtons();

        }


        /// <summary>
        /// Method to create the report buttons, add style and add them to the stack panel
        /// </summary>
        public void CreateReportButtons()
        {


            for (int i = 1; i <= 8; i++)
            {
                MusiFy_Library.Button button = new MusiFy_Library.Button();
                button.Text = "Reporte " + i;
                button.WidthButton = 200;
                button.HeightButton = 50;
                button.TextSize = 20;
                button.Click += ReportButton_ClickAsync;
                button.WidthGrid = new GridLength(110, GridUnitType.Pixel);


                if(i <= 4)
                {
                    reportStack.Children.Add(button);
                }
                else
                {
                    reportStackTwo.Children.Add(button);
                }
               
            }
        }


      
        public async void ReportButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            MusiFy_Lib.Reports report = new MusiFy_Lib.Reports();

            MessageBox.Show(await report.GetArtists());



            //await Task.Run(() => CrearPDF());
        }


      


        public async void CrearPDF()

        {
          
           
            
        }



    }
}
