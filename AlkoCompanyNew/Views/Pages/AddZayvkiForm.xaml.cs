using AlkoCompanyNew.Models;
using AlkoCompanyNew.Models.Entities;
using AlkoCompanyNew.Views.Windows;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace AlkoCompanyNew.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddZayvkiForm.xaml
    /// </summary>
    public partial class AddZayvkiForm : Page
    {
        private readonly Zayavka zayavka = new Zayavka();
        private readonly Klient kl = new Klient();

        public AddZayvkiForm(Zayavka setterzayvka)
        {
            InitializeComponent();
            //TextBoxSotrudnick.Text += $"{MainWindow.fio}";
            if (setterzayvka != null)
            {
                zayavka = setterzayvka;
            }

            DataContext = zayavka;
        }

        private void AddOnePhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog
            {
                Filter = "Image files (*.JPG, *.PNG)| *jpg; *.png;",
                Multiselect = true
            };
            if (file.ShowDialog() == true)
            {
                Photo.Source = new BitmapImage(new Uri(file.FileName));
                zayavka.Z_PhotoPreview = File.ReadAllBytes(file.FileName);
            }
        }

        private void TextBoxSotrudnick_Loaded(object sender, RoutedEventArgs e)
        {
            TextBoxSotrudnick.Text = $"{MainWindow.fio}";
            _ = TextBoxSotrudnick.Focus();
            TextBoxSotrudnick.Visibility = Visibility.Hidden;
        }

        private void TextBoxData1_Loaded(object sender, RoutedEventArgs e)
        {
            TextBoxData1.Text = DateTime.Now.ToString("dd-MM-yyyy");
            _ = TextBoxData1.Focus();
            TextBoxData1.Visibility = Visibility.Hidden;
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            string adress = TextBoxAdress.Text;
            string klient = TextBoxklient.Text;
            string tel = TextBoxTelNumber.Text;
            if (string.IsNullOrEmpty(adress))
            {
                _ = MessageBox.Show("Введите адрес");
            }
            else if (string.IsNullOrEmpty(klient))
            {
                _ = MessageBox.Show("Введите клиента");
            }
            else if (string.IsNullOrEmpty(tel))
            {
                _ = MessageBox.Show("Введите номер телефона клиента");
            }

            else if (zayavka.Z_ID == 0)
            {

                _ = AppData.Context.Zayavka.Add(zayavka);
            }
            try
            {
                AppData.Context.Entry(AppData.Context.Zayavka.Find(zayavka.Z_ID)).CurrentValues.SetValues(zayavka);
                _ = AppData.Context.SaveChanges();
                _ = MessageBox.Show("Данные успешно внесены");
                AppData.Context.ChangeTracker.Entries().ToList().ForEach(entry => entry.Reload());
                AppData.Context.ChangeTracker.Entries().ToList().ForEach(i => i.Reload());
                AppData.AddZayvki_.Reload();

            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }

            TextBoxAdress.Clear();
            TextBoxHotelka.Clear();
            TextBoxklient.Clear();
            TextBoxPrimichania.Clear();
            TextBoxTelNumber.Clear();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            _ = NavigationService.Navigate(null);
        }
    }
}
