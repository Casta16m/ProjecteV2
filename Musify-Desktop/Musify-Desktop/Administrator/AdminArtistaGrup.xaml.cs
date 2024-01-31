using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftWindowsWPF;
using MusiFy_Lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Threading;

namespace Musify_Desktop
{
    /// <summary>
    /// Lógica de interacción para AdminArtistaGrup.xaml
    /// </summary>
    public partial class AdminArtistaGrup : Window
    {
        string BaseUrlSql = ConfigurationManager.AppSettings["BaseUrlSql"];
        public AdminArtistaGrup()
        {
            InitializeComponent();
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += Timer_Tick;
            timer.Start();
            GetAllArtistsGrup();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            GetAllArtistsGrup();
        }
        private async void GetAllArtistsGrup()
        {
            Reports reports = new Reports();
            string url = $"{BaseUrlSql}Grup";
            List<Grups> grup = await reports.GetData<Grups>(url);

            if (grup != null)
            {
                List<string> groupNames = grup.Select(g => g.NomGrup).ToList();
                foreach (string groupName in groupNames)
                {
                    Grups specificGrup = grup.FirstOrDefault(g => g.NomGrup == groupName);
                    // Make another GET request to fetch more details about the specific group

                    string specificGrupUrl = $"{BaseUrlSql}Grup/BuscarNom/{specificGrup.NomGrup}";
                    Grups detailedGrup = await reports.GetDataOne<Grups>(specificGrupUrl);
                    if (detailedGrup != null)
                    {
                        lvArtistsGrup.ItemsSource = new List<Grups> { detailedGrup };
                    }
                    else
                    {
                        MessageBox.Show("No hi ha cap song");
                    }
                }
            }
        }


        public async void btCreateArtistesGrupClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var grup = txtNom.Text;
                var artista = txtArtistes.Text;
                string url = $"{BaseUrlSql}Grup/AfegirArtista/{grup}/{artista}/";
                Reports rep = new Reports();






                bool success = await rep.CreateDataWitoutJSON(url);
                if (success == true)
                {
                    // El objeto se actualizó correctamente
                }
                else
                {
                    // Hubo un error al actualizar el objeto
                    MessageBox.Show("Hubo un error al actualizar la canción. Por favor, inténtalo de nuevo.");
                }

            }
            catch (Exception ex)
            {
                // Aquí puedes manejar la excepción y mostrar un mensaje al usuario
                MessageBox.Show($"Se produjo un error: {ex.Message}");
            }


        }
    }

}