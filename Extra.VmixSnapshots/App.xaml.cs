using Common.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Extra.VmixSnapshots
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static string name = System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.Name;

        public App()
        {
            //InitializeComponent();

            // Thanks to Marlon Grech for the great idea of elevating the app's
            // processing priority, so that episode playback is not interrupted 
            // when the CPU is undergoing heavy loads.
            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.High;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            logger.Info("Application starting.");

            Application.Current.DispatcherUnhandledException += (sender, args) =>
            {
                args.Handled = true;
                logger.Error(args.Exception);

                //MessageBox.Show(args.Exception.Message, "Oops...an error has occurred", MessageBoxButton.OK, MessageBoxImage.Error);
                App.Current.Shutdown(-1);
            };

            AppDomain.CurrentDomain.UnhandledException += (s, a) =>
            {
                Exception ex = a.ExceptionObject as Exception;
                logger.Error(ex);

                //MessageBox.Show(ex.Message, "Oops...an error has occurred", MessageBoxButton.OK, MessageBoxImage.Error);
                App.Current.Shutdown(-1);
            };

            TaskScheduler.UnobservedTaskException += (s, a) =>
            {
                logger.Error(a.Exception);

                App.Current.Shutdown(-1);
            };

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            logger.Info("Application ending.");
            base.OnExit(e);
        }
    }
}
