using AlkoCompanyNew.Models;
using AlkoCompanyNew.Models.Entities;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AlkoCompanyNew.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для Zayavki.xaml
    /// </summary>
    public partial class Zayavki : Page
    {
        private Zayavka zayavka = new Zayavka();
        public Zayavki(Zayavka setterzayvka)
        {
            InitializeComponent();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            if (setterzayvka != null)
            {
                zayavka = setterzayvka;
            }

            DataContext = zayavka;
            AppData.Zayvki_ = this;
            Reload();
        }
        public void Reload()
        {
            AppData.Context.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            ListViewAddZayvka.ItemsSource = AppData.Context.Zayavka
                .ToList()
                .OrderBy(z => z.Z_StatusId);
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            if (Visibility == Visibility.Visible)
            {
                AppData.Context.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                ListViewAddZayvka.ItemsSource = AppData.Context.Zayavka
                    .ToList()
                    .OrderBy(z => z.Z_StatusId);
            }

        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var poisk = AppData.Context.Zayavka.ToList();
            poisk = poisk.Where(d => d.Z_Adress.ToString().ToLower().Contains(TextBoxSearch.Text.ToLower())
            || d.Klient.K_Fio.ToString().ToLower().Contains(TextBoxSearch.Text.ToLower())
            || d.Z_TelNumber.ToString().ToLower().Contains(TextBoxSearch.Text.ToLower())).ToList();
            ListViewAddZayvka.ItemsSource = poisk.OrderBy(z => z.Z_StatusId);
        }

        private void PodrobnyProsmotr_Click(object sender, RoutedEventArgs e)
        {
            AddZayvkaForm.Content = new DopInf((sender as Button).DataContext as Zayavka);
            Reload();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {


            AddZayvkaForm.Content = new WorkOrdinary((sender as Button).DataContext as Zayavka);
            Reload();
        }
    }
}
