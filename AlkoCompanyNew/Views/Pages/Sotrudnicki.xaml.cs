using AlkoCompanyNew.Models;
using AlkoCompanyNew.Models.Entities;
using System;
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
        private Sotrudnick sotr = new Sotrudnick();
        public Sotrudnicki(Sotrudnick settersotr)
        {
            InitializeComponent();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            if (settersotr != null)
            {
                sotr = settersotr;
            }

            DataContext = sotr;
            AppData.Sotrudnicki_ = this;
       
        }
    }
}
