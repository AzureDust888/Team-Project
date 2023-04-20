using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Team_Project
{
    public class Shop
    {
        public Border Shop_Border { get; set; } = new Border()
        {
            Width = 450,
            Height = 400,
            Margin = new Thickness(2500, 1900, 0, 0),
            Background = Brushes.Black
        };

        public Shop() {
            BitmapImage img = new BitmapImage(new Uri(Dir.GetPathX() + "\\Resources\\house2.png"));
            Shop_Border.Background = new ImageBrush(img);
        }
    }
}
