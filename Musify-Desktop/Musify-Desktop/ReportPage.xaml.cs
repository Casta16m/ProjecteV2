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

        public async void CreateReportButtons()
        {
            await Task.Run(() => CreateAndConfigureReportButtons());
        }
        /// <summary>
        /// Method to create the report buttons, add style and add them to the stack panel
        /// </summary>
        public async void CreateAndConfigureReportButtons<T>(string apiUrl, string reportName)
        {

            MusiFy_Lib.Reports report = new MusiFy_Lib.Reports();
            List<string> artistNames = new List<string>();
            List<string> albumNames = new List<string>();

            for (int i = 0; i < 8; i++)
            {
                int index = i ;
                MusiFy_Library.Button button = new MusiFy_Library.Button();
              
                List<Artist> artists = await report.GetData<Artist>("http://localhost:1443/api/Artista");
                

              
                {

                    string artistName = artists[index].NomArtista;
                    
                    artistNames.Add(artistName);
                   
                    // Asignar estilos a los botones creados
                    button.Text = "Reporte " + i;
                    button.WidthButton = 200;
                    button.HeightButton = 50;
                    button.TextSize = 20;

                    // Asignar evento de clic con manejo asíncrono


                    switch (i)
                    {
                        case 0:
                            button.Click += async (sender, e) =>
                            {
                                crearPDF.createPDF(artistNames, "Artistas");
                            };
                            break;
                        case 1:
                            button.Click += async (sender, e) =>
                            { 

                                crearPDF.createPDF(albumNames, "Albums"); 
                            
                            };
                            break;
                    }
                   

                    // Configurar otros casos del switch si es necesario
                }

                button.WidthGrid = new GridLength(110, GridUnitType.Pixel);

                // Añadir el botón al contenedor adecuado según el índice
                if (i < 4)
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
