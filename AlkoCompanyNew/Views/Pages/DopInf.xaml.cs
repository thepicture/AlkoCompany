using AlkoCompanyNew.Models.Entities;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace AlkoCompanyNew.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для DopInf.xaml
    /// </summary>
    public partial class DopInf : Page
    {
        private Zayavka zayavka = new Zayavka();
        public DopInf(Zayavka setterzayvka)
        {
            InitializeComponent();

            if (setterzayvka != null)
            {
                zayavka = setterzayvka;
            }

            DataContext = zayavka;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            _ = NavigationService.Navigate(null);
        }
    }
}
