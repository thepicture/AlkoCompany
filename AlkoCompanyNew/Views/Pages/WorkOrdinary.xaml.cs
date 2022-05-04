using AlkoCompanyNew.Models.Entities;
using AlkoCompanyNew.Services;
using AlkoCompanyNew.ViewModels;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AlkoCompanyNew.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для WorkOrdinary.xaml
    /// </summary>
    public partial class WorkOrdinary : Page
    {
        public WorkOrdinary(Zayavka zayavka, bool isEditing)
        {
            InitializeComponent();
            App.WorkOrdinary = this;
            DataContext = new WorkViewModel(zayavka, isEditing);

            Loaded += (_, __) =>
            {
                ((dynamic)DataContext).UpdateHousePercentOfCompletion();
                ((dynamic)DataContext).UpdateGroundPercentOfCompletion();
            };
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
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

        private void OnHouseGridToggled(object sender, RoutedEventArgs e)
        {
            if (HouseGrid.Visibility == Visibility.Visible)
            {
                HouseGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                ClientInformationControl.Visibility = Visibility.Collapsed;
                GroundGrid.Visibility = Visibility.Collapsed;
                HouseGrid.Visibility = Visibility.Visible;
            }

        }

        private void OnClientInformationClick(object sender, RoutedEventArgs e)
        {
            if (ClientInformationControl.Visibility == Visibility.Visible)
            {
                ClientInformationControl.Visibility = Visibility.Collapsed;
            }
            else
            {
                HouseGrid.Visibility = Visibility.Collapsed;
                GroundGrid.Visibility = Visibility.Collapsed;
                ClientInformationControl.Visibility = Visibility.Visible;
            }
        }

        private void OnGroundGridToggled(object sender, RoutedEventArgs e)
        {
            if (GroundGrid.Visibility == Visibility.Visible)
            {
                GroundGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                ClientInformationControl.Visibility = Visibility.Collapsed;
                HouseGrid.Visibility = Visibility.Collapsed;
                GroundGrid.Visibility = Visibility.Visible;
            }
        }
    }
}
