using AlkoCompanyNew.Models.Entities;
using AlkoCompanyNew.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AlkoCompanyNew.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для WorkOrdinary.xaml
    /// </summary>
    public partial class WorkOrdinary : Page
    {
        public WorkOrdinary(Zayavka zayavka)
        {
            InitializeComponent();
            App.WorkOrdinary = this;
            DataContext = new WorkViewModel(zayavka);
            Loaded += (_, __) =>
            {
                ((dynamic)DataContext).UpdatePercentOfCompletion();
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

        private void OnCalculatingClick(object sender, RoutedEventArgs e)
        {
            if (CalculatingControl.Visibility == Visibility.Visible)
            {
                CalculatingControl.Visibility = Visibility.Collapsed;
            }
            else
            {
                ClientInformationControl.Visibility = Visibility.Collapsed;
                CalculatingControl.Visibility = Visibility.Visible;
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
                CalculatingControl.Visibility = Visibility.Collapsed;
                ClientInformationControl.Visibility = Visibility.Visible;
            }
        }
    }
}
