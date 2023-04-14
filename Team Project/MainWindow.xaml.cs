
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

            //DrawIsometricGrid(); 
            TestMap();
        }
        DirectoryInfo? dir = new DirectoryInfo(Directory.GetCurrentDirectory());


        public class EnemyClass : MainWindow
        {
            public string Name { get; set; }
            public double Hp { get; set; }
            public double Mp { get; set; }
            public int Lvl { get; set; }
            public double Exp { get; set; }

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
            public EnemyClass(double hptmp, double mptmp)
            {
                Hp = hptmp;
                Mp = mptmp;
                border.Width = 100;
                border.Height = 100;
                border.Background = Brushes.Black;
                border.Margin = new Thickness(new Random().Next(3500), new Random().Next(3500), 0, 0);

                ProgressBar hp = new ProgressBar()
                {
                    BorderThickness = new Thickness(0),
                    Foreground = Brushes.Tomato,
                    Maximum = 150,
                    Value = Hp,
                    Width = 100,
                    Height = 20,
                    Margin = new Thickness(0, -30, 0, 0),
                    Background = Brushes.Transparent,
                };
                ProgressBar mp = new ProgressBar()
                {
                    BorderThickness = new Thickness(0),
                    Foreground = Brushes.Blue,
                    Maximum = 150,
                    Value = Mp,
                    Width = 100,
                    Height = 20,
                    Margin = new Thickness(0, 0, 0, 0),
                    Background = Brushes.Transparent,
                };
                Label hplabel = new Label()
                {
                    Content = Hp,
                    Margin = new Thickness(90, -20, 0, 0),
                };
                Label mplabel = new Label()
                {
                    Content = Mp,
                    Margin = new Thickness(90, 0, 0, 0),
                };
                Canvas ca = new Canvas();
                ca.Margin = new Thickness(0, -40, 0, 0);
                ca.Children.Add(hplabel);
                ca.Children.Add(mplabel);
                ca.Children.Add(mp);
                ca.Children.Add(hp);

                border.Child = ca;

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

                Task.Factory.StartNew(() =>
                {
                    while (true)
                    {

                        Dispatcher.Invoke(new Action(() =>
                        {



                            if (Math.Abs(border.Margin.Left - MainWindow.b.Margin.Left) <= 250 && Math.Abs(border.Margin.Top - MainWindow.b.Margin.Top) <= 250 && isc)
                            {
                                storyboard.Pause();
                                double tmpLeft = 0;
                                double tmpTop = 0;
                                if (border.Margin.Left > MainWindow.b.Margin.Left) tmpLeft = MainWindow.b.Margin.Left + 90;
                                else tmpLeft = MainWindow.b.Margin.Left - 90;

                                thicknessAnimation = new ThicknessAnimation
                                {

                                    From = new Thickness(border.Margin.Left, border.Margin.Top, 0, 0),
                                    To = new Thickness(tmpLeft, MainWindow.b.Margin.Top, 0, 0),

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
                                if (isrunning)
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

                            if (Math.Abs(border.Margin.Left - MainWindow.weapon.Margin.Left) <= 120 && Math.Abs(border.Margin.Top - MainWindow.weapon.Margin.Top) <= 120 && isc)
                            {

                                border.Background = Brushes.Red;
                                Hp -= 7;
                                hp.Value = Hp;
                                hplabel.Content = Hp.ToString();
                                if (Hp <= 0)
                                {
                                    ((MainWindow)System.Windows.Application.Current.MainWindow).canvas_enemy.Children.Remove(border);
                                }


                            }

                        }));
                        Thread.Sleep(500);
                    }
                });

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
        public static Border weapon = new Border();
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {

            weapon.Width = 60;
            weapon.Height = 120;
            weapon.Background = Brushes.Lime;
            b.Width = 100;
            b.Height = 100;
            b.Background = Brushes.White;
            b.Margin = new Thickness(2510, 2265, 0, 0);

            Rectangle rectangle = new Rectangle();



            canvas_enemy.Children.Add(b);

            for (int i = 0; i < 10; i++)
            {
                EnemyClass en = new EnemyClass(100, 100);
                canvas_enemy.Children.Add(en.border);
            }


            BitmapImage img2 = new BitmapImage();

            img2.BeginInit();
            img2.StreamSource = new System.IO.MemoryStream(File.ReadAllBytes(dir.FullName + "\\Resources\\Weapon_34.png"));
            img2.EndInit();

            weapon.Background = new ImageBrush(img2);

            BitmapImage img = new BitmapImage();

            img.BeginInit();
            img.StreamSource = new System.IO.MemoryStream(File.ReadAllBytes(dir.FullName + "\\Resources\\image.png"));
            img.EndInit();

            Player.Background = new ImageBrush(img);


        }


        double x_cord = 0;
        double y_cord = 0;

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

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


        class Chelovek
        {
            public string Name { get; set; }
            public double Hp { get; set; }
            public double Mp { get; set; }
            public int Lvl { get; set; }
            public double Exp { get; set; }
            public Chelovek()
            {

            }
            public Chelovek(string name, double hp, double mp, int lvl, double exp)
            {
                Name = name;
                Hp = hp;
                Mp = mp;
                Lvl = lvl;
                Exp = exp;
            }
        }

        bool isattack = false;
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Q && !isattack)
            {
                canvas_enemy.Children.Remove(weapon);
                canvas_enemy.Children.Add(weapon);
                weapon.Margin = b.Margin;

                RotateTransform rotateTransform = new RotateTransform();

                rotateTransform.CenterX = weapon.Width / 2;
                rotateTransform.CenterY = weapon.Height / 2;

                weapon.RenderTransform = rotateTransform;

                DoubleAnimation rotationAnimation = new DoubleAnimation();
                rotationAnimation.From = 0;
                rotationAnimation.To = 360;
                rotationAnimation.Duration = TimeSpan.FromSeconds(0.3);


                Storyboard storyboard = new Storyboard();
                storyboard.Children.Add(rotationAnimation);

                Storyboard.SetTargetProperty(rotationAnimation, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));
                Storyboard.SetTarget(storyboard, weapon);
                storyboard.Completed += (sender, e) => { isattack = false; };
                storyboard.Begin();

                weapon.Visibility = Visibility.Visible;
                weapon.Margin = new Thickness(weapon.Margin.Left + 150, weapon.Margin.Top - 10, weapon.Margin.Right, weapon.Margin.Bottom);
                isattack = true;
            }

        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Q)
            {
                canvas_enemy.Children.Remove(weapon);
                weapon.Visibility = Visibility.Hidden;

                isattack = false;
            }
        }

        private void Window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
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
                thicknessAnimation2 = ThicknessAnimation2(b.Margin.Left, BT.Width / 2, 0);
            }
            else if (b.Margin.Left >= 3800)
            {
                thicknessAnimation = ThicknessAnimation(0, BT.Margin.Top, 0);
            }
            else if (b.Margin.Top >= 3800)
            {
                thicknessAnimation = ThicknessAnimation(BT.Margin.Left, 0, 0);
                thicknessAnimation2 = ThicknessAnimation2(b.Margin.Left, BT.Width / 2, 0);
            }
            else
            {
                thicknessAnimation = ThicknessAnimation(BT.Margin.Left + x, BT.Margin.Top + y, 0.6);
                thicknessAnimation2 = ThicknessAnimation2(b.Margin.Left - x, b.Margin.Top - y, 0.6);
            }


            Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath(FrameworkElement.MarginProperty));
            storyboard.Children.Add(thicknessAnimation);
            storyboard.Begin(BT);


            if (b.Margin.Top <= 100)//2512, 2266
            {
                thicknessAnimation2 = ThicknessAnimation2(b.Margin.Left, 2265, 0);
            }
            else if (b.Margin.Top >= 3800)
            {
                thicknessAnimation2 = ThicknessAnimation2(b.Margin.Left, 2265, 0);
            }
            else if (b.Margin.Left >= 3800)
            {
                thicknessAnimation2 = ThicknessAnimation2(2510, b.Margin.Top, 0);
            }
            else if (b.Margin.Left <= 100)
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

        private void DrawIsometricGrid()
        {
            double cellWidth = 256; // ширина ячейки сетки
            double cellHeight = 128; // высота ячейки сетки
            double rows = 20; // количество строк
            double cols = 20; // количество столбцов

            // Рисуем горизонтальные линии
            for (int row = 0; row <= rows; row++)
            {
                double startX = (cols - row) * cellWidth / 2;
                double startY = row * cellHeight / 2;

                double endX = startX + cols * cellWidth / 2;
                double endY = startY + cols * cellHeight / 2;

                Polyline polyline = new Polyline();
                polyline.Points.Add(new Point(startX, startY));
                polyline.Points.Add(new Point(endX, endY));
                polyline.Stroke = Brushes.Black;

                Map_canvas.Children.Add(polyline);
            }

            // Рисуем вертикальные линии
            for (int col = 0; col <= cols; col++)
            {
                double startX = col * cellWidth / 2;
                double startY = (col + rows) * cellHeight / 2;

                double endX = startX + rows * cellWidth / 2;
                double endY = startY - rows * cellHeight / 2;

                Polyline polyline = new Polyline();
                polyline.Points.Add(new Point(startX, startY));
                polyline.Points.Add(new Point(endX, endY));
                polyline.Stroke = Brushes.AliceBlue;

                Map_canvas.Children.Add(polyline);
            }
        }

        void TestMap()
        {
            int cellWidth = 256; // ширина ячейки сетки
            int cellHeight = 256; // высота ячейки сетки
            int rows = 20; // количество строк
            int cols = 20; // количество столбцов

            BitmapImage img2 = new BitmapImage();
            img2.BeginInit();
            img2.StreamSource = new System.IO.MemoryStream(File.ReadAllBytes(dir.FullName + "\\Resources\\ground1.png"));
            img2.EndInit();

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Image image = new Image();
                    image.Source = img2;

                    image.Width = cellWidth;
                    image.Height = cellHeight;
                    Canvas.SetLeft(image, col * cellWidth / 2 + row * cellWidth / 2);
                    Canvas.SetTop(image, row * cellHeight / 2 - col * cellHeight / 2);

                    
                    Map_canvas.Children.Add(image);
                }
            }

        }
    }
}
