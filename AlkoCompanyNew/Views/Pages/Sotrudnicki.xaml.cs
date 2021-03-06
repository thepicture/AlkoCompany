using AlkoCompanyNew.Models;
using AlkoCompanyNew.Models.Entities;
using AlkoCompanyNew.Views.Windows;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AlkoCompanyNew.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для Sotrudnicki.xaml
    /// </summary>
    public partial class Sotrudnicki : Page
    {
        private readonly Sotrudnick sotr = new Sotrudnick();
        public Sotrudnicki(Sotrudnick settersotr)
        {
            InitializeComponent();
            if (MainWindow.PublicEmployee.SotrudnickRole.Title == "Администратор")
            {
                OpenMenu.Visibility = Visibility.Visible;
            }
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            if (settersotr != null)
            {
                sotr = settersotr;
            }

            DataContext = sotr;
            AppData.Sotrudnicki_ = this;
            Reload();
        }

        public void Reload()
        {
            AppData.Context.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            ListViewAddSotr.ItemsSource = AppData.Context.Sotrudnick.ToList();
        }
        private void OpenMenu_Click(object sender, RoutedEventArgs e)
        {
            OpenMenu.Visibility = Visibility.Collapsed;
            CloseMenu.Visibility = Visibility.Visible;
        }

        private void CloseMenu_Click(object sender, RoutedEventArgs e)
        {
            OpenMenu.Visibility = Visibility.Visible;
            CloseMenu.Visibility = Visibility.Collapsed;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            
            Frame.Navigate(new AddSotrudnick(null));

        }

        private void OnEditClick(object sender, RoutedEventArgs e)
        {
            if (ListViewAddSotr.SelectedItem is Sotrudnick selectedSotrudnick)
            {
                Frame.Navigate(new AddSotrudnick(selectedSotrudnick));
            }
            else
            {
                MessageBox.Show("Выберите сотрудника в списке для редактирования");
            }
        }

        private void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            if (ListViewAddSotr.SelectedItem is Sotrudnick selectedSotrudnick)
            {
                if (MessageBox.Show("Удалить сотрудника?",
                                   "Вопрос",
                                   MessageBoxButton.YesNo,
                                   MessageBoxImage.Question) != MessageBoxResult.Yes)
                {
                    return;
                }
                AppData.Context.Sotrudnick.Remove(
                    AppData.Context.Sotrudnick.Find(selectedSotrudnick.S_ID));
                AppData.Context.SaveChanges();
                Reload();
            }
            else
            {
                MessageBox.Show("Выберите сотрудника в списке для удаления");
            }
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var poisk = AppData.Context.Sotrudnick.ToList();
            poisk = poisk.Where(d => d.S_RoleId.ToString().ToLower().Contains(TextBoxSearch.Text.ToLower())
            || d.S_Fio.ToString().ToLower().Contains(TextBoxSearch.Text.ToLower())
            || d.S_TelNumber.ToString().ToLower().Contains(TextBoxSearch.Text.ToLower())
            || d.SotrudnickRole.Title.ToString().ToLower().Contains(TextBoxSearch.Text.ToLower())).ToList();
            ListViewAddSotr.ItemsSource = poisk;
        }
    }
}
