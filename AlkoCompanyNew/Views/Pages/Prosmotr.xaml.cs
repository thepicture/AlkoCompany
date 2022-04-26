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
            DataContext = this;
            AppData.Prosmotr_ = this;
            Reload();
        }

        public void Reload()
        {
            AppData.Context.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            ListViewProsmotr.ItemsSource = AppData.Context.ObjectAssessmentAll.ToList().Where(a =>
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
        }
        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

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
    }
}
