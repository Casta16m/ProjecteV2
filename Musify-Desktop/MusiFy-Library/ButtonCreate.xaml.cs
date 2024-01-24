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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MusiFy_Library
{
    /// <summary>
    /// Lógica de interacción para ButonCreate.xaml
    /// </summary>
    public partial class ButtonCreate : UserControl
    {
        public event RoutedEventHandler? Click;
        public ButtonCreate()
        {
            InitializeComponent();


            this.MyButton.Click += (s, e) =>
            {
                Click?.Invoke(s, e);
            };

        }
        protected virtual void OnClick()
        {
            Click?.Invoke(this, new RoutedEventArgs());
        }
    }
}
