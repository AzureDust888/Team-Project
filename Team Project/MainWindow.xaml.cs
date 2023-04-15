
using Elasticsearch.Net;
using Nest;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Windows.Threading;
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
            //PlayerHp.DataContext = player;
            InitializeComponent();
            dir = dir.Parent?.Parent?.Parent;
            mn = this;
            dirname = dir.FullName;
            TestMap();
            Map_addObjects();


        }
        public DirectoryInfo? dir = new DirectoryInfo(Directory.GetCurrentDirectory());

        public static string dirname;

        public static Border weapon = new Border();
        public static Player player = new Player("alex", 175, 100, 1, 0);
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {

            timer.Interval = TimeSpan.FromSeconds(0.04);
            timer.Tick += Timer_Event_on_Tick;


            this.WindowState = WindowState.Maximized;
            weapon.Width = 60;
            weapon.Height = 120;
            weapon.Background = Brushes.Lime;

            //NIghtBorder.Visibility = Visibility.Hidden;
            //NIghtBorder2.Visibility = Visibility.Hidden;
            //NIghtBorder.Background = new SolidColorBrush(Color.FromArgb(100,0,0,0));
            //NIghtBorder2.BorderBrush = new SolidColorBrush(Color.FromArgb(200, 0, 0, 0));
            canvas_enemy.Children.Add(player.Player_Back_Border);
            Player_Canvas.Children.Add(player.PLayer_Front_Border);
            for (int i = 0; i < 10; i++)
            {
                EnemyClass en = new EnemyClass(100, 100, $"Cerberus");
                canvas_enemy.Children.Add(en.border);
            }


            BitmapImage img2 = new BitmapImage();

            img2.BeginInit();
            img2.StreamSource = new System.IO.MemoryStream(File.ReadAllBytes(dir.FullName + "\\Resources\\sword.png"));
            img2.EndInit();

            weapon.Background = new ImageBrush(img2);

            PlayerHp.DataContext = player;
            PlayerMp.DataContext = player;
        }

        //
        double x_cord = 0;
        double y_cord = 0;

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            canvas_enemy.Children.Remove(weapon);
            canvas_enemy.Children.Add(weapon);
            weapon.Margin = player.Player_Back_Border.Margin;

            RotateTransform rotateTransform = new RotateTransform();

            rotateTransform.CenterX = weapon.Width / 2;
            rotateTransform.CenterY = weapon.Height;

            weapon.RenderTransform = rotateTransform;

            DoubleAnimation rotationAnimation = new DoubleAnimation();
            rotationAnimation.From = 0;
            rotationAnimation.To = 120;
            rotationAnimation.Duration = TimeSpan.FromSeconds(0.1);


            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(rotationAnimation);

            Storyboard.SetTargetProperty(rotationAnimation, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));
            Storyboard.SetTarget(storyboard, weapon);
            storyboard.Completed += (sender, e) => { isattack = false; };
            storyboard.Begin();
            storyboard.Completed += delegate { timer.Stop(); };
            timer.Start();
            weapon.Visibility = Visibility.Visible;

            p = Mouse.GetPosition(this);
            double x = (player.PLayer_Front_Border.Margin.Left + 50) - p.X;
            double y = (player.PLayer_Front_Border.Margin.Top + 50) - p.Y;

            TimerX = x;
            TImerY = y;
            if (y > 100)
            {
                y = 100;
            }
            else if (y < -100)
            {
                y = -100;
            }

            if (x > 100)
            {
                x = 100;
            }
            else if (x < -100)
            {
                x = -100;
            }

            weapon.Margin = new Thickness(weapon.Margin.Left - x, weapon.Margin.Top - y, 0, 0);




            //Left top x+ y+
            //Right top x- y+
            //Right bottom x- y-
            //Left bottom x+ y-

            isattack = true;
        }

        private void Storyboard_Completed(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        DispatcherTimer timer = new DispatcherTimer();
        int ux = 0;
        int uy = 0;
        double TimerX;
        double TImerY;
        private void Timer_Event_on_Tick(object? sender, EventArgs e)
        {
            //Left top x+ y+
            //Right top x- y+
            //Right bottom x- y-
            //Left bottom x+ y-
            this.Dispatcher.Invoke(() =>
            {
                try
                {
                    if (ux >= 820)
                        ux = 0;

                    if (TImerY > 100 && TimerX >= 100) //LT
                    {
                        uy = 600;
                    }
                    else if (TImerY > 100 && TimerX <= -100) //RT
                    {
                        uy = 985;
                    }
                    else if (TImerY < -100 && TimerX <= -100) //RB
                    {
                        uy = 1350;
                    }
                    else if (TImerY < -100 && TimerX >= 100) //LB
                    {
                        uy = 220;
                    }
                    else if ((TimerX > -100 && TimerX < 100) && TImerY > 0)//U
                    {
                        uy = 785;
                    }
                    else if ((TimerX > -100 && TimerX < 100) && TImerY < 0)//D
                    {
                        uy = 0;
                    }
                    else if ((TImerY > -100 && TImerY < 100) && TimerX > 0)//L
                    {
                        uy = 405;
                    }
                    else if ((TImerY > -100 && TImerY < 100) && TimerX < 0) //R
                    {
                        uy = 1180;
                    }

                    BitmapImage img = new BitmapImage(new Uri(dir.FullName + "\\Resources\\player_topchik.png"));

                    Int32Rect cropRect = new Int32Rect(ux, uy, 120, 180);

                    CroppedBitmap croppedBitmap = new CroppedBitmap(img, cropRect);

                    player.PLayer_Front_Border.Background = new ImageBrush(croppedBitmap);

                    ux += 137;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " " + ux + " " + uy);
                }

            });
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

                From = new Thickness(player.Player_Back_Border.Margin.Left, player.Player_Back_Border.Margin.Top, player.Player_Back_Border.Margin.Right, player.Player_Back_Border.Margin.Bottom),
                To = new Thickness(toLeft, toTop, 0, 0),

                Duration = TimeSpan.FromSeconds(speed),
                AutoReverse = false,
            };
            return thicknessAnimation;
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            canvas_enemy.Children.Remove(weapon);
            weapon.Visibility = Visibility.Hidden;

            isattack = false;
        }



        bool isattack = false;
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Q && !isattack)
            {

            }

            if (e.Key == Key.Escape)
            {
                if (Menu_border.Visibility == Visibility.Visible) Menu_border.Visibility = Visibility.Hidden;
                else Menu_border.Visibility = Visibility.Visible;
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Q)
            {

            }
        }

        private void Window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            p = Mouse.GetPosition(this);
            double x = (player.PLayer_Front_Border.Margin.Left + 50) - p.X;
            double y = (player.PLayer_Front_Border.Margin.Top + 50) - p.Y;
            TimerX = x;
            TImerY = y;
            int maxSpeed = 130;
            if (x > maxSpeed) x = maxSpeed;
            else if (x < -maxSpeed) x = -maxSpeed;
            if (y > maxSpeed) y = maxSpeed;
            else if (y < -maxSpeed) y = -maxSpeed;
            timer.Stop();

            storyboard.Stop();
            ThicknessAnimation thicknessAnimation;
            ThicknessAnimation thicknessAnimation2;
            if (player.Player_Back_Border.Margin.Left <= 100)
            {
                thicknessAnimation = ThicknessAnimation(0, BT.Margin.Top, 0);
            }
            else if (player.Player_Back_Border.Margin.Top <= 100)
            {
                thicknessAnimation = ThicknessAnimation(BT.Margin.Left, 0, 0);
            }
            else if (player.Player_Back_Border.Margin.Left >= BT.Width - 200)
            {
                thicknessAnimation = ThicknessAnimation(0, BT.Margin.Top, 0);
            }
            else if (player.Player_Back_Border.Margin.Top >= BT.Width - 200)
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

            storyboard.Completed += delegate { timer.Stop(); };

            if (player.Player_Back_Border.Margin.Top <= 100)//2512, 2266
            {
                thicknessAnimation2 = ThicknessAnimation2(player.Player_Back_Border.Margin.Left, 2265, 0);
            }
            else if (player.Player_Back_Border.Margin.Top >= BT.Width - 200)
            {
                thicknessAnimation2 = ThicknessAnimation2(player.Player_Back_Border.Margin.Left, 2265, 0);
            }
            else if (player.Player_Back_Border.Margin.Left >= BT.Width - 200)
            {
                thicknessAnimation2 = ThicknessAnimation2(2510, player.Player_Back_Border.Margin.Top, 0);
            }
            else if (player.Player_Back_Border.Margin.Left <= 100)
            {
                thicknessAnimation2 = ThicknessAnimation2(2510, player.Player_Back_Border.Margin.Top, 0);
            }
            else
            {
                thicknessAnimation2 = ThicknessAnimation2(player.Player_Back_Border.Margin.Left - x, player.Player_Back_Border.Margin.Top - y, 0.6);
            }
            var r = new Storyboard();

            Storyboard.SetTargetProperty(thicknessAnimation2, new PropertyPath(FrameworkElement.MarginProperty));
            r.Children.Add(thicknessAnimation2);
            r.Begin(player.Player_Back_Border);
            timer.Start();
            Title = Math.Round(player.Player_Back_Border.Margin.Left).ToString() + " " + Math.Round(player.Player_Back_Border.Margin.Top).ToString();
        }

        int cellWidth = 256; // ширина ячейки сетки
        int cellHeight = 256; // высота ячейки сетки
        const int rows = 20; // количество строк
        const int cols = 20; // количество столбцов
        async void TestMap()
        {
            BitmapImage img0 = new BitmapImage();
            img0.BeginInit();
            img0.StreamSource = new System.IO.MemoryStream(File.ReadAllBytes(dir.FullName + "\\Resources\\ground2.png"));
            img0.EndInit();
            BitmapImage img1 = new BitmapImage();
            img1.BeginInit();
            img1.StreamSource = new System.IO.MemoryStream(File.ReadAllBytes(dir.FullName + "\\Resources\\ground5.png"));
            img1.EndInit();

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Image image = new Image();
                    image.Source = img0;
                    image.Width = cellWidth;
                    image.Height = cellHeight;
                    Canvas.SetLeft(image, col * cellWidth - BT.Width / 2);    //bt.width для отступа влево т.к. привязка к другим координатам, а они слишком уехали вправо
                    Canvas.SetTop(image, row * cellHeight - BT.Height / 2);

                    Map_canvas.Children.Add(image);
                }
            }

        }

        void Map_addObjects()
        {

            ObservableCollection<BitmapImage> imgs = new ObservableCollection<BitmapImage>();
            imgs.Add(new BitmapImage(new Uri(dir.FullName + "\\Resources\\grass.png")));
            imgs.Add(new BitmapImage(new Uri(dir.FullName + "\\Resources\\tree.png")));
            imgs.Add(new BitmapImage(new Uri(dir.FullName + "\\Resources\\tree2.png")));
            imgs.Add(new BitmapImage(new Uri(dir.FullName + "\\Resources\\tree3.png")));
            imgs.Add(new BitmapImage(new Uri(dir.FullName + "\\Resources\\tree4.png")));
            imgs.Add(new BitmapImage(new Uri(dir.FullName + "\\Resources\\tree5.png")));
            imgs.Add(new BitmapImage(new Uri(dir.FullName + "\\Resources\\tree6.png")));


            int[,] map = new int[rows, cols]
            {
                {2,2,0,0,0,0,0,2,2,0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,1,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0 },
                {0,0,0,1,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,1,2,0,1,0,0,0,0,0,0,0,0,0,0,0,0,1 },
                {0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2 },
                {0,0,0,0,0,1,0,0,0,0,0,0,1,1,1,1,0,0,0,1 },
                {0,0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,0,0,0,1 },
                {0,0,0,0,0,0,2,0,0,0,1,0,0,0,0,0,0,0,0,2 },
                {0,0,0,0,0,0,2,0,0,1,0,2,2,0,0,0,0,0,0,2 },
                {0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,2 },
                {0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,2 },
                {0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,0,2 },
                {0,0,0,0,0,0,1,0,0,3,0,0,0,0,0,0,1,0,0,0 },
                {2,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0 },
                {2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0 },
                {0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {2,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            };

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    switch (map[col, row])
                    {
                        case 1:
                            {
                                Image image = new Image();
                                image.Source = imgs[6];
                                image.Width = cellWidth;
                                image.Height = cellHeight;
                                Canvas.SetLeft(image, col * cellWidth - BT.Width / 2);    //bt.width для отступа влево т.к. привязка к другим координатам, а они слишком уехали вправо
                                Canvas.SetTop(image, row * cellHeight - BT.Height / 2);

                                MapItems_canvas.Children.Add(image);

                            }

                            break;
                        case 2:
                            {
                                Image image = new Image();
                                image.Source = imgs[1];
                                image.Width = cellWidth;
                                image.Height = cellHeight;
                                Canvas.SetLeft(image, col * cellWidth - BT.Width / 2);    //bt.width для отступа влево т.к. привязка к другим координатам, а они слишком уехали вправо
                                Canvas.SetTop(image, row * cellHeight - BT.Height / 2);

                                MapItems_canvas.Children.Add(image);
                            }
                            break;
                        case 3:
                            {
                                Image image = new Image();
                                image.Source = imgs[0];
                                image.Width = cellWidth;
                                image.Height = cellHeight;
                                Canvas.SetLeft(image, col * cellWidth - BT.Width / 2);    //bt.width для отступа влево т.к. привязка к другим координатам, а они слишком уехали вправо
                                Canvas.SetTop(image, row * cellHeight - BT.Height / 2);

                                MapItems_canvas.Children.Add(image);
                            }
                            break;
                    }
                }
            }

        }

        private void Exit(object sender, RoutedEventArgs e)
        {

            var pr = Process.GetProcesses();
            foreach (var p in pr)
            {
                if (p.ProcessName.Contains("Team Project"))
                    p.Kill();
            }
        }

    }
}
