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
using System.IO;
using javax.swing.text.html;

namespace Musify_Desktop
{
    /// <summary>
    /// Lógica de interacción para ReportWindow.xaml
    /// </summary>
    public partial class ReportPage : Window{
        


        private MusiFy_Lib.Reports reports = new MusiFy_Lib.Reports();
        private CreatePDF crearPDF = new CreatePDF();
       

        public ReportPage()
        {
            InitializeComponent();
       
          
            CreateReportButtons();
        }


        private void CreateReportButtons()
        {
            for(int i = 0; i < 4; i++)
            {


                int index = i;
                MusiFy_Library.Button button = new MusiFy_Library.Button();
                button.WidthGrid = new GridLength(100, GridUnitType.Pixel);
                button.WidthButton = 200;
                button.HeightButton = 40;
                button.TextSize= 20;
                button.Text= "Button " + i;
                ButtonsContainer.Children.Add(button);


                switch (index)
                {
                    case 0:
                        button.Click += async (sender, e) =>
                        {
                            List<Artist> artists = await reports.GetData<Artist>("http://172.23.1.231:1443/api/Artista");
                            List<string?> artisNames = new List<string>();
                            artisNames = artists.Select(x => x.NomArtista).ToList();
                            CreatePDFAndOpenFileDialog(artisNames);
                         

                        };
                        break;
                    case 1:
                        button.Click += async (sender, e) => {

                            List<Album> albums = await reports.GetData<Album>("http://172.23.1.231:1443/api/Artista");
                            List<string?> albumNames = new List<string>();
                            albumNames = albums.Select(x => x.NomAlbum).ToList();
                            CreatePDFAndOpenFileDialog(albumNames);
                                               


                        };
                        break;

                }


             

            }
        }




        private async void CreatePDFAndOpenFileDialog
            ( List<string?> content)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                crearPDF.createPDF(content, saveFileDialog.FileName);
            }
        }

        private void CargarArchivosListView(string carpeta)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(carpeta);
                FileInfo[] archivos = di.GetFiles();
                List<string> listaArchivos = new List<string>();
                foreach (FileInfo archivo in archivos)
                {
                    listaArchivos.Add(archivo.Name);
                }
                ListaPDF.ItemsSource = listaArchivos;

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }


      

      
       





      





        
    }
}
