
using Elasticsearch.Net;
using Nest;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Metrics;
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
using WpfAnimatedGif;

namespace Team_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Point p = new Point();
        Storyboard storyboard = new Storyboard();
        public static MainWindow mn = new MainWindow();
        Map map;
        public MainWindow()
        {

            this.DataContext = this;
            //PlayerHp.DataContext = player;
          
            InitializeComponent();
            dir = dir.Parent?.Parent?.Parent;
            mn = this;
            dirname = dir.FullName;    
        }
        public DirectoryInfo? dir = new DirectoryInfo(Directory.GetCurrentDirectory());

        public static string dirname;
        DispatcherTimer camp_fire_timer = new DispatcherTimer();
        public static bool isdmgallowed = false;
        public static Player player = new Player("Azure", 175, 100, 1, 0, new Weapon("Novice Weapon", 12, "sword.png"));
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        { 
            timer.Interval = TimeSpan.FromSeconds(0.04);
            timer.Tick += Timer_Event_on_Tick;
            camp_fire_timer.Interval = TimeSpan.FromSeconds(1);
            camp_fire_timer.Tick += delegate {

                if(Math.Abs(SpawnCampFire.Margin.Left - MainWindow.player.Player_Back_Border.Margin.Left) <= 200 && Math.Abs(SpawnCampFire.Margin.Top - MainWindow.player.Player_Back_Border.Margin.Top) <= 200)
                {
                    player.Hp += 1;
                    player.Mp += 1;
                    if(player.Hp > player.MaxHp)
                        player.Hp = player.MaxHp;
                    if (player.Mp > player.MaxMp)
                        player.Mp = player.MaxMp;
                    PlayerHp.Value = player.Hp;
                    PlayerMp.Value = player.Mp;
                }
            };

            // map
            map = new Map(BT.Width / 2, BT.Height / 2);
            Map_canvas.Children.Add(map.Map_new());
            MapItems_canvas.Children.Add(map.Map_Trees());
            foreach (var house in map.Map_Houses())
            {
                canvas_enemy.Children.Add(house);
            }
            //

            Minimap();
            camp_fire_timer.Start();

            // Collision for map objects
            foreach (var obj in canvas_enemy.Children)
            {
                if (obj is Border)
                {
                    Thread t = new Thread(() =>
                    {
                        Collision(obj as Border);
                    });
                    t.SetApartmentState(ApartmentState.STA);
                    t.Start();
                }
            }

                if(Math.Abs(SpawnCampFire.Margin.Left - MainWindow.player.Player_Back_Border.Margin.Left) <= 200 && Math.Abs(SpawnCampFire.Margin.Top - MainWindow.player.Player_Back_Border.Margin.Top) <= 200)
                {
                    player.Hp += 1;
                    player.Mp += 1;
                    if(player.Hp > player.MaxHp)
                        player.Hp = player.MaxHp;
                    if (player.Mp > player.MaxMp)
                        player.Mp = player.MaxMp;
                    PlayerHp.Value = player.Hp;
                    PlayerMp.Value = player.Mp;
                }
            };

            
            camp_fire_timer.Start();
            this.WindowState = WindowState.Maximized;
            //NIghtBorder.Visibility = Visibility.Hidden;
            //NIghtBorder2.Visibility = Visibility.Hidden;
            //NIghtBorder.Background = new SolidColorBrush(Color.FromArgb(100,0,0,0));
            //NIghtBorder2.BorderBrush = new SolidColorBrush(Color.FromArgb(200, 0, 0, 0));
            canvas_enemy.Children.Add(player.Player_Back_Border);
            Player_Canvas.Children.Add(player.PLayer_Front_Border);
            for (int i = 0; i < 10; i++)
            {
                var lvl = new Random().Next(1,10);
                EnemyClass en = new EnemyClass(100, 100, $"Cerberus", lvl + 10, lvl*2 + 15, lvl,
                new Random().Next(500,Convert.ToInt32(BT.Width-500)), new Random().Next(500,Convert.ToInt32(BT.Height - 500)));

                canvas_enemy.Children.Add(en.border);
            }
            
            PlayerHp.DataContext = player;
            PlayerMp.DataContext = player;
            XpBar.DataContext = player;


            await Task.Factory.StartNew(() =>
            {
                try
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.UriSource = new Uri(dir.FullName + "\\Resources\\R.gif");
                        image.EndInit();
                        ImageBehavior.SetAnimatedSource(SpawnCampFire, image);
                    });
                  
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
              

            });

            {
                //await Task.Factory.StartNew(() =>
                //{
                //    while (true)
                //    {

                //        this.Dispatcher.Invoke(() =>
                //        {
                //            if (player.Player_Back_Border.Margin.Left <= FUCKBORDERBOTTOM.Margin.Left + FUCKBORDERBOTTOM.Width && player.Player_Back_Border.Margin.Left + player.Player_Back_Border.Width >= FUCKBORDERBOTTOM.Margin.Left
                //            && player.Player_Back_Border.Margin.Top + player.Player_Back_Border.Height
                //            >= FUCKBORDERBOTTOM.Margin.Top && player.Player_Back_Border.Margin.Top <= FUCKBORDERBOTTOM.Margin.Top + FUCKBORDERBOTTOM.Height)
                //            {

                //                if (player.Player_Back_Border.Margin.Left > FUCKBORDERBOTTOM.Margin.Left && canmoveleft && player.Player_Back_Border.Margin.Top > FUCKBORDERBOTTOM.Margin.Top && player.Player_Back_Border.Margin.Top + player.Player_Back_Border.Height < FUCKBORDERBOTTOM.Margin.Top + FUCKBORDERRIGHT.Height
                //                ) //<<------
                //                {

                //                    var dist = Math.Abs(FUCKBORDERRIGHT.Margin.Left + FUCKBORDERRIGHT.Width - player.Player_Back_Border.Margin.Left);
                //                    var t = ThicknessAnimation(BT.Margin.Left - dist, BT.Margin.Top, 0);
                //                    Storyboard.SetTargetProperty(t, new PropertyPath(FrameworkElement.MarginProperty));
                //                    storyboard.Children.Add(t);
                //                    storyboard.Begin(BT);

                //                    var t2 = ThicknessAnimation2(FUCKBORDERRIGHT.Margin.Left + FUCKBORDERRIGHT.Width, player.Player_Back_Border.Margin.Top, 0);
                //                    var r = new Storyboard();
                //                    Storyboard.SetTargetProperty(t2, new PropertyPath(FrameworkElement.MarginProperty));
                //                    r.Children.Add(t2);
                //                    r.Begin(player.Player_Back_Border);
                //                    canmoveleft = false;
                //                }
                //                else if (player.Player_Back_Border.Margin.Left < FUCKBORDERRIGHT.Margin.Left && canmoveright && player.Player_Back_Border.Margin.Top >= FUCKBORDERRIGHT.Margin.Top && player.Player_Back_Border.Margin.Top + player.Player_Back_Border.Height < FUCKBORDERRIGHT.Margin.Top + FUCKBORDERRIGHT.Height) //---->>
                //                {

                //                    var dist = Math.Abs(FUCKBORDERRIGHT.Margin.Left - player.Player_Back_Border.Margin.Left - player.Player_Back_Border.Width);
                //                    var t = ThicknessAnimation(BT.Margin.Left + dist, BT.Margin.Top, 0);
                //                    Storyboard.SetTargetProperty(t, new PropertyPath(FrameworkElement.MarginProperty));
                //                    storyboard.Children.Add(t);
                //                    storyboard.Begin(BT);

                //                    var t2 = ThicknessAnimation2(FUCKBORDERRIGHT.Margin.Left - player.Player_Back_Border.Width, player.Player_Back_Border.Margin.Top, 0);
                //                    var r = new Storyboard();
                //                    Storyboard.SetTargetProperty(t2, new PropertyPath(FrameworkElement.MarginProperty));
                //                    r.Children.Add(t2);
                //                    r.Begin(player.Player_Back_Border);
                //                    canmoveright = false;
                //                    //lab.Content =(" left: " + canmoveleft + " right: " + canmoveright, "Right");


                //                }
                //                else if (player.Player_Back_Border.Margin.Top > FUCKBORDERRIGHT.Margin.Top && canmoveup && player.Player_Back_Border.Margin.Left > FUCKBORDERRIGHT.Margin.Left && player.Player_Back_Border.Margin.Left + player.Player_Back_Border.Width < FUCKBORDERRIGHT.Margin.Left + FUCKBORDERRIGHT.Width
                //                ) //up
                //                {
                //                    var dist = Math.Abs(player.Player_Back_Border.Margin.Top - FUCKBORDERRIGHT.Margin.Top - FUCKBORDERRIGHT.Height);
                //                    var t = ThicknessAnimation(BT.Margin.Left, BT.Margin.Top - dist, 0);
                //                    Storyboard.SetTargetProperty(t, new PropertyPath(FrameworkElement.MarginProperty));
                //                    storyboard.Children.Add(t);
                //                    storyboard.Begin(BT);

                //                    var t2 = ThicknessAnimation2(player.Player_Back_Border.Margin.Left, FUCKBORDERRIGHT.Margin.Top + FUCKBORDERRIGHT.Height, 0);
                //                    var r = new Storyboard();
                //                    Storyboard.SetTargetProperty(t2, new PropertyPath(FrameworkElement.MarginProperty));
                //                    r.Children.Add(t2);
                //                    r.Begin(player.Player_Back_Border);
                //                    canmoveup = false;
                //                    //MessageBox.Show(" down: " + canmovedown + " up: " + canmoveup, "Up");
                //                }
                //                else if (player.Player_Back_Border.Margin.Top < FUCKBORDERRIGHT.Margin.Top && canmovedown && player.Player_Back_Border.Margin.Left > FUCKBORDERRIGHT.Margin.Left && player.Player_Back_Border.Margin.Left + player.Player_Back_Border.Width < FUCKBORDERRIGHT.Margin.Left + FUCKBORDERRIGHT.Width
                //                ) //down
                //                {
                //                    var dist = Math.Abs(player.Player_Back_Border.Margin.Top + player.Player_Back_Border.Height - FUCKBORDERRIGHT.Margin.Top);
                //                    var t = ThicknessAnimation(BT.Margin.Left, BT.Margin.Top + dist, 0);
                //                    Storyboard.SetTargetProperty(t, new PropertyPath(FrameworkElement.MarginProperty));
                //                    storyboard.Children.Add(t);
                //                    storyboard.Begin(BT);

                //                    var t2 = ThicknessAnimation2(player.Player_Back_Border.Margin.Left, FUCKBORDERRIGHT.Margin.Top - player.Player_Back_Border.Height, 0);
                //                    var r = new Storyboard();
                //                    Storyboard.SetTargetProperty(t2, new PropertyPath(FrameworkElement.MarginProperty));
                //                    r.Children.Add(t2);
                //                    r.Begin(player.Player_Back_Border);
                //                    canmovedown = false;
                //                    //MessageBox.Show(" down: " + canmovedown + " up: " + canmoveup, "Down");
                //                }

                //            }
                //            else
                //            {
                //                canmoveleft = true;
                //                canmoveright = true;
                //                canmovedown = true;
                //                canmoveup = true;
                //                //MessageBox.Show("right: " + canmoveright + " left: " + canmoveleft, "else");
                //            }
                //            //lab.Content = T.Margin + " " + player.Player_Back_Border.Margin + " \n" + canmoveleft;
                //            //lab.Content = $"left: {canmoveleft} right: {canmoveright}";
                //        });


                //        Thread.Sleep(50);
                //    }
                //});

                //await Task.Factory.StartNew(() =>
                //{

                //});


                //while (true)
                //{

                //    this.Dispatcher.Invoke(() =>
                //    {
                //        if (player.Player_Back_Border.Margin.Left <= FUCKBORDERBOTTOM.Margin.Left + FUCKBORDERBOTTOM.Width && player.Player_Back_Border.Margin.Left + player.Player_Back_Border.Width >= FUCKBORDERBOTTOM.Margin.Left
                //        && player.Player_Back_Border.Margin.Top + player.Player_Back_Border.Height
                //        >= FUCKBORDERBOTTOM.Margin.Top && player.Player_Back_Border.Margin.Top <= FUCKBORDERBOTTOM.Margin.Top + FUCKBORDERBOTTOM.Height)
                //        {

                //            if (player.Player_Back_Border.Margin.Left > FUCKBORDERBOTTOM.Margin.Left && canmoveleft && player.Player_Back_Border.Margin.Top > FUCKBORDERBOTTOM.Margin.Top && player.Player_Back_Border.Margin.Top + player.Player_Back_Border.Height < FUCKBORDERBOTTOM.Margin.Top + FUCKBORDERBOTTOM.Height
                //            ) //<<------
                //            {

                //                var dist = Math.Abs(FUCKBORDERBOTTOM.Margin.Left + FUCKBORDERBOTTOM.Width - player.Player_Back_Border.Margin.Left);
                //                var t = ThicknessAnimation(BT.Margin.Left - dist, BT.Margin.Top, 0);
                //                Storyboard.SetTargetProperty(t, new PropertyPath(FrameworkElement.MarginProperty));
                //                storyboard.Children.Add(t);
                //                storyboard.Begin(BT);

                //                var t2 = ThicknessAnimation2(FUCKBORDERBOTTOM.Margin.Left + FUCKBORDERBOTTOM.Width, player.Player_Back_Border.Margin.Top, 0);
                //                var r = new Storyboard();
                //                Storyboard.SetTargetProperty(t2, new PropertyPath(FrameworkElement.MarginProperty));
                //                r.Children.Add(t2);
                //                r.Begin(player.Player_Back_Border);
                //                canmoveleft = false;
                //            }
                //            else if (player.Player_Back_Border.Margin.Left < FUCKBORDERBOTTOM.Margin.Left && canmoveright && player.Player_Back_Border.Margin.Top >= FUCKBORDERBOTTOM.Margin.Top && player.Player_Back_Border.Margin.Top + player.Player_Back_Border.Height < FUCKBORDERBOTTOM.Margin.Top + FUCKBORDERBOTTOM.Height) //---->>
                //            {

                //                var dist = Math.Abs(FUCKBORDERBOTTOM.Margin.Left - player.Player_Back_Border.Margin.Left - player.Player_Back_Border.Width);
                //                var t = ThicknessAnimation(BT.Margin.Left + dist, BT.Margin.Top, 0);
                //                Storyboard.SetTargetProperty(t, new PropertyPath(FrameworkElement.MarginProperty));
                //                storyboard.Children.Add(t);
                //                storyboard.Begin(BT);

                //                var t2 = ThicknessAnimation2(FUCKBORDERBOTTOM.Margin.Left - player.Player_Back_Border.Width, player.Player_Back_Border.Margin.Top, 0);
                //                var r = new Storyboard();
                //                Storyboard.SetTargetProperty(t2, new PropertyPath(FrameworkElement.MarginProperty));
                //                r.Children.Add(t2);
                //                r.Begin(player.Player_Back_Border);
                //                canmoveright = false;
                //                //lab.Content =(" left: " + canmoveleft + " right: " + canmoveright, "Right");


                //            }
                //            else if (player.Player_Back_Border.Margin.Top > FUCKBORDERBOTTOM.Margin.Top && canmoveup && player.Player_Back_Border.Margin.Left > FUCKBORDERBOTTOM.Margin.Left && player.Player_Back_Border.Margin.Left + player.Player_Back_Border.Width < FUCKBORDERBOTTOM.Margin.Left + FUCKBORDERBOTTOM.Width
                //            ) //up
                //            {
                //                var dist = Math.Abs(player.Player_Back_Border.Margin.Top - FUCKBORDERBOTTOM.Margin.Top - FUCKBORDERBOTTOM.Height);
                //                var t = ThicknessAnimation(BT.Margin.Left, BT.Margin.Top - dist, 0);
                //                Storyboard.SetTargetProperty(t, new PropertyPath(FrameworkElement.MarginProperty));
                //                storyboard.Children.Add(t);
                //                storyboard.Begin(BT);

                //                var t2 = ThicknessAnimation2(player.Player_Back_Border.Margin.Left, FUCKBORDERBOTTOM.Margin.Top + FUCKBORDERBOTTOM.Height, 0);
                //                var r = new Storyboard();
                //                Storyboard.SetTargetProperty(t2, new PropertyPath(FrameworkElement.MarginProperty));
                //                r.Children.Add(t2);
                //                r.Begin(player.Player_Back_Border);
                //                canmoveup = false;
                //                //MessageBox.Show(" down: " + canmovedown + " up: " + canmoveup, "Up");
                //            }
                //            else if (player.Player_Back_Border.Margin.Top < FUCKBORDERBOTTOM.Margin.Top && canmovedown && player.Player_Back_Border.Margin.Left > FUCKBORDERBOTTOM.Margin.Left && player.Player_Back_Border.Margin.Left + player.Player_Back_Border.Width < FUCKBORDERBOTTOM.Margin.Left + FUCKBORDERBOTTOM.Width
                //            ) //down
                //            {
                //                var dist = Math.Abs(player.Player_Back_Border.Margin.Top + player.Player_Back_Border.Height - FUCKBORDERBOTTOM.Margin.Top);
                //                var t = ThicknessAnimation(BT.Margin.Left, BT.Margin.Top + dist, 0);
                //                Storyboard.SetTargetProperty(t, new PropertyPath(FrameworkElement.MarginProperty));
                //                storyboard.Children.Add(t);
                //                storyboard.Begin(BT);

                //                var t2 = ThicknessAnimation2(player.Player_Back_Border.Margin.Left, FUCKBORDERBOTTOM.Margin.Top - player.Player_Back_Border.Height, 0);
                //                var r = new Storyboard();
                //                Storyboard.SetTargetProperty(t2, new PropertyPath(FrameworkElement.MarginProperty));
                //                r.Children.Add(t2);
                //                r.Begin(player.Player_Back_Border);
                //                canmovedown = false;
                //                //MessageBox.Show(" down: " + canmovedown + " up: " + canmoveup, "Down");
                //            }

                //        }
                //        else
                //        {
                //            canmoveleft = true;
                //            canmoveright = true;
                //            canmovedown = true;
                //            canmoveup = true;
                //            //MessageBox.Show("right: " + canmoveright + " left: " + canmoveleft, "else");
                //        }
                //        //lab.Content = T.Margin + " " + player.Player_Back_Border.Margin + " \n" + canmoveleft;
                //        //lab.Content = $"left: {canmoveleft} right: {canmoveright}";
                //    });


                //    Thread.Sleep(50);
                //}
            }
            Thread borderLeft = new Thread(() =>
            {
                Collision(FUCKBORDERLEFT);
            });
            borderLeft.Start();

            Thread borderRight = new Thread(() =>
            {
                Collision(FUCKBORDERRIGHT);
            });
            borderRight.Start();


            Thread borderTop = new Thread(() =>
            {
                Collision(FUCKBORDERTOP);
            });
            borderTop.Start();


            Thread borderBottom = new Thread(() =>
            {
                Collision(FUCKBORDERBOTTOM);
            });
            borderBottom.Start();

        }

        public void Collision(Border bt)
        {
           
                while (true)
                {

                    this.Dispatcher.Invoke(() =>
                    {
                        var btLeft = bt.Margin.Left + (player.Player_Back_Border.Width * 0.5);
                        var btTop = bt.Margin.Top + (player.Player_Back_Border.Height * 0.5);
                        var btWid = bt.Width - (player.Player_Back_Border.Width);
                        var btHei = bt.Height - (player.Player_Back_Border.Height);

                        //proverka na razmer Border'a
                        if (btWid <= 0)
                        {
                            btWid = bt.Width;
                            btLeft = bt.Margin.Left;
                        }
                        if (btHei <= 0)
                        {
                            btHei = bt.Height;
                            btTop = bt.Margin.Top;
                        }

                        Rect rectbt = new Rect(btLeft, btTop, btWid, btHei);
                        Rect rectPlayer = new Rect(player.Player_Back_Border.Margin.Left, player.Player_Back_Border.Margin.Top, player.Player_Back_Border.Width, player.Player_Back_Border.Height);

                        /*if (player.Player_Back_Border.Margin.Left <= bt.Margin.Left + bt.Width && player.Player_Back_Border.Margin.Left + player.Player_Back_Border.Width >= bt.Margin.Left
                        && player.Player_Back_Border.Margin.Top + player.Player_Back_Border.Height
                        >= bt.Margin.Top && player.Player_Back_Border.Margin.Top <= bt.Margin.Top + bt.Height)*/
                        if(rectPlayer.IntersectsWith(rectbt))
                        {

                            if (player.Player_Back_Border.Margin.Left > btLeft && canmoveleft && player.Player_Back_Border.Margin.Top > btTop && player.Player_Back_Border.Margin.Top + player.Player_Back_Border.Height < btTop + btHei
                            ) //<<------
                            {

                                var dist = Math.Abs(btLeft + btWid - player.Player_Back_Border.Margin.Left);
                                var t = ThicknessAnimation(BT.Margin.Left - dist, BT.Margin.Top, 0);
                                Storyboard.SetTargetProperty(t, new PropertyPath(FrameworkElement.MarginProperty));
                                storyboard.Children.Clear();
                                storyboard.Children.Add(t);
                                storyboard.Begin(BT);

                                var t2 = ThicknessAnimation2(btLeft + btWid, player.Player_Back_Border.Margin.Top, 0);
                                var r = new Storyboard();
                                Storyboard.SetTargetProperty(t2, new PropertyPath(FrameworkElement.MarginProperty));
                                r.Children.Add(t2);
                                r.Begin(player.Player_Back_Border);
                                canmoveleft = false;
                            }
                            else if (player.Player_Back_Border.Margin.Left < btLeft && canmoveright && player.Player_Back_Border.Margin.Top >= btTop && player.Player_Back_Border.Margin.Top + player.Player_Back_Border.Height < btTop + btHei) //---->>
                            {

                                var dist = Math.Abs(btLeft - player.Player_Back_Border.Margin.Left - player.Player_Back_Border.Width);
                                var t = ThicknessAnimation(BT.Margin.Left + dist, BT.Margin.Top, 0);
                                Storyboard.SetTargetProperty(t, new PropertyPath(FrameworkElement.MarginProperty));
                                storyboard.Children.Clear();
                                storyboard.Children.Add(t);
                                storyboard.Begin(BT);

                                var t2 = ThicknessAnimation2(btLeft - player.Player_Back_Border.Width, player.Player_Back_Border.Margin.Top, 0);
                                var r = new Storyboard();
                                Storyboard.SetTargetProperty(t2, new PropertyPath(FrameworkElement.MarginProperty));
                                r.Children.Add(t2);
                                r.Begin(player.Player_Back_Border);
                                canmoveright = false;
                                //lab.Content =(" left: " + canmoveleft + " right: " + canmoveright, "Right");


                            }
                            else if (player.Player_Back_Border.Margin.Top > btTop && canmoveup && player.Player_Back_Border.Margin.Left > btLeft && player.Player_Back_Border.Margin.Left + player.Player_Back_Border.Width < btLeft + btWid
                            ) //up
                            {
                                var dist = Math.Abs(player.Player_Back_Border.Margin.Top - btTop - btHei);
                                var t = ThicknessAnimation(BT.Margin.Left, BT.Margin.Top - dist, 0);
                                Storyboard.SetTargetProperty(t, new PropertyPath(FrameworkElement.MarginProperty));
                                storyboard.Children.Clear();
                                storyboard.Children.Add(t);
                                storyboard.Begin(BT);

                                var t2 = ThicknessAnimation2(player.Player_Back_Border.Margin.Left, btTop + btHei, 0);
                                var r = new Storyboard();
                                Storyboard.SetTargetProperty(t2, new PropertyPath(FrameworkElement.MarginProperty));
                                r.Children.Add(t2);
                                r.Begin(player.Player_Back_Border);
                                canmoveup = false;
                                //MessageBox.Show(" down: " + canmovedown + " up: " + canmoveup, "Up");
                            }
                            else if (player.Player_Back_Border.Margin.Top < btTop && canmovedown && player.Player_Back_Border.Margin.Left > btLeft && player.Player_Back_Border.Margin.Left + player.Player_Back_Border.Width < btLeft + btWid
                            ) //down
                            {
                                var dist = Math.Abs(player.Player_Back_Border.Margin.Top + player.Player_Back_Border.Height - btTop);
                                var t = ThicknessAnimation(BT.Margin.Left, BT.Margin.Top + dist, 0);
                                Storyboard.SetTargetProperty(t, new PropertyPath(FrameworkElement.MarginProperty));
                                storyboard.Children.Clear();
                                storyboard.Children.Add(t);
                                storyboard.Begin(BT);

                                var t2 = ThicknessAnimation2(player.Player_Back_Border.Margin.Left, btTop - player.Player_Back_Border.Height, 0);
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
        }
        bool canmoveleft = true;
        bool canmoveright = true;
        bool canmoveup = true;
        bool canmovedown = true;

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
                    //MessageBox.Show(ex.Message + " " + ux + " " + uy);
                }
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

        public async void Minimap()
        {
            Border miniplayerborder = new Border()
            {
                Width = 12,
                Height = 16,
                Background = Brushes.White,
                Margin = new Thickness(251, 226.5, 0, 0),
            };
            MiniMapBorder.Child = miniplayerborder;
            await Task.Factory.StartNew(() =>
            {
                while(true)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        miniplayerborder.Margin = new Thickness(player.Player_Back_Border.Margin.Left / 10, player.Player_Back_Border.Margin.Top / 10, 0, 0);
                    });
                   
                    Task.Delay(10);
                }
            });
        }

        bool isattack = false;
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (Menu_border.Visibility == Visibility.Visible) Menu_border.Visibility = Visibility.Hidden;
                else Menu_border.Visibility = Visibility.Visible;
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void Window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            p = Mouse.GetPosition(this);
            double x = (player.PLayer_Front_Border.Margin.Left + 50) - p.X;
            double y = (player.PLayer_Front_Border.Margin.Top + 50) - p.Y;
            TimerX = x;
            TImerY = y;

            //lab.Content = player.Player_Back_Border.Margin.Top + " " + y;

            double sumXY = Math.Abs(x) + Math.Abs(y);
            double maxSpeed; 
            if(sumXY > 400) maxSpeed = sumXY / 333;
            else maxSpeed = sumXY / 222;

            timer.Stop();
            storyboard.Stop();

            ThicknessAnimation thicknessAnimation;
            ThicknessAnimation thicknessAnimation2;
            
            if(true)
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
            storyboard.Children.Clear();
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

        private void Exit(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(player.Player_Back_Border.Margin + "||" + player.PLayer_Front_Border.Margin + "||" + BT.Margin);
            var pr = Process.GetProcesses();
            foreach (var p in pr)
            {
                if (p.ProcessName.Contains("Team Project"))
                    p.Kill();
            }
        }

       
    }
}
