using AlkoCompanyNew.Models;
using AlkoCompanyNew.Models.Entities;
using AlkoCompanyNew.Models.Enums;
using Microsoft.Win32;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace AlkoCompanyNew.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddSotrudnick.xaml
    /// </summary>
    public partial class AddSotrudnick : Page
    {
        public Sotrudnick Sotrudnick { get; set; } = new Sotrudnick();
        public Sotrudnick sotr = new Sotrudnick();
        public AddSotrudnick(Sotrudnick settersotr)
        {
            InitializeComponent();
            if (settersotr != null)
                sotr = settersotr;

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
            if (sotr.S_ID == 0)
            {

                AppData.Context.Sotrudnick.Add(sotr);
            }
            try
            {
                AppData.Context.Entry(AppData.Context.Sotrudnick.Find(sotr.S_ID)).CurrentValues.SetValues(sotr);
                AppData.Context.SaveChanges();
                MessageBox.Show("Данные внесены");
                AppData.Context.ChangeTracker.Entries().ToList().ForEach(entry => entry.Reload());
                AppData.Context.ChangeTracker.Entries().ToList().ForEach(i => i.Reload());
                AppData.Sotrudnicki_.Reload();


            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
        }



        private void DateBorn_Loaded(object sender, RoutedEventArgs e)
        {
            DateBorn.Text = DateTime.Now.ToString("dd-MM-yyyy");
        }
    }
}
