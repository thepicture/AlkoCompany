using AlkoCompanyNew.Models;
using AlkoCompanyNew.Models.Entities;
using AlkoCompanyNew.Models.Enums;
using Microsoft.Win32;
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
    public partial class AddZayvkiForm : Page
    {
        public Zayavka Zayavka { get; set; } = new Zayavka();
        public Klient CurrentClient { get; set; } = new Klient();
        public ObservableCollection<Sotrudnick> Sotrudnicks { get; set; }

        public AddZayvkiForm(Zayavka setterzayvka)
        {
            InitializeComponent();
            DataContext = this;
            ComboBoxSotrudnick.ItemsSource = new ObservableCollection<Sotrudnick>(
                AppData.Context.Sotrudnick.ToList());
            if (setterzayvka != null)
            {
                Zayavka = setterzayvka;
                ComboBoxSotrudnick.SelectedItem = ComboBoxSotrudnick.Items
                    .Cast<Sotrudnick>()
                    .First(s => s.S_ID == Zayavka.S_ID);
                CurrentClient = setterzayvka.Klient;
            }

        }

        private void Grid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog
            {
                Filter = "Image files (*.JPG, *.PNG)| *jpg; *.png;",

            };
            if (file.ShowDialog() == true)
            {
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
            else if (string.IsNullOrEmpty(CurrentClient.K_Fio))
            {
                MessageBox.Show("Введите ФИО клиента");
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

            Zayavka.S_ID = (ComboBoxSotrudnick.SelectedItem as Sotrudnick).S_ID;
            Zayavka.Z_StatusId = ZayavkaStatuses.V_Obrabotke;

            if (Zayavka.Z_ID == 0)
            {
                try
                {
                    CurrentClient = AppData.Context.Klient.Add(CurrentClient);
                    Zayavka.K_ID = CurrentClient.K_ID;
                    AppData.Context.Zayavka.Add(Zayavka);
                    AppData.Context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при сохранении клиента. " +
                        "Подробная информация будет показана далее");
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                Zayavka.Klient.K_Fio = CurrentClient.K_Fio;
            }
            try
            {
                AppData.Context.Entry(AppData.Context.Zayavka.Find(Zayavka.Z_ID))
                    .CurrentValues.SetValues(Zayavka);
                AppData.Context.SaveChanges();
                MessageBox.Show("Данные успешно внесены");
                AppData.Context.ChangeTracker
                    .Entries()
                    .ToList()
                    .ForEach(entry =>
                    {
                        entry.Reload();
                    });
                AppData.Context.ChangeTracker
                    .Entries()
                    .ToList()
                    .ForEach(i =>
                    {
                        i.Reload();
                    });
                AppData.AddZayvki_.Reload();

            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }

            TextBoxAdress.Clear();
            TextBoxFIO.Clear();
            TextBoxHotelka.Clear();
            TextBoxPrimichania.Clear();
            TextBoxTelNumber.Clear();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(null);
        }

        
    }
}
