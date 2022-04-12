using AlkoCompanyNew.Views.Pages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AlkoCompanyNew.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для BaseWindowSotrudnick.xaml
    /// </summary>
    public partial class BaseWindowSotrudnick : Window
    {
        public BaseWindowSotrudnick()
        {
            InitializeComponent();
            Hi.Text += $", {MainWindow.fio}";
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
        }

        private void ListButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            ListButtonOpen.Visibility = Visibility.Collapsed;
            ListButtonClose.Visibility = Visibility.Visible;
        }

        private void ListButtonClose_Click(object sender, RoutedEventArgs e)
        {
            ListButtonOpen.Visibility = Visibility.Visible;
            ListButtonClose.Visibility = Visibility.Collapsed;
        }

        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _ = Frame.Navigate(new AddZayvki(null));
        }

        private void ElementZayvki_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _ = Frame.Navigate(new Zayavki(null));
        }
    }
}
