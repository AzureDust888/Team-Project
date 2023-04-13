
using Nest;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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
using System.Xml.Linq;

namespace Team_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Point p = new Point();
        Storyboard storyboard = new Storyboard();
        public MainWindow()
        {
            this.DataContext = this;
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           Border b = new Border();

            
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            p = Mouse.GetPosition(this);
            double x = (Player.Margin.Left + 50) - p.X;
            double y = (Player.Margin.Top + 50) - p.Y;

            //MessageBox.Show(Player.Margin.Left.ToString() + " " + p.X.ToString()); d = √[(x₂ - x₁)² + (y₂ - y₁)² + (z₂ - z₁)²]

            //double t = Math.Sqrt(Math.Pow((p.X - x), 2) + Math.Pow((p.Y - y), 2));
            //MessageBox.Show("x1 = " + p.X + " y1 = " + p.Y + " x2 = " + x + " y2 = " + y);
            
            storyboard.Stop();
            var thicknessAnimation = new ThicknessAnimation
            {

                From = new Thickness(BT.Margin.Left, BT.Margin.Top, BT.Margin.Right, BT.Margin.Bottom),
                To = new Thickness(BT.Margin.Left + x, BT.Margin.Top + y, 0, 0),
                
                Duration = TimeSpan.FromSeconds(0.3),
                AutoReverse = false,

            };

            Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath(FrameworkElement.MarginProperty));    
            storyboard.Children.Add(thicknessAnimation);
            storyboard.Begin(BT);
            BT.Margin    = new Thickness(3);

        }

      
    }
}
