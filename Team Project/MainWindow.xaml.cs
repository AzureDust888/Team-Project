
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
        Storyboard storyboards = new Storyboard();
        Storyboard storyboard = new Storyboard();
        public MainWindow()
        {
            this.DataContext = this;
            InitializeComponent();
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           Border b = new Border();


            var thicknessAnimation = new ThicknessAnimation
            {

                From = new Thickness(Enemy.Margin.Left, Enemy.Margin.Top, 0, 0),
                To = new Thickness(Enemy.Margin.Left + 200, Enemy.Margin.Top + 200, 0, 0),

                Duration = TimeSpan.FromSeconds(2),

                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever

            };

            Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath(FrameworkElement.MarginProperty));
            storyboards.Children.Add(thicknessAnimation);
            storyboards.Begin(Enemy);

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            p = Mouse.GetPosition(this);
            double x = (Player.Margin.Left + 50) - p.X;
            double y = (Player.Margin.Top + 50) - p.Y;


            this.Title = BT.Margin.Left + " " + BT.Margin.Top + " ";
            storyboard.Stop();
            ThicknessAnimation thicknessAnimation;

            if (BT.Margin.Left <= (BT.Width/2) * -1)
            {
                thicknessAnimation = new ThicknessAnimation
                {

                    From = new Thickness(BT.Margin.Left, BT.Margin.Top, BT.Margin.Right, BT.Margin.Bottom),
                    To = new Thickness(0, BT.Margin.Top + y, 0, 0),

                    Duration = TimeSpan.FromSeconds(0),
                    AutoReverse = false,
                };
            }
            else if (BT.Margin.Top >= (BT.Height / 2))
            {
                thicknessAnimation = new ThicknessAnimation
                {

                    From = new Thickness(BT.Margin.Left, BT.Margin.Top, BT.Margin.Right, BT.Margin.Bottom),
                    To = new Thickness(BT.Margin.Left, 0, 0, 0),
                    Duration = TimeSpan.FromSeconds(0),
                    AutoReverse = false,
                };
            }
            else if (BT.Margin.Left >= (BT.Width / 2))
            {
                thicknessAnimation = new ThicknessAnimation
                {

                    From = new Thickness(BT.Margin.Left, BT.Margin.Top, BT.Margin.Right, BT.Margin.Bottom),
                    To = new Thickness(0, BT.Margin.Top + y, 0, 0),

                    Duration = TimeSpan.FromSeconds(0),
                    AutoReverse = false,
                };
            }
            else if (BT.Margin.Top <= (BT.Height / 2) * -1)
            {
                thicknessAnimation = new ThicknessAnimation
                {

                    From = new Thickness(BT.Margin.Left, BT.Margin.Top, BT.Margin.Right, BT.Margin.Bottom),
                    To = new Thickness(BT.Margin.Left, 0, 0, 0),

                    Duration = TimeSpan.FromSeconds(0),
                    AutoReverse = false,
                };
            }
            else
            {
                thicknessAnimation = new ThicknessAnimation
                {

                    From = new Thickness(BT.Margin.Left, BT.Margin.Top, BT.Margin.Right, BT.Margin.Bottom),
                    To = new Thickness(BT.Margin.Left + x, BT.Margin.Top + y, 0, 0),

                    Duration = TimeSpan.FromSeconds(0.3),
                    AutoReverse = false,
                };
            }
            Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath(FrameworkElement.MarginProperty));
            storyboard.Children.Add(thicknessAnimation);
            storyboard.Begin(BT);
        }


    }
}
