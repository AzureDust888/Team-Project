using Nest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Team_Project
{
    internal class Map
    {
        string dirname = MainWindow.dirname;

        public double wid { get; set; }
        public double hei { get; set; }       
        public ushort CellWidth { get; set; } = 256;
        public ushort CellHeight { get; set; } = 256;
        public ushort Rows { get; set; } = 20;
        public ushort Cols { get; set; } = 20;

        public Map(double bt_wid, double bt_hei) 
        {
            wid = bt_wid;
            hei = bt_hei;
        }
        public Map(ushort width, ushort height, ushort rows, ushort cols,double bt_wid,double bt_hei) 
        {
            CellWidth = width;
            CellHeight = height;
            Rows = rows;
            Cols = cols;
            wid= bt_wid;
            hei= bt_hei;
        }

        public Border Map_new()
        {
            Border tmpBorder = new Border();
            Canvas tmpCanvas = new Canvas();

            BitmapImage groundImg = new BitmapImage(new Uri(dirname + "\\Textures\\File00000893.png"));

            BitmapImage groundImg10 = new BitmapImage(new Uri(dirname + "\\Textures\\File00000939.png")); //stone
            BitmapImage groundImg9 = new BitmapImage(new Uri(dirname + "\\Textures\\File00000815.png")); //sand2
            BitmapImage groundImg8 = new BitmapImage(new Uri(dirname + "\\Textures\\File00000825.png")); //sand
            BitmapImage groundImg7 = new BitmapImage(new Uri(dirname + "\\Textures\\File00000740.png"));
            BitmapImage groundImg6 = new BitmapImage(new Uri(dirname + "\\Textures\\File00000639.png")); //snow
            BitmapImage groundImg4 = new BitmapImage(new Uri(dirname + "\\Textures\\File00000516.png"));
            BitmapImage groundImg2 = new BitmapImage(new Uri(dirname + "\\Textures\\File00000454.png"));
            BitmapImage groundImg3 = new BitmapImage(new Uri(dirname + "\\Textures\\File00000495.png"));

            for (int row = 0; row < 30; row++)
            {
                for (int col = 0; col < 30; col++)
                {
                    Image image = new Image();
                    if (row <= 7 || col <= 7 || col>=23 || row >= 23)
                    {
                        image.Source = groundImg3;
                    }
                    else
                    {
                        image.Source = groundImg2;
                    }

                    image.Width = CellWidth;
                    image.Height = CellHeight;
                    Canvas.SetLeft(image, col * CellWidth - wid);    //bt.width для отступа влево т.к. привязка к другим координатам, а они слишком уехали вправо
                    Canvas.SetTop(image, row * CellHeight - hei);

                    tmpCanvas.Children.Add(image);
                }
            }
            tmpBorder.Child = tmpCanvas;
            return tmpBorder;
        }

        public Border Map_Trees()
        {
            Border tmpBorder = new Border();
            Canvas tmpCanvas = new Canvas();

            BitmapImage TreeImg = new BitmapImage(new Uri(dirname + "\\Resources\\tree.png"));
            BitmapImage TreeImg2 = new BitmapImage(new Uri(dirname + "\\Resources\\tree2.png"));
            BitmapImage TreeImg3 = new BitmapImage(new Uri(dirname + "\\Resources\\tree3.png"));
            BitmapImage TreeImg4 = new BitmapImage(new Uri(dirname + "\\Resources\\tree4.png"));
            BitmapImage TreeImg5 = new BitmapImage(new Uri(dirname + "\\Resources\\tree5.png"));
            BitmapImage TreeImg6 = new BitmapImage(new Uri(dirname + "\\Resources\\tree6.png"));
            BitmapImage GrassImg = new BitmapImage(new Uri(dirname + "\\Resources\\grass.png"));

            BitmapImage TreeImg7 = new BitmapImage(new Uri(dirname + "\\Resources\\tree7.png"));     //за картой деревья
            CroppedBitmap TreeImg7_1 = new CroppedBitmap(TreeImg7, new Int32Rect(0, 0, 585, 650));
            CroppedBitmap TreeImg7_2 = new CroppedBitmap(TreeImg7, new Int32Rect(585, 0, 475, 650));

            BitmapImage groundImg683 = new BitmapImage(new Uri(dirname + "\\Textures\\File00000683.png"));

            for (int i = 0; i < 40; i++)
            {
                Image image = new Image();
                image.Width = TreeImg5.Width;
                image.Height = TreeImg5.Height;
                image.Source = TreeImg5;
                image.Margin = new Thickness(new Random().NextDouble() * 4000-2000, new Random().NextDouble() * 4000 - 2000, 0, 0);
                tmpCanvas.Children.Add(image);
                /*if(i < 30)
                {*/
                    Image image2 = new Image();
                    image2.Width = GrassImg.Width * 0.6;
                    image2.Height = GrassImg.Height * 0.6;
                    image2.Source = GrassImg;
                    image2.Margin = new Thickness(i * image2.Width * 0.8 - (wid * 2), hei * 0.85, 0, 0);

                    tmpCanvas.Children.Add(image2);
                /*}*/
            }

            tmpBorder.Child = tmpCanvas;
            return tmpBorder;
        }

        public List<Border> Map_Houses()
        {
            List<Border> borderList = new List<Border>();

            BitmapImage HouseImg = new BitmapImage(new Uri(dirname + "\\Resources\\house.png"));
            BitmapImage HouseImg2 = new BitmapImage(new Uri(dirname + "\\Resources\\house2.png"));


            for (int i = 0; i < 4; i++)
            {
                Border border = new Border();
                border.Width = HouseImg.Width;
                border.Height = HouseImg.Height;

                if (i % 2 == 0)
                {
                    border.Background = new ImageBrush(HouseImg2);
                }   
                else
                {
                    border.Background = new ImageBrush(HouseImg);
                }               
                
                border.BorderThickness = new Thickness(1);
                border.BorderBrush = Brushes.White;
                //border.CornerRadius = new CornerRadius(500);
                border.Margin = new Thickness(1700 + new Random().Next(-1500,1500), 1800 + new Random().Next(-1500, 1500), 0, 0);

                borderList.Add(border);
            }

            return borderList;
        }


    }
}
