using AlkoCompanyNew.Models.Entities;
using System.Windows;
using System.Windows.Controls;

namespace AlkoCompanyNew.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для WorkOrdinary.xaml
    /// </summary>
    public partial class WorkOrdinary : Page
    {
        private readonly Zayavka zayavka = new Zayavka();
        public WorkOrdinary(Zayavka setterzayvka)
        {
            InitializeComponent();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            if (setterzayvka != null)
            {
                zayavka = setterzayvka;
            }

            DataContext = zayavka;
        }

        private void ButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpen.Visibility = Visibility.Collapsed;
            ButtonClose.Visibility = Visibility.Visible;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpen.Visibility = Visibility.Visible;
            ButtonClose.Visibility = Visibility.Collapsed;
        }
    }
}
