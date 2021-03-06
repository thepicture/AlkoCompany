using AlkoCompanyNew.Models;
using AlkoCompanyNew.Models.Entities;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AlkoCompanyNew.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для Prosmotr.xaml
    /// </summary>
    public partial class Prosmotr : Page
    {

        public Prosmotr()
        {
            InitializeComponent();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            DataContext = this;
            AppData.Prosmotr_ = this;
            Reload();
        }

        public void Reload()
        {
            AppData.Context.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            var poisk = AppData.Context.ObjectAssessmentAll.ToList().Where(a=>
            {
                return a.OA_PloshadZemli != null
                && a.OA_PloshadDom != null
                && a.OA_CenaAll != null
                && a.OA_CenaZemliKvm != null
                && a.OA_CenaDomVse != null
                && a.OA_CenaZemliVse != null
                && a.OA_CenaDomVse != null
                && a.OA_CenaDomKvm != null
                && a.OA_CenaAllKvm != null;
            });
            poisk = poisk.Where(d => d.OA_Adress.ToString().ToLower().Contains(TextBoxSearch.Text.ToLower())).ToList();
            ListViewProsmotr.ItemsSource = poisk;
        }
        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Reload();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility.Visible == Visibility)
            {
                if (App.IsRemoveRequestOrphans)
                {
                    App.TableGarbageCollector.Collect();
                }
                Reload();
            }
        }

        private void ProsmotrR_Click(object sender, RoutedEventArgs e)
        {
            WorkOrdinary currentWorkOrdinary = new WorkOrdinary(
                ((sender as Button).DataContext as ObjectAssessmentAll).Zayavka.ToList().First(), false);
            NavigationService.Navigate(currentWorkOrdinary);
            Reload();
        }
    }
}
