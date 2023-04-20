using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;

namespace Team_Project
{
    public class Weapon
    {

        public string Name { get; set; } = "";
        public double Damage { get; set; } = 0;
        public Border weapon_border { get; set; } = new Border();

        public Weapon() {

            
        }

        public Weapon(string name, double damage, string imagefilename)
        {
            weapon_border.Width = 60;
            weapon_border.Height = 120;
            Name = name;
            Damage = damage;
            BitmapImage img2 = new BitmapImage();

            img2.BeginInit();
            img2.StreamSource = new System.IO.MemoryStream(File.ReadAllBytes(Dir.GetPathX() + "\\Resources\\" + imagefilename));
            img2.EndInit();

            weapon_border.Background = new ImageBrush(img2);
        }
        
    }
}
