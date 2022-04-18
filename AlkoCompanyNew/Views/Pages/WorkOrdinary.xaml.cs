using AlkoCompanyNew.Models.Entities;
using AlkoCompanyNew.Services;
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
            foreach (ComboBox comboBox 
                in LogicalChildrenFinder.Find<ComboBox>(CalculatingControl))
            {
                comboBox.SelectionChanged += (o, e) =>
                {
                    ((dynamic)DataContext).PerformChangeContext();
                };
            }
            Loaded += (o, e) =>
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
                CalculatingControl.Visibility = Visibility.Visible;
            }
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ((dynamic)DataContext).PerformChangeContext();
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            ((dynamic)DataContext).PerformChangeContext();
        }
    }
}
