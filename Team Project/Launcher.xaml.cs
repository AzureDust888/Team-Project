using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfAnimatedGif;

namespace Team_Project
{
    /// <summary>
    /// Interaction logic for Launcher.xaml
    /// </summary>
    public partial class Launcher : Window
    {
        MainWindow mainwindow = new MainWindow();
        public Launcher()
        {
            InitializeComponent();
        }

        private void Launcher_Main_Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ImageAwesome_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var pr = Process.GetProcesses();
            foreach (var p in pr)
            {
                if (p.ProcessName.Contains("Team Project"))
                    p.Kill();
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.UriSource = new Uri(MainWindow.dirname + "\\Resources\\campfire.gif");
                        image.EndInit();
                        ImageBehavior.SetAnimatedSource(SpawnCampFire, image);
                    });

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                Task.Delay(1000);
            });
        }


        private void Exit_Button_GotMouseCapture(object sender, MouseEventArgs e)
        {
            var pr = Process.GetProcesses();
            foreach (var p in pr)
            {
                if (p.ProcessName.Contains("Team Project"))
                    p.Kill();
            }
        }

        private void Setting_Button_GotMouseCapture(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Settings");
        }

        private void Play_Button_GotMouseCapture(object sender, MouseEventArgs e)
        {
            Thread start_game_window = new Thread(() =>
            {
                this.Dispatcher.Invoke(() => {
                    mainwindow.Show();
                   
                });

            });
            start_game_window.Start();
            this.Close();
        }
    }
}
