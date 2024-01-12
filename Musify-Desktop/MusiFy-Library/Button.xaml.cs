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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;




namespace MusiFy_Library
{
    /// <summary>
    /// Lógica de interacción para Button.xaml
    /// </summary>
    public partial class Button : UserControl
    {




        public event RoutedEventHandler? Click;
        
      
        public string Text
        {
            get { return ButtonName.Text; }
            set { ButtonName.Text = value; }
        }
       
        public GridLength WidthGrid
        {
            get { return ButtonNameSpace.ColumnDefinitions[1].Width; }
            set { ButtonNameSpace.ColumnDefinitions[1].Width = value; }
        }
       
        public int WidthButton
        {
            get { return (int)btn_glb.Width; }
            set { btn_glb.Width = value; }
        }


       


        public int HeightButton
        {
            get { return (int)btn_glb.Height; }
            set { btn_glb.Height = value; }
        }
        public int TextSize 
        {
            get { return (int)ButtonName.FontSize; }
            set { ButtonName.FontSize = value; }
        }


        public ImageSource SourceImageButton {
            get { return ButtonImage.Source; }
            set { ButtonImage.Source = value; }
        }


        public int ImageSize
        {
            get { return (int)ButtonImage.Width; }
            set { ButtonImage.Width = value; ButtonImage.Height = value; }
        }

      

        public Button()
        {
            InitializeComponent();


            btn_glb.Click += (s, e) =>
            {
                Click?.Invoke(s, e);
            };

        }

      

        protected virtual void OnClick()
        {
            Click?.Invoke(this, new RoutedEventArgs());
        }

        private void myButton_MouseEnter(object sender, MouseEventArgs e)
        {
            ScaleTransform scaleTransform = (ScaleTransform)btn_glb.RenderTransform;
            DoubleAnimation animation = new DoubleAnimation(1.05, new Duration(TimeSpan.FromSeconds(0.3)));
            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }


      
        private void myButton_MouseLeave(object sender, MouseEventArgs e)
        {
            ScaleTransform scaleTransform = (ScaleTransform)btn_glb.RenderTransform;
            DoubleAnimation animation = new DoubleAnimation(1, new Duration(TimeSpan.FromSeconds(0.3)));
            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }
    }
}
