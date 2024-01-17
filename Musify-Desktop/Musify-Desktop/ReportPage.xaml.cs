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
        public async void CreateReportButtons()
        {
                    

                

            for (int i = 0; i < 4; i++)
            {
                int index = i;
                MusiFy_Lib.Reports report = new MusiFy_Lib.Reports();
                List <string> artistNames = new List<string>();
                List<Artist> artists = await report.GetData<Artist>("http://localhost:1443/api/Artista");
                artistNames.Add(artists[index].NomArtista);
                //Asignar estilos a los botones creados
                MusiFy_Library.Button button = new MusiFy_Library.Button();
                button.Text = "Reporte " + i;
                button.WidthButton = 200;
                button.HeightButton = 50;
                button.TextSize = 20;





                switch (index)
                {
                    case 0:
                        button.Click += (sender, e) => crearPDF.createPDF(artistNames, "Artistas");
                        break;
                    case 1:
                        
                        break;
                    case 2:
                        
                        break;
                    case 3:
                       
                        break;
                    default:
                        break;
                }
                          




                button.WidthGrid = new GridLength(110, GridUnitType.Pixel);




                if(i <= 2)
                {
                    reportStack.Children.Add(button);
                }
                else
                {
                    reportStackTwo.Children.Add(button);
                }
               
            }
        }


       


       



    }
}
