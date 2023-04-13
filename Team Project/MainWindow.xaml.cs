
using Nest;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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
        public static MainWindow mn = new MainWindow();
        public MainWindow()
        {

            this.DataContext = this;
            InitializeComponent();
            dir = dir.Parent?.Parent?.Parent;
            mn = this;
        }
        DirectoryInfo? dir = new DirectoryInfo(Directory.GetCurrentDirectory());

        int count;

        public class EnemyClass : MainWindow
        {
            public Border border = new Border();
            Storyboard storyboard = new Storyboard();
            public EnemyClass()
            {
                
                border.Width = 100;
                border.Height = 100;
                border.Background = Brushes.Black;
                border.Margin = new Thickness(100);

               
                
                var thicknessAnimation = new ThicknessAnimation
                {

                    From = new Thickness(border.Margin.Left, border.Margin.Top, 0, 0),
                    To = new Thickness(border.Margin.Left + 200, border.Margin.Top + 200, 0, 0),

                    Duration = TimeSpan.FromSeconds(2),

                    AutoReverse = true,
                    RepeatBehavior = RepeatBehavior.Forever


                };
                
                Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath(FrameworkElement.MarginProperty));
                storyboard.Children.Add(thicknessAnimation);
                storyboard.Begin(border);

                Thread t = new Thread(() =>
                {
                    while (true)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            Rect rect1 = new Rect(border.Margin.Left, border.Margin.Top, ENT.Width, ENT.Height);
                            Rect rect2 = new Rect(b.Margin.Left, b.Margin.Top, b.Width, b.Height);

                            if (rect1.IntersectsWith(rect2))
                            {
                                MessageBox.Show("alo");
                            }

                        });
                        Thread.Sleep(500);
                        //Dispatcher.Invoke(() =>
                        //{
                        //    Title = Player.Margin + " " + Player2.Margin + "map: " + BT.Margin;
                        //});
                    }
                });
                t.SetApartmentState(ApartmentState.STA);
                t.Start();

            }
        }
        Border b = new Border();
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
           

            b.Width = 100;
            b.Height = 100;
            b.Background = Brushes.White;
            b.Margin = new Thickness(1950, 2033, 0,0);

            Rectangle rectangle = new Rectangle();
            
            EnemyClass en = new EnemyClass();
            canvas_enemy.Children.Add(b);
           
            canvas_enemy.Children.Add(en.border);


            Thread t = new Thread(() =>
            {
                while (true)
                {
                    Dispatcher.Invoke(() =>
                    {
                        Rect rect1 = new Rect(ENT.Margin.Left, ENT.Margin.Top, ENT.Width, ENT.Height);
                        Rect rect2 = new Rect(b.Margin.Left, b.Margin.Top, b.Width, b.Height);

                        if (rect1.IntersectsWith(rect2))
                        {
                            MessageBox.Show("alo");
                        }

                    });
                    Thread.Sleep(500);
                    //Dispatcher.Invoke(() =>
                    //{
                    //    Title = Player.Margin + " " + Player2.Margin + "map: " + BT.Margin;
                    //});
                }
            });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();

            //BitmapImage img = new BitmapImage();

            //img.BeginInit();
            //img.StreamSource = new System.IO.MemoryStream(File.ReadAllBytes(dir.FullName + "\\Resources\\ddd.jpg"));
            //img.EndInit();

            //Player.Background = new ImageBrush(img);

        }


        double x_cord = 0;
        double y_cord = 0;

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            p = Mouse.GetPosition(this);
            double x = (Player.Margin.Left + 50) - p.X;
            double y = (Player.Margin.Top + 50) - p.Y;

            
            storyboard.Stop();
            ThicknessAnimation thicknessAnimation;

            if (BT.Margin.Left <= (BT.Width / 2) * -1)
            {
                thicknessAnimation = ThicknessAnimation(0, BT.Margin.Top + y, 0);
            }
            else if (BT.Margin.Top >= (BT.Height / 2))
            {
                thicknessAnimation = ThicknessAnimation(BT.Margin.Left, 0, 0);
            }
            else if (BT.Margin.Left >= (BT.Width / 2))
            {
                thicknessAnimation = ThicknessAnimation(0, BT.Margin.Top + y, 0);
            }
            else if (BT.Margin.Top <= (BT.Height / 2) * -1)
            {
                thicknessAnimation = ThicknessAnimation(BT.Margin.Left, 0, 0);
            }
            else
            {
                thicknessAnimation = ThicknessAnimation(BT.Margin.Left + x, BT.Margin.Top + y, 0.6);
            }
            
            Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath(FrameworkElement.MarginProperty));
            storyboard.Children.Add(thicknessAnimation);
            storyboard.Begin(BT);

            var r = new Storyboard();
            thicknessAnimation = ThicknessAnimation2(b.Margin.Left - x, b.Margin.Top - y, 0.6);
            Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath(FrameworkElement.MarginProperty));
            r.Children.Add(thicknessAnimation);
            r.Begin(b);

            Title = b.Margin.ToString() + "\n " + BT.Margin.ToString();


        }

        ThicknessAnimation ThicknessAnimation(double toLeft, double toTop, double speed)
        {
            var thicknessAnimation = new ThicknessAnimation
            {

                From = new Thickness(BT.Margin.Left, BT.Margin.Top, BT.Margin.Right, BT.Margin.Bottom),
                To = new Thickness(toLeft, toTop, 0, 0),

                Duration = TimeSpan.FromSeconds(speed),
                AutoReverse = false,
            };
            return thicknessAnimation;
        }

        ThicknessAnimation ThicknessAnimation2(double toLeft, double toTop, double speed)
        {
            var thicknessAnimation = new ThicknessAnimation
            {

                From = new Thickness(b.Margin.Left, b.Margin.Top, b.Margin.Right, b.Margin.Bottom),
                To = new Thickness(toLeft, toTop, 0, 0),

                Duration = TimeSpan.FromSeconds(speed),
                AutoReverse = false,
            };
            return thicknessAnimation;
        }

    }
}
