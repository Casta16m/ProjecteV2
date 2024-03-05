﻿using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftWindowsWPF;
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
        /// <summary>
        /// Temporizador de 5 segundos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            GetAllArtistsGrup();
        }

        /// <summary>
        /// Obtiene todos los artistas Grup
        /// </summary>
        private async void GetAllArtistsGrup()
        {
            Reports reports = new Reports();
            string url = $"{BaseUrlSql}Grup";
            List<Grups> grup = await reports.GetData<Grups>(url);
            if(grup != null)
            {
                lvArtistsGrup.ItemsSource = grup;
            }
            
        }

        /// <summary>
        /// Crea un artista Grup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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