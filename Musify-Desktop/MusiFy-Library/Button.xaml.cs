using BenchmarkDotNet.Reports;
using Prometheus;
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
        
        public static readonly DependencyProperty BotonBackgroundProperty =
        DependencyProperty.Register("BotonBackground", typeof(Brush), typeof(Button), new PropertyMetadata(Brushes.Transparent));


        /// <summary>
        /// Event to handle the click
        /// </summary>
        public event RoutedEventHandler? Click;
        
      
      /// <summary>
      /// Property to set the text of the button
      /// </summary>
        public string Text
        {
            get { return ButtonName.Text; }
            set { ButtonName.Text = value; }
        }



        /// <summary>
        /// Property to set the width of TextBlock inside the button
        /// </summary>
        public GridLength WidthGrid
        {
            get { return ButtonNameSpace.ColumnDefinitions[1].Width; }
            set { ButtonNameSpace.ColumnDefinitions[1].Width = value; }
        }

        /// <summary>
        /// Property to set the width of Image inside the button
        /// </summary>
        public GridLength WidthGridImage
        {
            get { return ButtonNameSpace.ColumnDefinitions[0].Width; }
            set { ButtonNameSpace.ColumnDefinitions[0].Width = value; }
        }

        /// <summary>
        /// Property to set the background of the button
        /// </summary>
        public Brush BotonBackground
        {
            get { return (Brush)GetValue(BotonBackgroundProperty); }
            set { SetValue(BotonBackgroundProperty, value); }
        }

        /// <summary>
        /// Property to set the width of the button
        /// </summary>
        public int WidthButton
        {
            get { return (int)btn_glb.Width; }
            set { btn_glb.Width = value; }
        }

        /// <summary>
        /// Property to set the height of the button
        /// </summary>
        public int HeightButton
        {
            get { return (int)btn_glb.Height; }
            set { btn_glb.Height = value; }
        }

        /// <summary>
        /// Property to set the font size of the text inside the button
        /// </summary>
        public int TextSize 
        {
            get { return (int)ButtonName.FontSize; }
            set { ButtonName.FontSize = value; }
        }

        /// <summary>
        /// Property to set the image of the Control 
        /// </summary>
        public ImageSource SourceImageButton {
            get { return ButtonImage.Source; }
            set { ButtonImage.Source = value; }
        }

        /// <summary>
        /// Property to set the margin of the text inside the button
        /// </summary>
        public int MarginName
        {
            get { return (int)ButtonName.Margin.Left; }
            set { ButtonName.Margin = new Thickness(value); }
        }

        /// <summary>
        /// Property to set the size of the image inside the button
        /// </summary>
        public int ImageSize
        {
            get { return (int)ButtonImage.Width; }
            set { ButtonImage.Width = value; ButtonImage.Height = value; }
        }
        
        /// <summary>
        /// Property to set the margin of the image inside the button
        /// </summary>
        public Button()
        {
            InitializeComponent();

            btn_glb.Click += (s, e) =>
            {
                Click?.Invoke(s, e);
            };

        }

        /// <summary>
        /// Method to handle the click event
        /// </summary>
        protected virtual void OnClick()
        {
            Click?.Invoke(this, new RoutedEventArgs());
        }

        /// <summary>
        /// Method to handle the mouse enter event and animate the button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myButton_MouseEnter(object sender, MouseEventArgs e)
        {
            ScaleTransform scaleTransform = (ScaleTransform)btn_glb.RenderTransform;
            DoubleAnimation animation = new DoubleAnimation(1.05, new Duration(TimeSpan.FromSeconds(0.3)));
            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }

        /// <summary>
        /// Method to handle the mouse leave event and animate the button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myButton_MouseLeave(object sender, MouseEventArgs e)
        {
            ScaleTransform scaleTransform = (ScaleTransform)btn_glb.RenderTransform;
            DoubleAnimation animation = new DoubleAnimation(1, new Duration(TimeSpan.FromSeconds(0.3)));
            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }
    }
}
