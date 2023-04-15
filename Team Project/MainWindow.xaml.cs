
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
            InitializeComponent();
            dir = dir.Parent?.Parent?.Parent;
            mn = this;

            TestMap();
            Map_addObjects();
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
            public EnemyClass(double hptmp, double mptmp,string nametmp)
            {
                Name = nametmp;
                Name += "    lvl " + Lvl.ToString();
                Hp = hptmp;
                Mp = mptmp;
                border.Width = 100;
                border.Height = 100;
                border.Background = Brushes.Black;
                border.Margin = new Thickness(new Random().Next(3500), new Random().Next(3500), 0, 0);

                ProgressBar hp = new ProgressBar()
                {

                    BorderThickness = new Thickness(0),
                    Foreground = new SolidColorBrush(Color.FromArgb(200, 108, 167, 28)),
                    Maximum = Hp,
                    Value = Hp,
                    Width = 100,
                    Height = 20,
                    Margin = new Thickness(0, 0, 0, 0),
                    Style = (Style)FindResource("Enemy_Hp_ProgressBar"),
                };
                
                ProgressBar mp = new ProgressBar()
                {
                    BorderThickness = new Thickness(0),
                    Foreground = new SolidColorBrush(Color.FromArgb(150, 7, 85, 145)),
                    Maximum = Mp,
                    Value = Mp,
                    Width = 100,
                    Height = 20,
                    Margin = new Thickness(0, 20, 0, 0),
                    Style = (Style)FindResource("Enemy_Hp_ProgressBar"),
                };
                Label hplabel = new Label()
                {
                    Content = Hp,
                    FontWeight = FontWeights.Bold,
                    Foreground = Brushes.LightGreen,
                    Margin = new Thickness(100, 0, 0, 0),
                };
                Label mplabel = new Label()
                {
                    Content = Mp,
                    FontWeight = FontWeights.Bold,
                    Foreground = Brushes.DarkBlue,
                    Margin = new Thickness(100, 20, 0, 0),
                };
                Label namelabel = new Label()
                {
                    Content = Name,
                    FontWeight = FontWeights.Bold,
                    Foreground = Brushes.Gold,
                    Margin = new Thickness(0, -30, 0, 0),
                };
                Canvas ca = new Canvas();
                ca.Margin = new Thickness(0, -50, 0, 0);

                ca.Children.Add(hplabel);
                ca.Children.Add(mplabel);
                ca.Children.Add(namelabel);
                ca.Children.Add(mp);
                ca.Children.Add(hp);

                border.Child = ca;
                BitmapImage img = new BitmapImage(new Uri(dir.FullName + "\\Resources\\cerberus.png"));


                //кусок картинки
                Int32Rect cropRect = new Int32Rect(0, 0, 120, 120);
                CroppedBitmap croppedBitmap = new CroppedBitmap(img, cropRect);

                border.Background = new ImageBrush(croppedBitmap);
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
                    bool ift = true;
                    while (ift)
                    {

                        Dispatcher.Invoke( new Action(async () =>
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

                                //border.Background = Brushes.Yellow;

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
                                    //border.Background = Brushes.Black;
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
                                //border.Background = Brushes.Black;
                                isc = false;
                            }

                            if (Math.Abs(border.Margin.Left - MainWindow.weapon.Margin.Left) <= 120 && Math.Abs(border.Margin.Top - MainWindow.weapon.Margin.Top) <= 120 && isc)
                            {

                                //border.Background = Brushes.Red;
                                Hp -= 40;
                                hp.Value = Hp;
                                hplabel.Content = Hp.ToString();
                                if (Hp <= 0 && ift)
                                {
                                    BitmapImage img = new BitmapImage(new Uri(dir.FullName + "\\Resources\\cerberus.png"));


                                    //кусок картинки
                                    Int32Rect cropRect = new Int32Rect(1020, 1010, 100, 50);
                                    CroppedBitmap croppedBitmap = new CroppedBitmap(img, cropRect);
                                    
                                    border.Background = new ImageBrush(croppedBitmap);
                                    storyboard.Stop();
                                    storyboard2.Stop();
                                    storyboard3.Stop();
                                    storyboard4.Stop();
                                    storyboard5.Stop();

                                    await Task.Delay(1000);
                                    ((MainWindow)System.Windows.Application.Current.MainWindow).canvas_enemy.Children.Remove(border);
                                    
                                    EnemyClass newen = new EnemyClass(100, 750, "SUPER Cerberus");
                                    ((MainWindow)System.Windows.Application.Current.MainWindow).canvas_enemy.Children.Add(newen.border);
                                    ift = false;
                                }


                            }

                            if (Math.Abs(border.Margin.Left - MainWindow.b.Margin.Left) <= 120 && Math.Abs(border.Margin.Top - MainWindow.b.Margin.Top) <= 10 && isc) // Mob attack
                            {
                                //MessageBox.Show("Hit");
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
            timer.Interval = TimeSpan.FromSeconds(0.08);
            timer.Tick += Timer_Event_on_Tick;
           
            
            this.WindowState = WindowState.Maximized;
            weapon.Width = 60;
            weapon.Height = 120;
            weapon.Background = Brushes.Lime;
            b.Width = 100;
            b.Height = 100;
            b.Background = Brushes.Transparent;
            b.Margin = new Thickness(2510, 2265, 0, 0);

            Rectangle rectangle = new Rectangle();
            //NIghtBorder.Visibility = Visibility.Hidden;
            //NIghtBorder2.Visibility = Visibility.Hidden;
            //NIghtBorder.Background = new SolidColorBrush(Color.FromArgb(100,0,0,0));
            //NIghtBorder2.BorderBrush = new SolidColorBrush(Color.FromArgb(200, 0, 0, 0));
            canvas_enemy.Children.Add(b);

            for (int i = 0; i < 10; i++)
            {
                EnemyClass en = new EnemyClass(100, 100,$"Cerberus");
                canvas_enemy.Children.Add(en.border);
            }


            BitmapImage img2 = new BitmapImage();

            img2.BeginInit();
            img2.StreamSource = new System.IO.MemoryStream(File.ReadAllBytes(dir.FullName + "\\Resources\\sword.png"));
            img2.EndInit();

            weapon.Background = new ImageBrush(img2);

            BitmapImage img = new BitmapImage(new Uri(dir.FullName + "\\Resources\\player_topchik.png"));

            //кусок картинки
            Int32Rect cropRect = new Int32Rect(0, 0, 120, 180);
            CroppedBitmap croppedBitmap = new CroppedBitmap(img, cropRect);
            //
            Player.Background = new ImageBrush(croppedBitmap);
        }


        double x_cord = 0;
        double y_cord = 0;

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
          
            canvas_enemy.Children.Remove(weapon);
            canvas_enemy.Children.Add(weapon);
            weapon.Margin = b.Margin;

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

            weapon.Visibility = Visibility.Visible;

            p = Mouse.GetPosition(this);
            double x = (Player.Margin.Left + 50) - p.X;
            double y = (Player.Margin.Top + 50) - p.Y;

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
                    else if(TImerY < -100 && TimerX <= -100) //RB
                    {
                        uy = 1350;
                    }
                    else if (TImerY < -100 && TimerX >= 100) //LB
                    {
                        uy = 220;
                    }
                    else if((TimerX > -100 && TimerX < 100) && TImerY > 0)//U
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

                    Player.Background = new ImageBrush(croppedBitmap);

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

                From = new Thickness(b.Margin.Left, b.Margin.Top, b.Margin.Right, b.Margin.Bottom),
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
               
            }

            if (e.Key == Key.Escape)
            {
                if(Menu_border.Visibility == Visibility.Visible) Menu_border.Visibility = Visibility.Hidden;
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
            double x = (Player.Margin.Left + 50) - p.X;
            double y = (Player.Margin.Top + 50) - p.Y;
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
            if (b.Margin.Left <= 100)
            {
                thicknessAnimation = ThicknessAnimation(0, BT.Margin.Top, 0);
            }
            else if (b.Margin.Top <= 100)
            {
                thicknessAnimation = ThicknessAnimation(BT.Margin.Left, 0, 0);
            }
            else if (b.Margin.Left >= BT.Width - 200)
            {
                thicknessAnimation = ThicknessAnimation(0, BT.Margin.Top, 0);
            }
            else if (b.Margin.Top >= BT.Width - 200)
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
            else if (b.Margin.Top >= BT.Width - 200)
            {
                thicknessAnimation2 = ThicknessAnimation2(b.Margin.Left, 2265, 0);
            }
            else if (b.Margin.Left >= BT.Width - 200)
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
            timer.Start();
            Title = Math.Round(b.Margin.Left).ToString() + " " + Math.Round(b.Margin.Top).ToString();
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

                                Map_canvas.Children.Add(image);
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

                                Map_canvas.Children.Add(image);
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

                                Map_canvas.Children.Add(image);
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
                if(p.ProcessName.Contains("Team Project"))
                    p.Kill();
            }
        }

    }
}
