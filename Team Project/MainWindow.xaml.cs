
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

            Mini_map();
        }
        public DirectoryInfo? dir = new DirectoryInfo(Directory.GetCurrentDirectory());

        public static string dirname;

        public static bool isdmgallowed = false;
        public static Player player = new Player("alex", 175, 100, 1, 0, new Weapon("Novice Weapon", 12, "sword.png"));
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        { 
            timer.Interval = TimeSpan.FromSeconds(0.04);
            timer.Tick += Timer_Event_on_Tick;


            this.WindowState = WindowState.Maximized;

            //NIghtBorder.Visibility = Visibility.Hidden;
            //NIghtBorder2.Visibility = Visibility.Hidden;
            //NIghtBorder.Background = new SolidColorBrush(Color.FromArgb(100,0,0,0));
            //NIghtBorder2.BorderBrush = new SolidColorBrush(Color.FromArgb(200, 0, 0, 0));
            canvas_enemy.Children.Add(player.Player_Back_Border);
            Player_Canvas.Children.Add(player.PLayer_Front_Border);
            for (int i = 0; i < 10; i++)
            {
                EnemyClass en = new EnemyClass(100, 100, $"Cerberus", 10, 15, 1);
                canvas_enemy.Children.Add(en.border);
            }

            PlayerHp.DataContext = player;
            PlayerMp.DataContext = player;
            XpBar.DataContext = player;

            await Task.Factory.StartNew(() => {
                while(true)
                {
                    
                    this.Dispatcher.Invoke(() => {
                        if (player.Player_Back_Border.Margin.Left <= T.Margin.Left + T.Width && player.Player_Back_Border.Margin.Left + player.Player_Back_Border.Width >= T.Margin.Left
                        && player.Player_Back_Border.Margin.Top + player.Player_Back_Border.Height
                        >= T.Margin.Top && player.Player_Back_Border.Margin.Top <= T.Margin.Top + T.Height)
                        {

                            if (player.Player_Back_Border.Margin.Left > T.Margin.Left && canmoveleft && player.Player_Back_Border.Margin.Top > T.Margin.Top && player.Player_Back_Border.Margin.Top + player.Player_Back_Border.Height < T.Margin.Top + T.Height
                            ) //<<------
                            {

                                    var dist = Math.Abs(T.Margin.Left + T.Width - player.Player_Back_Border.Margin.Left);
                                    var t = ThicknessAnimation(BT.Margin.Left - dist, BT.Margin.Top, 0);
                                    Storyboard.SetTargetProperty(t, new PropertyPath(FrameworkElement.MarginProperty));
                                    storyboard.Children.Add(t);
                                    storyboard.Begin(BT);

                                    var t2 = ThicknessAnimation2(T.Margin.Left + T.Width, player.Player_Back_Border.Margin.Top, 0);
                                    var r = new Storyboard();
                                    Storyboard.SetTargetProperty(t2, new PropertyPath(FrameworkElement.MarginProperty));
                                    r.Children.Add(t2);
                                    r.Begin(player.Player_Back_Border);
                                    canmoveleft = false;
                            }
                            else if (player.Player_Back_Border.Margin.Left < T.Margin.Left && canmoveright && player.Player_Back_Border.Margin.Top >= T.Margin.Top && player.Player_Back_Border.Margin.Top + player.Player_Back_Border.Height < T.Margin.Top + T.Height) //---->>
                            {

                                    var dist = Math.Abs(T.Margin.Left - player.Player_Back_Border.Margin.Left - player.Player_Back_Border.Width);
                                    var t = ThicknessAnimation(BT.Margin.Left + dist, BT.Margin.Top, 0);
                                    Storyboard.SetTargetProperty(t, new PropertyPath(FrameworkElement.MarginProperty));
                                    storyboard.Children.Add(t);
                                    storyboard.Begin(BT);

                                    var t2 = ThicknessAnimation2(T.Margin.Left - player.Player_Back_Border.Width, player.Player_Back_Border.Margin.Top, 0);
                                    var r = new Storyboard();
                                    Storyboard.SetTargetProperty(t2, new PropertyPath(FrameworkElement.MarginProperty));
                                    r.Children.Add(t2);
                                    r.Begin(player.Player_Back_Border);
                                    canmoveright = false;
                                    //lab.Content =(" left: " + canmoveleft + " right: " + canmoveright, "Right");
                            
                               
                            }
                            else if (player.Player_Back_Border.Margin.Top > T.Margin.Top && canmoveup && player.Player_Back_Border.Margin.Left > T.Margin.Left && player.Player_Back_Border.Margin.Left + player.Player_Back_Border.Width < T.Margin.Left + T.Width
                            ) //up
                            {
                                var dist = Math.Abs(player.Player_Back_Border.Margin.Top - T.Margin.Top - T.Height);
                                var t = ThicknessAnimation(BT.Margin.Left, BT.Margin.Top - dist, 0);
                                Storyboard.SetTargetProperty(t, new PropertyPath(FrameworkElement.MarginProperty));
                                storyboard.Children.Add(t);
                                storyboard.Begin(BT);

                                var t2 = ThicknessAnimation2(player.Player_Back_Border.Margin.Left, T.Margin.Top + T.Height, 0);
                                var r = new Storyboard();
                                Storyboard.SetTargetProperty(t2, new PropertyPath(FrameworkElement.MarginProperty));
                                r.Children.Add(t2);
                                r.Begin(player.Player_Back_Border);
                                canmoveup = false;
                                //MessageBox.Show(" down: " + canmovedown + " up: " + canmoveup, "Up");
                            }
                            else if (player.Player_Back_Border.Margin.Top < T.Margin.Top && canmovedown && player.Player_Back_Border.Margin.Left > T.Margin.Left && player.Player_Back_Border.Margin.Left + player.Player_Back_Border.Width < T.Margin.Left + T.Width
                            ) //down
                            {
                                var dist = Math.Abs(player.Player_Back_Border.Margin.Top + player.Player_Back_Border.Height - T.Margin.Top);
                                var t = ThicknessAnimation(BT.Margin.Left, BT.Margin.Top + dist, 0);
                                Storyboard.SetTargetProperty(t, new PropertyPath(FrameworkElement.MarginProperty));
                                storyboard.Children.Add(t);
                                storyboard.Begin(BT);

                                var t2 = ThicknessAnimation2(player.Player_Back_Border.Margin.Left, T.Margin.Top - player.Player_Back_Border.Height, 0);
                                var r = new Storyboard();
                                Storyboard.SetTargetProperty(t2, new PropertyPath(FrameworkElement.MarginProperty));
                                r.Children.Add(t2);
                                r.Begin(player.Player_Back_Border);
                                canmovedown = false;
                                //MessageBox.Show(" down: " + canmovedown + " up: " + canmoveup, "Down");
                            }

                        }
                        else
                        {
                            canmoveleft = true;
                            canmoveright = true;
                            canmovedown = true;
                            canmoveup = true;
                            //MessageBox.Show("right: " + canmoveright + " left: " + canmoveleft, "else");
                        }
                        //lab.Content = T.Margin + " " + player.Player_Back_Border.Margin + " \n" + canmoveleft;
                        //lab.Content = $"left: {canmoveleft} right: {canmoveright}";
                    });

                   
                    Thread.Sleep(50);
                }
            });

           
        }
        bool canmoveleft = true;
        bool canmoveright = true;
        bool canmoveup = true;
        bool canmovedown = true;
        //
        double x_cord = 0;
        double y_cord = 0;
        //
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!isattack)
            {
                isdmgallowed = true;
                player.weapon.weapon_border.IsEnabled = true;
                canvas_enemy.Children.Add(player.weapon.weapon_border);
                player.weapon.weapon_border.Margin = player.Player_Back_Border.Margin;
                
                RotateTransform rotateTransform = new RotateTransform();

                rotateTransform.CenterX = player.weapon.weapon_border.Width / 2;
                rotateTransform.CenterY = player.weapon.weapon_border.Height;

                player.weapon.weapon_border.RenderTransform = rotateTransform;

                DoubleAnimation rotationAnimation = new DoubleAnimation();
                rotationAnimation.From = 0;
                rotationAnimation.To = 120;
                rotationAnimation.Duration = TimeSpan.FromSeconds(0.15);


                Storyboard storyboard = new Storyboard();
                storyboard.Children.Add(rotationAnimation);

                Storyboard.SetTargetProperty(rotationAnimation, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));
                Storyboard.SetTarget(storyboard, player.weapon.weapon_border);
                storyboard.Completed += delegate
                {
                    timer.Stop();
                    isattack = false;
                    player.weapon.weapon_border.IsEnabled = false;
                    canvas_enemy.Children.Remove(player.weapon.weapon_border);
                };
                storyboard.Begin();
                timer.Start();
                isattack = true;

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

                player.weapon.weapon_border.Margin = new Thickness(player.weapon.weapon_border.Margin.Left - x, player.weapon.weapon_border.Margin.Top - y - 40, 0, 0);




                //Left top x+ y+
                //Right top x- y+
                //Right bottom x- y-
                //Left bottom x+ y-

            }

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
            /*canvas_enemy.Children.Remove(weapon);
            weapon.Visibility = Visibility.Hidden;

            isattack = false;*/
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


            double sumXY = Math.Abs(x) + Math.Abs(y);
            double maxSpeed; 
            if(sumXY > 400) maxSpeed = sumXY / 333;
            else maxSpeed = sumXY / 222;

            lab.Content = $"x: {x} y: {y}";
            timer.Stop();
            storyboard.Stop();

            ThicknessAnimation thicknessAnimation;
            ThicknessAnimation thicknessAnimation2;
            if (player.Player_Back_Border.Margin.Left <= 100)
            {
                thicknessAnimation = ThicknessAnimation(0, BT.Margin.Top, 0);
                thicknessAnimation2 = ThicknessAnimation2(2510, player.Player_Back_Border.Margin.Top, 0);
            }
            else if (player.Player_Back_Border.Margin.Top <= 100)
            {
                thicknessAnimation = ThicknessAnimation(BT.Margin.Left, 0, 0);
                thicknessAnimation2 = ThicknessAnimation2(player.Player_Back_Border.Margin.Left, 2265, 0);
            }
            else if (player.Player_Back_Border.Margin.Left >= BT.Width - 200)
            {
                thicknessAnimation = ThicknessAnimation(0, BT.Margin.Top, 0);
                thicknessAnimation2 = ThicknessAnimation2(2510, player.Player_Back_Border.Margin.Top, 0);
            }
            else if (player.Player_Back_Border.Margin.Top >= BT.Width - 200)
            {
                thicknessAnimation = ThicknessAnimation(BT.Margin.Left, 0, 0);
                thicknessAnimation2 = ThicknessAnimation2(player.Player_Back_Border.Margin.Left, 2265, 0);
            }
            else
            {
                if (!canmoveleft)
                {
                    thicknessAnimation = ThicknessAnimation(BT.Margin.Left, BT.Margin.Top + y, maxSpeed);
                    thicknessAnimation2 = ThicknessAnimation2(player.Player_Back_Border.Margin.Left, player.Player_Back_Border.Margin.Top - y, maxSpeed);
                    if (canmoveright && x < 0)  //движ вправо
                    {
                        canmoveleft = true;
                        thicknessAnimation = ThicknessAnimation(BT.Margin.Left + x, BT.Margin.Top + y, maxSpeed);
                        thicknessAnimation2 = ThicknessAnimation2(player.Player_Back_Border.Margin.Left - x, player.Player_Back_Border.Margin.Top - y, maxSpeed);
                    }
                        
                }
                else if (!canmoveright)
                {
                    thicknessAnimation = ThicknessAnimation(BT.Margin.Left, BT.Margin.Top + y, maxSpeed);
                    thicknessAnimation2 = ThicknessAnimation2(player.Player_Back_Border.Margin.Left, player.Player_Back_Border.Margin.Top - y, maxSpeed);
                    if (canmoveleft && x > 0) //движ влево
                    {
                        canmoveright = true;
                        thicknessAnimation = ThicknessAnimation(BT.Margin.Left + x, BT.Margin.Top + y, maxSpeed);
                        thicknessAnimation2 = ThicknessAnimation2(player.Player_Back_Border.Margin.Left - x, player.Player_Back_Border.Margin.Top - y, maxSpeed);
                    }
                }
                else if (!canmoveup)
                {
                    thicknessAnimation = ThicknessAnimation(BT.Margin.Left + x, BT.Margin.Top, maxSpeed);
                    thicknessAnimation2 = ThicknessAnimation2(player.Player_Back_Border.Margin.Left - x, player.Player_Back_Border.Margin.Top, maxSpeed);
                    if (canmovedown && y < 0)
                    {
                        canmoveup = true;
                        thicknessAnimation = ThicknessAnimation(BT.Margin.Left + x, BT.Margin.Top + y, maxSpeed);
                        thicknessAnimation2 = ThicknessAnimation2(player.Player_Back_Border.Margin.Left - x, player.Player_Back_Border.Margin.Top - y, maxSpeed);
                    }
                }
                else if (!canmovedown)
                {
                    thicknessAnimation = ThicknessAnimation(BT.Margin.Left + x, BT.Margin.Top, maxSpeed);
                    thicknessAnimation2 = ThicknessAnimation2(player.Player_Back_Border.Margin.Left - x, player.Player_Back_Border.Margin.Top, maxSpeed);
                    if (canmoveup && y > 0)
                    {
                        canmovedown = true;
                        thicknessAnimation = ThicknessAnimation(BT.Margin.Left + x, BT.Margin.Top + y, maxSpeed);
                        thicknessAnimation2 = ThicknessAnimation2(player.Player_Back_Border.Margin.Left - x, player.Player_Back_Border.Margin.Top - y, maxSpeed);
                    }
                }
                else //можно двигаться
                {
                    thicknessAnimation = ThicknessAnimation(BT.Margin.Left + x, BT.Margin.Top + y, maxSpeed);
                    thicknessAnimation2 = ThicknessAnimation2(player.Player_Back_Border.Margin.Left - x, player.Player_Back_Border.Margin.Top - y, maxSpeed);
                }
               
            }

            Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath(FrameworkElement.MarginProperty));
            storyboard.Children.Add(thicknessAnimation);
            storyboard.Begin(BT);
            storyboard.Completed += delegate { timer.Stop(); };
   
            var r = new Storyboard();
            Storyboard.SetTargetProperty(thicknessAnimation2, new PropertyPath(FrameworkElement.MarginProperty));
            r.Children.Add(thicknessAnimation2);
            r.Begin(player.Player_Back_Border);
            timer.Start();

            //Title = Math.Round(player.Player_Back_Border.Margin.Left).ToString() + " " + Math.Round(player.Player_Back_Border.Margin.Top).ToString();
        }

        BitmapImage img0 = new BitmapImage();
        BitmapImage img1 = new BitmapImage();
        int cellWidth = 256; // ширина ячейки сетки
        int cellHeight = 256; // высота ячейки сетки
        const int rows = 20; // количество строк
        const int cols = 20; // количество столбцов
        
        
        async void TestMap()
        {
            img0.BeginInit();
            img0.StreamSource = new System.IO.MemoryStream(File.ReadAllBytes(dir.FullName + "\\Resources\\ground2.png"));
            img0.EndInit();
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
        void Mini_map()
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Image image2 = new Image();
                    image2.Source = img0;
                    image2.Width = cellWidth / 15;
                    image2.Height = cellHeight / 15;
                    Canvas.SetLeft(image2, col * cellWidth / 20);    //bt.width для отступа влево т.к. привязка к другим координатам, а они слишком уехали вправо
                    Canvas.SetTop(image2, row * cellHeight / 20);
                    MiniMap_canvas.Children.Add(image2);
                }
            }
        }
        void Map_addObjects()
        {
            ObservableCollection<BitmapImage> imgs = new ObservableCollection<BitmapImage>();
            string[] files = Directory.GetFiles(dir.FullName + "\\Resources\\", "*.png");
            foreach (string file in files)
            {
                BitmapImage img = new BitmapImage(new Uri(file));
                imgs.Add(img);
            }


            int[,] map = new int[rows, cols]
            {
                {2,2,0,0,0,0,0,2,2,0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,1,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0 },
                {0,0,0,1,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,1,2,0,1,0,0,0,0,0,0,0,0,0,0,0,0,1 },
                {0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2 },
                {0,0,0,0,0,1,0,0,0,0,0,0,0,1,1,1,0,0,0,1 },
                {0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,1 },
                {0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,2 },
                {0,0,0,0,0,0,2,0,0,0,0,2,2,0,0,0,0,0,0,2 },
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2 },
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2 },
                {0,0,0,0,0,0,11,0,0,0,0,0,0,0,0,0,0,1,0,2 },
                {0,0,0,0,0,0,19,19,0,3,0,0,0,0,0,0,1,0,0,0 },
                {2,0,0,0,0,0,19,0,0,0,0,0,1,1,1,1,0,0,0,0 },
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
                    if (map[col, row] >= 8)
                    {
                        Image image = new Image();
                        image.Source = imgs[map[col, row]];
                        image.Width = cellWidth;
                        image.Height = cellHeight;
                        Canvas.SetLeft(image, col * cellWidth - BT.Width / 2);    //bt.width для отступа влево т.к. привязка к другим координатам, а они слишком уехали вправо
                        Canvas.SetTop(image, row * cellHeight - BT.Height / 2);

                        MapItems_canvas.Children.Add(image);
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
