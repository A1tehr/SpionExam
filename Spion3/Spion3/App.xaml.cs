using System;
using System.Diagnostics;
using System.IO;
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

            StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
            /*

            var args = e.Args;
            if (args.Length != 1)
            {
                MessageBox.Show("Неверный запуск приложения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Current.Shutdown();
                Process.GetCurrentProcess().Kill();
            }
            else
            {
                
                var arg = args[0];
                if (arg == "options") StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
                else if (arg == "spion")
                {
                    Process p = new Process();
                    p.StartInfo.FileName = Directory.GetCurrentDirectory() + "/Spion.exe";
                    p.StartInfo.Arguments = "fsdhjfweohfgdnsvklhstsdfax";
                    p.Start();
                    Current.Shutdown();
                    Process.GetCurrentProcess().Kill();
                }
            }
            */
        }
    }
}

