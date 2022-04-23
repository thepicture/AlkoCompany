using AlkoCompanyNew.Models;
using AlkoCompanyNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AlkoCompanyNew.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddZayvki.xaml
    /// </summary>
    public partial class AddZayvki : Page
    {
        private Zayavka zayavka = new Zayavka();
        public AddZayvki(Zayavka setterzayvka)
        {
            InitializeComponent();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            if (setterzayvka != null)
            {
                zayavka = setterzayvka;
            }

            DataContext = zayavka;
            AppData.AddZayvki_ = this;
            Reload();
        }

        public void Reload()
        {
            AppData.Context.ChangeTracker
                .Entries()
                .ToList()
                .ForEach(p =>
                {
                    p.Reload();
                });
            ListViewAddZayvka.ItemsSource = AppData.Context.Zayavka.ToList();
        }
        private void AddZayvki_Click(object sender, RoutedEventArgs e)
        {
            AddZayvkaForm.Navigate(new AddZayvkiForm(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                AppData.Context.ChangeTracker
                    .Entries()
                    .ToList()
                    .ForEach(p =>
                    {
                        p.Reload();
                    });
                ListViewAddZayvka.ItemsSource = AppData.Context.Zayavka.ToList();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Zayavka editingZayavka = (sender as Button).DataContext as Zayavka;
            AddZayvkaForm.Content = new AddZayvkiForm(editingZayavka);
            Reload();
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            Zayavka zayavkaToDelete = (sender as Button).DataContext as Zayavka;
            if (MessageBox.Show("Удалить заявку и расчёты?",
                                "Вопрос",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (zayavkaToDelete.ObjectAssessmentHouse is ObjectAssessmentHouse house)
                {
                    if (house.AnalogiHouse.Count > 0)
                    {
                        foreach (AnalogiHouse analogueHouse
                            in house.AnalogiHouse.ToList())
                        {
                            AppData.Context.AnalogiHouse.Remove(
                                AppData.Context.AnalogiHouse.Find(analogueHouse.AH_ID));
                        }
                    }
                    AppData.Context.ObjectAssessmentHouse.Remove(
                        AppData.Context.ObjectAssessmentHouse.Find(house.OH_ID));
                }
                if (zayavkaToDelete.ObjectAssessmentGround is ObjectAssessmentGround ground)
                {
                    if (ground.AnalogiGround.Count > 0)
                    {
                        foreach (AnalogiGround analogueGround
                            in ground.AnalogiGround.ToList())
                        {
                            AppData.Context.AnalogiGround.Remove(
                                AppData.Context.AnalogiGround.Find(analogueGround.AG_ID));
                        }
                    }
                    AppData.Context.ObjectAssessmentGround.Remove(
                        AppData.Context.ObjectAssessmentGround.Find(ground.OG_ID));
                }
                if (zayavkaToDelete.ObjectAssessmentAll is ObjectAssessmentAll objectAll)
                {
                    AppData.Context.ObjectAssessmentAll.Remove(
                        AppData.Context.ObjectAssessmentAll.Find(objectAll.OA_ID));
                }
                AppData.Context.Zayavka.Remove(
                    AppData.Context.Zayavka.Find(zayavkaToDelete.Z_ID));
                AppData.Context.SaveChanges();
                Reload();
                MessageBox.Show("Удалена заявка и её расчёты");
            }
            else if (MessageBox.Show("Удалить только заявку?",
                              "Вопрос",
                              MessageBoxButton.YesNo,
                              MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                AppData.Context.Zayavka.Remove(
                    AppData.Context.Zayavka.Find(zayavkaToDelete.Z_ID));
                AppData.Context.SaveChanges();
                Reload();
                MessageBox.Show("Удалена только заявка");
            }
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Zayavka> poisk = AppData.Context.Zayavka.ToList();
            poisk = poisk.Where(d =>
            {
                return d.Z_Adress.ToLower().Contains(TextBoxSearch.Text.ToLower())
                            || d.Klient.K_Fio.ToLower()
                            .Contains(TextBoxSearch.Text.ToLower())
                            || d.Z_TelNumber.ToLower().Contains(TextBoxSearch.Text.ToLower());
            }).ToList();
            ListViewAddZayvka.ItemsSource = poisk;
        }
    }
}