using FontAwesome.WPF;
using System;
using System.Collections.Generic;
using System.Data.Linq;
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

namespace Team_Project
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            TextBox_PasswordShow_Login.Visibility = Visibility.Hidden;
            TextBox_PasswordShow_Signup_Repeat.Visibility = Visibility.Hidden;
            TextBox_PasswordShow_Signup.Visibility = Visibility.Hidden;
            SignupCanvas.Visibility = Visibility.Hidden;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

        private void ShowHidePAsswordButton_SignUp1_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (ShowHidePAsswordButton_SignUp1.Icon == FontAwesomeIcon.Eye)
            {
                TextBox_PasswordShow_Signup.Text = Password_SignUp.Password;
                TextBox_PasswordShow_Signup.Visibility = Visibility.Visible;
                Password_SignUp.Visibility = Visibility.Hidden;
                ShowHidePAsswordButton_SignUp1.Icon = FontAwesomeIcon.EyeSlash;
            }
            else
            {
                Password_SignUp.Password = TextBox_PasswordShow_Signup.Text;
                TextBox_PasswordShow_Signup.Visibility = Visibility.Hidden;
                Password_SignUp.Visibility = Visibility.Visible;
                ShowHidePAsswordButton_SignUp1.Icon = FontAwesomeIcon.Eye;
            }
        }

        private void ShowHidePAsswordButton_SignUp2_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (ShowHidePAsswordButton_SignUp2.Icon == FontAwesomeIcon.Eye)
            {
                TextBox_PasswordShow_Signup_Repeat.Text = Password_SignUp_Repeat.Password;
                TextBox_PasswordShow_Signup_Repeat.Visibility = Visibility.Visible;
                Password_SignUp_Repeat.Visibility = Visibility.Hidden;
                ShowHidePAsswordButton_SignUp2.Icon = FontAwesomeIcon.EyeSlash;
            }
            else
            {
                Password_SignUp_Repeat.Password = TextBox_PasswordShow_Signup_Repeat.Text;
                TextBox_PasswordShow_Signup_Repeat.Visibility = Visibility.Hidden;
                Password_SignUp_Repeat.Visibility = Visibility.Visible;
                ShowHidePAsswordButton_SignUp2.Icon = FontAwesomeIcon.Eye;
            }
        }


        private void BackToLogin_OnMouseEnter(object sender, MouseEventArgs e)
        {
            BackToLogin.Foreground = Brushes.Aquamarine;
            BackToLogin.TextDecorations = TextDecorations.Underline;
        }

        private void BackToLogin_OnMouseLeave(object sender, MouseEventArgs e)
        {
            var color = (Color)ColorConverter.ConvertFromString("#cdd9e5");
            var colorlast = new SolidColorBrush(color);
            BackToLogin.Foreground = colorlast;
            BackToLogin.TextDecorations = null;
        }

        private void BackToLogin_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SignupCanvas.Visibility = Visibility.Hidden;
            LoginCanvas.Visibility = Visibility.Visible;
            TextBox_PasswordShow_Signup_Repeat.Text = "";
            TextBox_PasswordShow_Signup.Text = "";
            Password_SignUp.Password = "";
            Password_SignUp_Repeat.Password = "";
            UserNameSignUp.Text = "";
        }


        private void CreateAccount_OnMouseEnter(object sender, MouseEventArgs e)
        {
            CreateAccount.Foreground = Brushes.Aquamarine;
            CreateAccount.TextDecorations = TextDecorations.Underline;
        }

        private void CreateAccount_OnMouseLeave(object sender, MouseEventArgs e)
        {
            var color = (Color)ColorConverter.ConvertFromString("#cdd9e5");
            var colorlast = new SolidColorBrush(color);
            CreateAccount.Foreground = colorlast;
            CreateAccount.TextDecorations = null;
        }

        private void CreateAccount_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            LoginCanvas.Visibility = Visibility.Hidden;
            Password_Login.Password = "";
            TextBox_PasswordShow_Login.Text = "";
            UserNameLogin.Text = "";
            SignupCanvas.Visibility = Visibility.Visible;
        }

        private void Show_Password_Button(object sender, MouseButtonEventArgs e)
        {
            if (ShowHidePAsswordButton.Icon == FontAwesomeIcon.Eye)
            {
                TextBox_PasswordShow_Login.Text = Password_Login.Password;
                TextBox_PasswordShow_Login.Visibility = Visibility.Visible;
                Password_Login.Visibility = Visibility.Hidden;
                ShowHidePAsswordButton.Icon = FontAwesomeIcon.EyeSlash;
            }
            else
            {
                Password_Login.Password = TextBox_PasswordShow_Login.Text;
                TextBox_PasswordShow_Login.Visibility = Visibility.Hidden;
                Password_Login.Visibility = Visibility.Visible;
                ShowHidePAsswordButton.Icon = FontAwesomeIcon.Eye;
            }
        }
        Launcher l = new Launcher();
        private void Button_Click(object sender, RoutedEventArgs e)//sign up || create acc
        {
            try
            {
                if (UserNameSignUp.Text.Trim().Length > 2 && Password_SignUp.Password.Trim().Length > 2 && Password_SignUp.Password == Password_SignUp_Repeat.Password)
                {
                    Launcher.current_user = new User(UserNameSignUp.Text, LinqClass.ToSHA256(Password_SignUp.Password));
                    DataContext db = new DataContext(LinqClass.connectionstring);
                    db.GetTable<User>().InsertOnSubmit(Launcher.current_user);
                    db.SubmitChanges();
                    Thread start_launcher_window = new Thread(() =>
                    {
                        this.Dispatcher.Invoke(() => {
                            l.Show();

                        });

                    });
                    start_launcher_window.Start();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect Input!");
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
           
           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//log in || existing acc
        {
            try
            {
                DataContext db = new DataContext(LinqClass.connectionstring);
                User tmpuser = null;
                foreach (var item in db.GetTable<User>().ToList<User>())
                {
                    if(item is User)
                    {
                        User u = (User)item;
                        if(u.UserName == UserNameLogin.Text && u.Password == LinqClass.ToSHA256(Password_Login.Password))
                        {
                            tmpuser = u;
                            break;
                        }
                    }
                }

                if(tmpuser != null)
                {
                    Launcher.current_user = tmpuser;
                    Thread start_launcher_window = new Thread(() =>
                    {
                        this.Dispatcher.Invoke(() => {
                            l.Show();
                        });

                    });
                    start_launcher_window.Start();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect password or username or user doesn't exist. Try again");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

           
        }
    }
}
