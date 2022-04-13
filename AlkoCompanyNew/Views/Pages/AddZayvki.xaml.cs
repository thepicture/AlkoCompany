using AlkoCompanyNew.Models;
using AlkoCompanyNew.Models.Entities;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AlkoCompanyNew.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddZayvki.xaml
    /// </summary>
    public partial class AddZayvki : Page
    {
        private readonly Zayavka zayavka = new Zayavka();
        public AddZayvki(Zayavka setterzayvka)
        {
            InitializeComponent();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            if (setterzayvka != null)
            {
                zayavka = setterzayvka;
            }

            DataContext = zayavka;
            AppData.AddZayvki_ = this;
            Reload();
        }

        public void Reload()
        {
            AppData.Context.ChangeTracker
                .Entries()
                .ToList()
                .ForEach(p =>
                {
                    p.Reload();
                });
            ListViewAddZayvka.ItemsSource = AppData.Context.Zayavka.ToList();
        }
        private void AddZayvki_Click(object sender, RoutedEventArgs e)
        {
            _ = AddZayvkaForm.Navigate(new AddZayvkiForm(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                AppData.Context.ChangeTracker
                    .Entries()
                    .ToList()
                    .ForEach(p =>
                    {
                        p.Reload();
                    });
                ListViewAddZayvka.ItemsSource = AppData.Context.Zayavka.ToList();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Zayavka editingZayavka = (sender as Button).DataContext as Zayavka;
            AddZayvkaForm.Content = new AddZayvkiForm(editingZayavka);
            Reload();
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            Zayavka zay = (sender as Button).DataContext as Zayavka;

            if (MessageBox.Show($"Вы точно хотите удалить элемент {zay.Z_Adress}?",
                                "Внимание",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    _ = AppData.Context.Zayavka.Remove(zay);
                    _ = AppData.Context.SaveChanges();
                    AppData.Context.ChangeTracker.Entries().ToList().ForEach(i => i.Reload());
                    ListViewAddZayvka.ItemsSource = AppData.Context.Zayavka.ToList();
                }
                catch (Exception ex)
                {
                    _ = MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var poisk = AppData.Context.Zayavka.ToList();
            poisk = poisk.Where(d => d.Z_Adress.ToString().ToLower().Contains(TextBoxSearch.Text.ToLower()) || d.Klient.K_Fio.ToString().ToLower().Contains(TextBoxSearch.Text.ToLower()) || d.Z_TelNumber.ToString().ToLower().Contains(TextBoxSearch.Text.ToLower())).ToList();
            ListViewAddZayvka.ItemsSource = poisk;
        }
    }
}