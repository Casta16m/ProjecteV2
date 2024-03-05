﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Musify_Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            ReportButton.SourceImageButton = new BitmapImage(new Uri("pack://application:,,,/MusiFy-Library;component/Images/report.png", UriKind.Absolute));
            AdminButton.SourceImageButton = new BitmapImage(new Uri("pack://application:,,,/MusiFy-Library;component/Images/admin.png", UriKind.Absolute));

        }

        /// <summary>
        /// Method to navigate to the Reports page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportButton_Click(object sender, RoutedEventArgs e)
        {
            ReportPage reportPage = new ReportPage();
            reportPage.Show();
        }

        /// <summary>
        /// Method to navigate to the Admin page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            Admin adminPage = new Admin();
            adminPage.Show();
        }

        /// <summary>
        /// Method to navigate to the Home page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}