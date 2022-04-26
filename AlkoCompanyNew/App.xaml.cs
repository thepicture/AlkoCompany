using AlkoCompanyNew.Models;
using AlkoCompanyNew.Services;
using AlkoCompanyNew.Views.Pages;
using System;
using System.Windows;

namespace AlkoCompanyNew
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Устанавливает состояние, характеризующее, 
        /// следует ли авторизоваться автоматически при 
        /// запуске приложения.
        /// </summary>
        public static bool IsLoginOnStartup = false;
        /// <summary>
        /// Устанавливает состояние, характеризующее, следует ли 
        /// обнаруживать и удалять объекты оценки и аналоги, 
        /// не связанные с заявками.
        /// </summary>
        public static bool IsRemoveRequestOrphans = true;
        public static WorkOrdinary WorkOrdinary { get; set; }
        public static IGarbageCollector TableGarbageCollector = new TableGarbageCollector();

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
