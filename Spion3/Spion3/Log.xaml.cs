using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

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
                //allLogs += "<h1 align=\"center\">" + file.Substring(file.LastIndexOf("\\") + 1) + " </h1>\n";
                var reader = new StreamReader(file);
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    MessageBox.Show(line);
                    if (line.Contains("]"))
                    {
                        string timeFormat = line.Substring(0, line.IndexOf("]") + 1);
                        allLogs += line.Replace(timeFormat, "<h2 style=\"foreground: blue;\">" + timeFormat + "</h2> <h3>");
                        allLogs += "</h3>";
                    }
                }
                reader.Close();
                //File.Delete(file);
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
