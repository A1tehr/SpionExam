using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
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
        public static readonly string PathConf = Directory.GetCurrentDirectory() + "/logs";
        public static readonly string HTML = PathConf + "/html";
        public static readonly string Temp = PathConf + "/temp";


        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (!Directory.Exists(PathConf)) Directory.CreateDirectory(PathConf);
            if (!Directory.Exists(HTML)) Directory.CreateDirectory(HTML);
            if (!Directory.Exists(Temp)) Directory.CreateDirectory(Temp);
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
