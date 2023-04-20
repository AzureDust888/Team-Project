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

        public int GoldCoins { get; set; }

        public Weapon weapon { get; set; }

        public Player() { }
           
        
        public Player(string name, double hp, double mp, int lvl, double exp, Weapon weapon, double maxhp, double maxmp)
        {
            BitmapImage img = new BitmapImage(new Uri(Dir.GetPathX() + "\\Resources\\player_topchik.png"));
            Int32Rect cropRect = new Int32Rect(0, 0, 120, 180);
            CroppedBitmap croppedBitmap = new CroppedBitmap(img, cropRect);
            PLayer_Front_Border.Background = new ImageBrush(croppedBitmap);
            Name = name;
            Hp = hp;
            Mp = mp;
            Lvl = lvl;
            Exp = exp;
            this.weapon = weapon;
            this.MaxHp = maxhp;
            this.MaxMp = maxmp;
        }
    }

}
