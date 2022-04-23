using AlkoCompanyNew.Models;
using AlkoCompanyNew.Views.Pages;
using System;
using System.Diagnostics;
using System.Windows;

namespace AlkoCompanyNew
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static bool IsLoginOnStartup = true;
        public static WorkOrdinary WorkOrdinary { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AppDomain.CurrentDomain.ProcessExit += (_, __) =>
            {
                AppData.Context.Dispose();
            };
            FrameworkCompatibilityPreferences.KeepTextBoxDisplaySynchronizedWithTextProperty = false;
        }
    }
}
