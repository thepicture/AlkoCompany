﻿using AlkoCompanyNew.Models;
using AlkoCompanyNew.Models.Entities;
using System.Linq;
using System.Threading.Tasks;
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

        private async void ButtonVhod_ClickAsync(object sender, RoutedEventArgs e)
        {
            string login = TextBoxLogin.Text;
            string password = PasswordBoxPassword.Password;
            if (string.IsNullOrEmpty(login))
            {
                MessageBox.Show("Введите логин");
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите пароль");
                return;
            }

            ButtonVhod.Content = "Подождите...";
            ButtonVhod.IsEnabled = false;
            LogInProgress.Visibility = Visibility.Visible;
            Sotrudnick sotrudnick = await Task.Run(() =>
            {
                return AppData.Context.Sotrudnick
                .FirstOrDefault(s =>
                    s.S_Login == login && s.S_Password == password);
            });
            ButtonVhod.Content = "Вход";
            ButtonVhod.IsEnabled = true;
            LogInProgress.Visibility = Visibility.Collapsed;

            if (sotrudnick == null)
            {
                MessageBox.Show("Введен неверный логин или пароль");
            }
            else
            {
                pubsotr = sotrudnick;
                fio = $"{sotrudnick.S_Fio}";
                MessageBox.Show("Пользователь авторизован");
                BaseWindowSotrudnick Form = new BaseWindowSotrudnick();
                Form.Show();
                Close();
            }
        }
    }
}
