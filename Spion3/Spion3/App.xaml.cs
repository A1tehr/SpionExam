using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Spion3
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            /*
            var args = e.Args;
            if (args.Length != 1)
            {
                MessageBox.Show("Ошибка", "Неверный запуск приложения", MessageBoxButton.OK, MessageBoxImage.Error);
                Current.Shutdown();
            }
            else
            {
                var arg = args[0];
                if (arg == "options") StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
            }
            */
            StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
        }
    }
}
