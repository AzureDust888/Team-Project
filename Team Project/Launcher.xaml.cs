using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Data.Linq;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
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
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfAnimatedGif;
using static System.Net.Mime.MediaTypeNames;

namespace Team_Project
{
    /// <summary>
    /// Interaction logic for Launcher.xaml
    /// </summary>
    public partial class Launcher : Window
    {
        public static User current_user = new User();
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
        DispatcherTimer timer = new DispatcherTimer();
        int tmp_k = 0;
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BitmapImage avatarimg = new BitmapImage();
            avatarimg.BeginInit();
            avatarimg.StreamSource = new System.IO.MemoryStream(current_user.Avatar);
            avatarimg.EndInit();
            CurrentUserAvatarBorder.Background = new ImageBrush(avatarimg);
            CurrentUserNameLabel.Content = current_user.UserName;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += delegate { tmp_k++;
                if (tmp_k == 3)
                {
                    Storyboard storyboard = new Storyboard();
                    TimeSpan duration = TimeSpan.FromSeconds(0.5);

                    DoubleAnimation animation = new DoubleAnimation();

                    animation.From = Loading_Border.Opacity;
                    animation.To = 0.0;
                    animation.Duration = new Duration(duration);
                    // Configure the animation to target de property Opacity
                    Storyboard.SetTargetName(animation, Loading_Border.Name);
                    Storyboard.SetTargetProperty(animation, new PropertyPath(Control.OpacityProperty));
                    storyboard.Children.Add(animation);
                    storyboard.Completed += delegate {
                        Loading_Border.Visibility = Visibility.Hidden;
                    };
                    storyboard.Begin(this);
                    timer.Stop();
                }
            };
            timer.Start();
           
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
            MessageBox.Show("Coming soon");
        }

        private void Play_Button_GotMouseCapture(object sender, MouseEventArgs e)
        {
            MainWindow.player = new Player(current_user.UserName, current_user.CurrentHp, current_user.CurrentMp, current_user.Lvl, current_user.CurrentExp, new Weapon("Novice Weapon", 12, "sword.png"), current_user.MaxHp, current_user.MaxMp);
            MainWindow.current_user = current_user;
            Thread start_game_window = new Thread(() =>
            {
                this.Dispatcher.Invoke(() => {
                    mainwindow.Show();
                   
                });

            });
            start_game_window.Start();
            this.Close();
        }

        private void Tutorial_Button_GotMouseCapture(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Coming soon");
        }

        private void Github_Source_Code_Button_GotMouseCapture(object sender, MouseEventArgs e)
        {
            try
            {
                string browserPath = GetBrowserPath();
                Process process = new Process();
                process.StartInfo = new ProcessStartInfo(browserPath);
                process.StartInfo.Arguments = "\""+ "https://github.com/AzureDust888/Team-Project" + "\"";
                process.Start();
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            
        }

        private static string GetBrowserPath()
        {
            string browser = string.Empty;
            RegistryKey key = null;

            try
            {
                // try location of default browser path in XP
                key = Registry.ClassesRoot.OpenSubKey(@"HTTP\shell\open\command", false);

                // try location of default browser path in Vista
                if (key == null)
                {
                    key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http", false); ;
                }

                if (key != null)
                {
                    //trim off quotes
                    browser = key.GetValue(null).ToString().ToLower().Replace("\"", "");
                    if (!browser.EndsWith("exe"))
                    {
                        //get rid of everything after the ".exe"
                        browser = browser.Substring(0, browser.LastIndexOf(".exe") + 4);
                    }

                    key.Close();
                }
            }
            catch
            {
                return string.Empty;
            }

            return browser;
        }

        private void Loading_Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CurrentUserAvatarBorder_GotMouseCapture(object sender, MouseEventArgs e)
        {
           
        }

        private void CurrentUserAvatarBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "png; jpg|*.png; *.jpg";
            ofd.ShowDialog();
            try
            {
                BitmapImage bp = new BitmapImage();
                bp.BeginInit();
                bp.StreamSource = new System.IO.MemoryStream(File.ReadAllBytes(ofd.FileName));
                bp.EndInit();
                CurrentUserAvatarBorder.Background = new ImageBrush(bp);
                DataContext db = new DataContext(LinqClass.connectionstring);
                var t = db.GetTable<User>().ToList();
                for (int i = 0; i < t.Count; i++)
                {
                    if (t[i].UserName == current_user.UserName)
                    {
                        t[i].Avatar = File.ReadAllBytes(ofd.FileName);
                        db.SubmitChanges();
                        current_user.Avatar = t[i].Avatar;
                        break;
                    }
                }
            }
            catch (Exception exception)
            {
            }
        }
    }
}
