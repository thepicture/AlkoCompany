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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace AlkoCompanyNew.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddZayvkiForm.xaml
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public partial class AddZayvkiForm : Page
    {
        public Zayavka Zayavka { get; set; } = new Zayavka();
        public Klient Kl { get; set; } = new Klient();
        public ObservableCollection<Sotrudnick> Sotrudnicks { get; set; }
        public ObservableCollection<Klient> Clients { get; set; }

        public AddZayvkiForm(Zayavka setterzayvka)
        {
            InitializeComponent();
            DataContext = this;
            ComboBoxSotrudnick.ItemsSource = new ObservableCollection<Sotrudnick>(
                AppData.Context.Sotrudnick.ToList());
            ComboBoxClient.ItemsSource = new ObservableCollection<Klient>(
                AppData.Context.Klient.ToList());
            if (setterzayvka != null)
            {
                Zayavka = setterzayvka;
                ComboBoxSotrudnick.SelectedItem = ComboBoxSotrudnick.Items
                    .Cast<Sotrudnick>()
                    .First(s => s.S_ID == Zayavka.S_ID);
                ComboBoxClient.SelectedItem = ComboBoxClient.Items
                    .Cast<Klient>()
                    .First(c => c.K_ID == Zayavka.K_ID);
            }

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
                Zayavka.Z_PhotoPreview = File.ReadAllBytes(file.FileName);
            }
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
            string tel = TextBoxTelNumber.Text;
            if (string.IsNullOrEmpty(adress))
            {
                MessageBox.Show("Введите адрес");
                return;
            }
            else if (ComboBoxClient.SelectedItem == null)
            {
                MessageBox.Show("Укажите клиента");
                return;
            }
            else if (ComboBoxSotrudnick.SelectedItem == null)
            {
                MessageBox.Show("Укажите сотрудника");
                return;
            }
            else if (string.IsNullOrEmpty(tel))
            {
                MessageBox.Show("Введите номер телефона клиента");
                return;
            }

            Zayavka.K_ID = (ComboBoxClient.SelectedItem as Klient).K_ID;
            Zayavka.S_ID = (ComboBoxSotrudnick.SelectedItem as Sotrudnick).S_ID;
            Zayavka.Z_StatusId = ZayavkaStatuses.V_Obrabotke;

            if (Zayavka.Z_ID == 0)
            {

                _ = AppData.Context.Zayavka.Add(Zayavka);
            }
            try
            {
                AppData.Context.Entry(AppData.Context.Zayavka.Find(Zayavka.Z_ID)).CurrentValues.SetValues(Zayavka);
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
            TextBoxPrimichania.Clear();
            TextBoxTelNumber.Clear();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            _ = NavigationService.Navigate(null);
        }
    }
}
