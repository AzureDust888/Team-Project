using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Threading;
using System.Windows.Media.Animation;

namespace Team_Project
{
    public class Player : MainWindow
    {
        public Border Player_Back_Border = new Border()
        {
            Width = 120,
            Height = 160,
            Margin = new Thickness(2510, 2265, 0, 0),
        };
        public Border PLayer_Front_Border = new Border()
        {
            Width = 120,
            Height = 160,
            Margin = new Thickness(900, 482, 0, 0),
        };
        public string Name { get; set; }
        public double Hp { get; set; }
        public double MaxHp { get; set; } = 200;
        public double Mp { get; set; }
        public double MaxMp { get; set; } = 200;
        public int Lvl { get; set; }
        public double Exp { get; set; }
        public double CurrentLvlExpCap { get; set; } = 1000;
        public Weapon weapon { get; set; }

        public string ExpProperty { get {
                return this.Exp + "/" + Convert.ToString(this.Lvl * 100);
            } }

        public Player() { }
           
        
        public Player(string name, double hp, double mp, int lvl, double exp, Weapon weapon)
        {
            BitmapImage img = new BitmapImage(new Uri(MainWindow.dirname + "\\Resources\\player_topchik.png"));
            Int32Rect cropRect = new Int32Rect(0, 0, 120, 180);
            CroppedBitmap croppedBitmap = new CroppedBitmap(img, cropRect);
            PLayer_Front_Border.Background = new ImageBrush(croppedBitmap);
            Name = name;
            Hp = hp;
            Mp = mp;
            Lvl = lvl;
            Exp = exp;
            this.weapon = weapon;

            //Thread t = new Thread(() =>
            //    {
            //        while (true)
            //        {
            //            if(this.Hp <= 0)
            //            {
            //                try
            //                {
            //                    Dispatcher.Invoke(() =>
            //                    {

            //                        Storyboard s = new Storyboard();
                                
            //                        var thicknessAnimation = new ThicknessAnimation
            //                        {
                                        
            //                            From = new Thickness(((MainWindow)System.Windows.Application.Current.MainWindow).BT.Margin.Left, ((MainWindow)System.Windows.Application.Current.MainWindow).BT.Margin.Top, 0, 0),
            //                            To = new Thickness(0, 0, 0, 0),

            //                            Duration = TimeSpan.FromSeconds(0),

            //                        };


            //                        Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath(FrameworkElement.MarginProperty));
            //                        s.Children.Add(thicknessAnimation);
            //                        s.Begin(((MainWindow)System.Windows.Application.Current.MainWindow).BT);

            //                        thicknessAnimation = new ThicknessAnimation
            //                        {

            //                            From = new Thickness(Player_Back_Border.Margin.Left, Player_Back_Border.Margin.Top, 0, 0),
            //                            To = new Thickness(2510, 2265, 0, 0),

            //                            Duration = TimeSpan.FromSeconds(0),

            //                        };

            //                        Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath(FrameworkElement.MarginProperty));
            //                        s.Children.Add(thicknessAnimation);
            //                        s.Begin(Player_Back_Border);

            //                        thicknessAnimation = new ThicknessAnimation
            //                        {

            //                            From = new Thickness(PLayer_Front_Border.Margin.Left, PLayer_Front_Border.Margin.Top, 0, 0),
            //                            To = new Thickness(900, 482, 0, 0),

            //                            Duration = TimeSpan.FromSeconds(0),

            //                        };

            //                        Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath(FrameworkElement.MarginProperty));
            //                        s.Children.Add(thicknessAnimation);
            //                        s.Begin(PLayer_Front_Border);


            //                        Hp = 100;

            //                    });
            //                }
            //                catch(Exception ex) { MessageBox.Show(ex.Message); }

            //            }
            //            Thread.Sleep(100);
            //        }
            //});
            //t.Start();
            
        }
    }

}
