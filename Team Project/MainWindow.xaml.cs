﻿
using Elasticsearch.Net;
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


        public class EnemyClass : MainWindow
        {
            public Border border = new Border();
            Storyboard storyboard = new Storyboard();
            Storyboard storyboard2 = new Storyboard();
            Storyboard storyboard3 = new Storyboard();
            Storyboard storyboard4 = new Storyboard();
            Storyboard storyboard5 = new Storyboard();

            bool isrunning = false;
            bool isc = true;
            ThicknessAnimation thicknessAnimation;
            Point cord = new Point();
            public EnemyClass()
            {
                
                border.Width = 100;
                border.Height = 100;
                border.Background = Brushes.Black;
                border.Margin = new Thickness(new Random().Next(4000), new Random().Next(4000), 0, 0);

                cord.X = border.Margin.Left;
                cord.Y = border.Margin.Top;
                storyboard4.Completed += Storyboard_Completed;
                storyboard5.Completed += Storyboard_Completed;
                thicknessAnimation = new ThicknessAnimation
                {

                    From = new Thickness(cord.X, cord.Y, 0, 0),
                    To = new Thickness(cord.X + 200, cord.Y + 200, 0, 0),

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
                        
                        Dispatcher.Invoke(new Action(() =>
                        {
                         
                           

                            if(Math.Abs(border.Margin.Left - MainWindow.b.Margin.Left) <= 250 && Math.Abs(border.Margin.Top - MainWindow.b.Margin.Top) <= 250 && isc)
                            {
                                storyboard.Pause();
                                thicknessAnimation = new ThicknessAnimation
                                {

                                    From = new Thickness(border.Margin.Left, border.Margin.Top, 0, 0),
                                    To = new Thickness(MainWindow.b.Margin.Left, MainWindow.b.Margin.Top, 0, 0),

                                    Duration = TimeSpan.FromSeconds(0.7),

                                    AutoReverse = false

                                };

                                border.Background = Brushes.Yellow;

                                Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath(FrameworkElement.MarginProperty));
                                storyboard2.Children.Add(thicknessAnimation);
                                storyboard2.Begin(border);
                                isrunning = true;
                            }
                            else
                            {
                                if(isrunning)
                                {
                                   
                                    thicknessAnimation = new ThicknessAnimation
                                    {

                                        From = new Thickness(border.Margin.Left, border.Margin.Top, 0, 0),
                                        To = new Thickness(cord.X, cord.Y, 0, 0),

                                        Duration = TimeSpan.FromSeconds(2)

                                    };

                                    Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath(FrameworkElement.MarginProperty));
                                    storyboard4.Children.Add(thicknessAnimation);
                                    storyboard4.Begin(border);
                                    border.Background = Brushes.Black;
                                    isrunning = false;
                                    isc = false;
                                }
                              
                                
                                

                            }
                            if (Math.Abs(border.Margin.Left - cord.X) >= 500 || Math.Abs(border.Margin.Top - cord.Y) >= 500)
                            {
                                thicknessAnimation = new ThicknessAnimation
                                {

                                    From = new Thickness(border.Margin.Left, border.Margin.Top, 0, 0),
                                    To = new Thickness(cord.X, cord.Y, 0, 0),

                                    Duration = TimeSpan.FromSeconds(2)

                                };

                                Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath(FrameworkElement.MarginProperty));
                                storyboard5.Children.Add(thicknessAnimation);
                                storyboard5.Begin(border);
                                border.Background = Brushes.Black;
                                isc = false;
                            }

                        }));
                        Thread.Sleep(300);
                    }
                });
                t.Start();

            }

            private void Storyboard_Completed(object? sender, EventArgs e)
            {
                isc = true;
                thicknessAnimation = new ThicknessAnimation
                {

                    From = new Thickness(cord.X, cord.Y, 0, 0),
                    To = new Thickness(cord.X + 200, cord.Y + 200, 0, 0),

                    Duration = TimeSpan.FromSeconds(2),

                    AutoReverse = true,

                    RepeatBehavior = RepeatBehavior.Forever

                };

                Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath(FrameworkElement.MarginProperty));
                storyboard3.Children.Add(thicknessAnimation);
                storyboard3.Begin(border);
            }

            public void stop_s()
            {
                storyboard.Stop();
            }
        }
        public static Border b = new Border();
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
            
            b.Width = 100;
            b.Height = 100;
            b.Background = Brushes.White;
            b.Margin = new Thickness(2510, 2265, 0,0);

            Rectangle rectangle = new Rectangle();
            
            
           
            canvas_enemy.Children.Add(b);
            for (int i = 0; i < 10; i++)
            {
                EnemyClass en = new EnemyClass();
                canvas_enemy.Children.Add(en.border);
            }
            
        


            BitmapImage img = new BitmapImage();

            img.BeginInit();
            img.StreamSource = new System.IO.MemoryStream(File.ReadAllBytes(dir.FullName + "\\Resources\\image.png"));
            img.EndInit();

            Player.Background = new ImageBrush(img);

            //MessageBox.Show(Math.Round(b.Margin.Left).ToString() + " " + Math.Round(b.Margin.Top).ToString() + "||" + Math.Round(BT.Margin.Left).ToString() + " " + Math.Round(BT.Margin.Top).ToString());

        }


        double x_cord = 0;
        double y_cord = 0;

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            p = Mouse.GetPosition(this);
            double x = (Player.Margin.Left + 50) - p.X;
            double y = (Player.Margin.Top + 50) - p.Y;

            this.WindowState = WindowState.Maximized;
            storyboard.Stop();
            ThicknessAnimation thicknessAnimation;
            ThicknessAnimation thicknessAnimation2;
            if (b.Margin.Left <= 100)
            {
                thicknessAnimation = ThicknessAnimation(0, BT.Margin.Top, 0);
            }
            else if (b.Margin.Top <= 100)
            {
                thicknessAnimation = ThicknessAnimation(BT.Margin.Left, 0, 0);
            }
            else if (b.Margin.Left >= 3800)
            {
                thicknessAnimation = ThicknessAnimation(0, BT.Margin.Top, 0);
            }
            else if (b.Margin.Top >= 3800)
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


            if (b.Margin.Top <= 100)//2512, 2266
            {
                thicknessAnimation2 = ThicknessAnimation2(b.Margin.Left, 2265, 0);
            }
            else if(b.Margin.Top >= 3800)
            {
                thicknessAnimation2 = ThicknessAnimation2(b.Margin.Left, 2265, 0);
            }
            else if(b.Margin.Left >= 3800)
            {
                thicknessAnimation2 = ThicknessAnimation2(2510, b.Margin.Top, 0);
            }
            else if(b.Margin.Left <= 100)
            {
                thicknessAnimation2 = ThicknessAnimation2(2510, b.Margin.Top, 0);
            }
            else
            {
                thicknessAnimation2 = ThicknessAnimation2(b.Margin.Left - x, b.Margin.Top - y, 0.6);
            }
            var r = new Storyboard();
            
            Storyboard.SetTargetProperty(thicknessAnimation2, new PropertyPath(FrameworkElement.MarginProperty));
            r.Children.Add(thicknessAnimation2);
            r.Begin(b);

            Title = Math.Round(b.Margin.Left).ToString() + " " + Math.Round(b.Margin.Top).ToString();

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

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            //MessageBox.Show(Math.Round(b.Margin.Left).ToString() + " " + Math.Round(b.Margin.Top).ToString() + "||" + Math.Round(BT.Margin.Left).ToString() + " " + Math.Round(BT.Margin.Top).ToString());
        }
    }
}
