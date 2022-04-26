using AlkoCompanyNew.Models;
using AlkoCompanyNew.Models.Entities;
using System;
using System.Data.Entity;
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
        public Zayavka Zayavka { get; set; }
        public ObjectAssessmentAll OAA { get; set; } = new ObjectAssessmentAll();
        public Prosmotr(ObjectAssessmentAll setteroaa)
        {
            InitializeComponent();
            if (setteroaa != null)
            {
                OAA = setteroaa;
                Zayavka = OAA.Zayavka.First();
            }

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
                RemoveRequestOrphans();
                Reload();
            }

        }
        private void RemoveRequestOrphans()
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    foreach (Klient klient
                       in entities.Klient.ToList())
                    {
                        if (klient.Zayavka.Count == 0)
                        {
                            entities.Entry(
                                entities.Klient.Find(klient.K_ID))
                                .State = EntityState.Deleted;
                        }
                    }
                    foreach (ObjectAssessmentHouse house
                        in entities.ObjectAssessmentHouse.ToList())
                    {
                        if (house.Zayavka.Count == 0)
                        {
                            entities.Entry(
                                entities.ObjectAssessmentHouse.Find(house.OH_ID))
                                .State = EntityState.Deleted;
                        }
                    }
                    foreach (ObjectAssessmentGround ground
                        in entities.ObjectAssessmentGround.ToList())
                    {
                        if (ground.Zayavka.Count == 0)
                        {
                            entities.Entry(
                                entities.ObjectAssessmentGround.Find(ground.OG_ID))
                                .State = EntityState.Deleted;
                        }
                    }
                    foreach (ObjectAssessmentAll objectAll
                        in entities.ObjectAssessmentAll.ToList())
                    {
                        if (objectAll.Zayavka.Count == 0)
                        {
                            entities.Entry(
                                entities.ObjectAssessmentAll.Find(objectAll.OA_ID))
                                .State = EntityState.Deleted;
                        }
                    }
                    entities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось удалить " +
                    "объекты без заявок. " + ex);
            }
        }

    }
}
