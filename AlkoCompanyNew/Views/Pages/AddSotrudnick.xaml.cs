using AlkoCompanyNew.Models;
using AlkoCompanyNew.Models.Entities;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AlkoCompanyNew.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddSotrudnick.xaml
    /// </summary>
    public partial class AddSotrudnick : Page
    {
        public Sotrudnick Sotrudnick { get; set; } = new Sotrudnick();
        public AddSotrudnick(Sotrudnick inputSotrudnick)
        {
            InitializeComponent();
            ComboRoles.ItemsSource = AppData.Context.SotrudnickRole.ToList();
            Sotrudnick.S_Born = DateTime.Now;
            if (inputSotrudnick != null)
            {
                Sotrudnick = inputSotrudnick;
                ComboRoles.SelectedItem = ComboRoles.Items
                    .Cast<SotrudnickRole>()
                    .First(r => r.Id == inputSotrudnick.SotrudnickRole.Id);
            }
            DataContext = this;
        }
        private void Photo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog
            {
                Filter = "Image files (*.JPG, *.PNG)| *jpg; *.png;",

            };
            if (file.ShowDialog() == true)
            {
                Sotrudnick.S_Photo = File.ReadAllBytes(file.FileName);
            }
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Sotrudnick.S_Email)
                || !Regex.IsMatch(Sotrudnick.S_Email, @"\w+@\w+\.\w{2,3}"))
            {
                MessageBox.Show("Введите корректную электронную почту");
                return;
            }
            if (Sotrudnick.S_ID == 0)
            {
                AppData.Context.Sotrudnick.Add(Sotrudnick);
            }
            try
            {
                AppData.Context.SaveChanges();
                MessageBox.Show("Данные внесены");
                AppData.Context.ChangeTracker.Entries().ToList().ForEach(entry => entry.Reload());
                AppData.Context.ChangeTracker.Entries().ToList().ForEach(i => i.Reload());
                AppData.Sotrudnicki_.Reload();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(),
                                "Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                Debug.WriteLine(ex);
            }
        }

        private void OnBackClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(null);
        }
    }
}
