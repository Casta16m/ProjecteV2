﻿using System;
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
    /// Lógica de interacción para Paginacio.xaml
    /// </summary>
    public partial class Paginacio : Window
    {
        public Paginacio(string pdfPath)
        {
            InitializeComponent();
            pdfViewer.Load(pdfPath);
        }
    }
}