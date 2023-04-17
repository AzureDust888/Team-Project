using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Windows.Threading;

namespace Team_Project
{
    public class EnemyClass : MainWindow
    {
        public string Name { get; set; }
        public double Hp { get; set; }
        public double MaxHp { get; set; }
        public double Mp { get; set; }
        public double MaxMp { get; set; }
        public int Lvl { get; set; }
        public double Exp { get; set; }
        public double Dmg { get; set; }
        int ux = 0;
        int ux2 = 0;
        int uy = 130;
        public Border border = new Border();
        Storyboard storyboard = new Storyboard();
        Storyboard storyboard2 = new Storyboard();
        Storyboard storyboard3 = new Storyboard();
        Storyboard storyboard4 = new Storyboard();
        Storyboard storyboard5 = new Storyboard();
        DispatcherTimer timerAnimation = new DispatcherTimer();
        bool isrunning = false;
        bool isc = true;
        ThicknessAnimation thicknessAnimation;
        Point cord = new Point();
        public EnemyClass(double hptmp, double mptmp, string nametmp)
        {
            Dmg = 7;
            timerAnimation.Interval = TimeSpan.FromSeconds(0.04);
            timerAnimation.Tick += delegate {
                this.Dispatcher.Invoke(() =>
                {
                    if(border.Margin.Left < MainWindow.player.Player_Back_Border.Margin.Left)
                    {
                        try
                        {
                            if (ux >= 1050)
                                ux = 0;


                            BitmapImage img = new BitmapImage(new Uri(dir.FullName + "\\Resources\\cerberus.png"));

                            Int32Rect cropRect = new Int32Rect(ux, uy, 120, 100);

                            CroppedBitmap croppedBitmap = new CroppedBitmap(img, cropRect);

                            border.Background = new ImageBrush(croppedBitmap);

                            ux += 159;

                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(ex.Message + " " + ux + " " + uy);
                        }
                    }
                    else if(border.Margin.Left >= MainWindow.player.Player_Back_Border.Margin.Left)
                    {
                        try
                        {
                            if (ux2 <= 0)
                                ux2 = 1100;


                            BitmapImage img = new BitmapImage(new Uri(dir.FullName + "\\Resources\\cerberusInvert.png"));

                            Int32Rect cropRect = new Int32Rect(ux2, uy, 120, 100);

                            CroppedBitmap croppedBitmap = new CroppedBitmap(img, cropRect);

                            border.Background = new ImageBrush(croppedBitmap);

                            ux2 -= 159;

                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(ex.Message + " " + ux2 + " " + uy);
                        }
                    }
                  

                });
            };
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
            Int32Rect cropRect = new Int32Rect(0, 0, 100, 100);
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
                int i = 0;
                while (ift)
                {

                    Dispatcher.Invoke(new Action(async () =>
                    {



                        if (Math.Abs(border.Margin.Left - MainWindow.player.Player_Back_Border.Margin.Left) <= 250 && Math.Abs(border.Margin.Top - MainWindow.player.Player_Back_Border.Margin.Top) <= 250 && isc)
                        {
                            storyboard.Pause();
                            double tmpLeft = 0;
                            double tmpTop = 0;
                            if (border.Margin.Left > MainWindow.player.Player_Back_Border.Margin.Left) tmpLeft = MainWindow.player.Player_Back_Border.Margin.Left + 90;
                            else tmpLeft = MainWindow.player.Player_Back_Border.Margin.Left - 90;

                            thicknessAnimation = new ThicknessAnimation
                            {

                                From = new Thickness(border.Margin.Left, border.Margin.Top, 0, 0),
                                To = new Thickness(tmpLeft, MainWindow.player.Player_Back_Border.Margin.Top, 0, 0),

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
                            if (MainWindow.weapon.IsEnabled == true)
                            {
                                Hp -= 40;
                                if (Hp < 0) Hp = 0;
                                hp.Value = Hp;
                                hplabel.Content = Hp.ToString();

                            }
                        }
                        if (Hp <= 0 && ift)
                        {
                            BitmapImage img = new BitmapImage(new Uri(dir.FullName + "\\Resources\\cerberus.png"));


                            //кусок картинки
                            Int32Rect cropRect = new Int32Rect(1020, 1010, 100, 50);
                            CroppedBitmap croppedBitmap = new CroppedBitmap(img, cropRect);

                            border.Background = new ImageBrush(croppedBitmap);
                            thicknessAnimation = new ThicknessAnimation
                            {

                                From = new Thickness(border.Margin.Left, border.Margin.Top, 0, 0),
                                To = new Thickness(border.Margin.Left, border.Margin.Top, 0, 0),

                                Duration = TimeSpan.FromSeconds(2)

                            };

                            Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath(FrameworkElement.MarginProperty));
                            storyboard5.Children.Add(thicknessAnimation);
                            storyboard5.Begin(border);
                            timerAnimation.Stop();
                            await Task.Delay(2000);
                            ((MainWindow)System.Windows.Application.Current.MainWindow).canvas_enemy.Children.Remove(border);

                            ift = false;
                        }
                        if (Math.Abs(border.Margin.Left - MainWindow.player.Player_Back_Border.Margin.Left) <= 120 && Math.Abs(border.Margin.Top - MainWindow.player.Player_Back_Border.Margin.Top) <= 10 && isc) // Mob attack
                        {
                            if(i%2 == 0)
                            {
                                MainWindow.player.Hp -= Dmg;
                                if (MainWindow.player.Hp < 0) MainWindow.player.Hp = 0;
                                ((MainWindow)System.Windows.Application.Current.MainWindow).PlayerHp.Value = MainWindow.player.Hp;
                            }
                            
                            //MessageBox.Show("MainWindow.player.Hp " + MainWindow.player.Hp);
                        }


                    }));
                    if (i >= 100) i = 0;
                    i++;
                    Thread.Sleep(200);
                }
            });
            timerAnimation.Start();
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
}
