using AlkoCompanyNew.Models;
using AlkoCompanyNew.Models.Entities;
using System.Linq;
using System.Windows;

namespace AlkoCompanyNew.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Sotrudnick pubsotr;
        public static string fio;

        public MainWindow()
        {
            InitializeComponent();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            TextBoxPassword.Visibility = Visibility.Visible;
            PasswordBoxPassword.Visibility = Visibility.Hidden;
            TextBoxPassword.Text = PasswordBoxPassword.Password;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            TextBoxPassword.Visibility = Visibility.Hidden;
            PasswordBoxPassword.Visibility = Visibility.Visible;
            TextBoxPassword.Text = PasswordBoxPassword.Password;
        }

        private void ButtonVhod_Click(object sender, RoutedEventArgs e)
        {
            string login = TextBoxLogin.Text;
            string password = PasswordBoxPassword.Password;
            if (string.IsNullOrEmpty(login))
            {
                _ = MessageBox.Show("Введите логин");
            }

            else if (string.IsNullOrEmpty(password))
            {
                _ = MessageBox.Show("Введите пароль");
            }

            else
            {
                var V = from sotrudnick in AppData.Context.Sotrudnick
                        where sotrudnick.S_Login == TextBoxLogin.Text && sotrudnick.S_Password == PasswordBoxPassword.Password
                        select new
                        {
                            Fio = sotrudnick.S_Fio,
                            Login = sotrudnick.S_Login,
                            Password = sotrudnick.S_Password
                        };
                //if (V.Count() != 0)
                //{
                //   
                //    MessageBox.Show("Вход успешно выполнен");
                //    var Form = new BaseWindowSotrudnick();
                //    Form.Show();
                //    this.Close();
                //}

                //else
                //{
                //    MessageBox.Show("Логин или пароль введен неверно");                     
                //}
                //TextBoxLogin.Clear();
                //PasswordBoxPassword.Clear();
                //TextBoxPassword.Clear();
                if (V.Count() == 0)
                {
                    _ = MessageBox.Show("Введен неверный логин или пароль");
                }
                else
                {
                    foreach (var item in V)
                    {
                        pubsotr = AppData.Context.Sotrudnick.First(x => x.S_Login == item.Login);
                        fio = $"{item.Fio}";
                    }
                    _ = MessageBox.Show("Пользователь авторизован");
                    BaseWindowSotrudnick Form = new BaseWindowSotrudnick();
                    Form.Show();
                    Close();
                }
            }
        }
    }
}
