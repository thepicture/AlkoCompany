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
            if (MainWindow.PublicEmployee.SotrudnickRole.Title == "Оценщик" || MainWindow.PublicEmployee.SotrudnickRole.Title == "Администратор")
            {
                MessageBox.Show("Вы не можете добавить заявку");
                return;
            }
            _ = Frame.Navigate(new AddZayvki(null));
        }

        private void ElementZayvki_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MainWindow.PublicEmployee.SotrudnickRole.Title == "Секретарь" || MainWindow.PublicEmployee.SotrudnickRole.Title == "Администратор")
            {
                MessageBox.Show("Вы не можете выполнить расчеты!");
                return;
            }
            _ = Frame.Navigate(new Zayavki(null));
        }

        private void ElementZayvkiSotrudnicki_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _ = Frame.Navigate(new Sotrudnicki(null));
        }

        private void ElementZayvkiProsmotr_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _ = Frame.Navigate(new Prosmotr());
        }

        private void ElementBack_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            this.Close();
        }
    }
}
