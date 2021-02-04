using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Spion3
{
    /// <summary>
    /// Логика взаимодействия для Log.xaml
    /// </summary>
    public partial class Log : UserControl
    {
        public string PathFile { private set; get; }
        public string LogName { private set; get; }

        public Log()
        {
            InitializeComponent();
            MainWindow.Instance.StackPanel_Logs.Children.Add(this);
            TextBox_NameLog.IsEnabled = true;
            TextBox_NameLog.Focus();

        }
        public Log(string nameOfFile)
        {
            InitializeComponent();
            MainWindow.Instance.StackPanel_Logs.Children.Add(this);
            LogName = nameOfFile.Substring(0, nameOfFile.LastIndexOf("."));
            PathFile = App.HTML + "/" + nameOfFile;
            Grid_Save.Visibility = Visibility.Collapsed;
            Grid_OpenHTML.Visibility = Visibility.Visible;
            TextBox_NameLog.Text = LogName;
        }

        private void Button_DeleteClick(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.StackPanel_Logs.Children.Remove(this);
            File.Delete(PathFile);
        }

        private void Button_OpenLogClick(object sender, RoutedEventArgs e)
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(PathFile)
            {
                UseShellExecute = true
            };
            p.Start();
        }

        private void Button_SaveLogClick(object sender, RoutedEventArgs e)
        {
            TextBox_NameLog.IsEnabled = false;
            LogName = TextBox_NameLog.Text;
            string allLogs = "";
            foreach(var file in Directory.GetFiles(App.Temp))
            {
                allLogs += File.ReadAllText(file);
                File.Delete(file);
            }
            PathFile = App.HTML + "/" + LogName + ".html";
            File.Create(PathFile).Close();
            StreamWriter streamWriter = new StreamWriter(PathFile, true);
            streamWriter.WriteAsync(allLogs);
            streamWriter.Close();
            Grid_Save.Visibility = Visibility.Collapsed;
            Grid_OpenHTML.Visibility = Visibility.Visible;
        }
    }
}
