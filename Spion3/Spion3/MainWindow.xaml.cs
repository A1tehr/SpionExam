using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace Spion3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static RegistryKey CurrentRegistry;
        public static MainWindow Instance { private set; get; }
        public MainWindow()
        {
            Instance = this;
            CurrentRegistry = Registry.CurrentUser.CreateSubKey("KeySpion");
            InitializeComponent();

            var procceses = Process.GetProcessesByName("Spion");
            if(procceses.Length > 0)
            {
                Button_StartSpion.Visibility = Visibility.Collapsed;
                Button_CloseSpion.Visibility = Visibility.Visible;
            } else
            {
                Button_StartSpion.Visibility = Visibility.Visible;
                Button_CloseSpion.Visibility = Visibility.Collapsed;
            }
        }

        private bool SecretWordsClicked = false;
        private void Button_SecretWordsClick(object sender, RoutedEventArgs e)
        {
            if (!SecretWordsClicked)
            {
                if (LogClicked)
                {
                    Grid_Logs.Visibility = Visibility.Collapsed;
                    LogClicked = false;
                }
                Grid_SecretWords.Visibility = Visibility.Visible;
                var words = (string[])CurrentRegistry.GetValue("Words");
                if(words != null)
                {
                    for (int i = 0; i < words.Length; i++)
                    {
                        new SecretWord(i, words[i]);
                    }
                }
                
                SecretWordsClicked = true;
            } else
            {
                StackPanel_WordList.Children.Clear();
                Grid_SecretWords.Visibility = Visibility.Collapsed;
                SecretWordsClicked = false;
            }
        }

        private void Button_AddWordClick(object sender, RoutedEventArgs e)
        {
            if (SecretWord.CanAddWord)
            {
                new SecretWord();
            } else
            {
                MessageBox.Show("Закончите редактирование секретного слова", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        private bool LogClicked = false;
        private void Button_LogClick(object sender, RoutedEventArgs e)
        {
            if (!LogClicked)
            {
                if (SecretWordsClicked)
                {
                    Grid_SecretWords.Visibility = Visibility.Collapsed;
                    SecretWordsClicked = false;
                } 
                    
                Grid_Logs.Visibility = Visibility.Visible;
                try
                {
                    foreach (var file in Directory.GetFiles(App.HTML))
                    {
                        new Log(file.Substring(file.LastIndexOf('\\') + 1));
                    }
                    LogClicked = true;

                } catch (Exception e1)
                {
                    MessageBox.Show(e1.Message);
                }
            }
            else
            {
                StackPanel_Logs.Children.Clear();
                Grid_Logs.Visibility = Visibility.Collapsed;
                LogClicked = false;
            }
        }

        private void Button_CreateLogClick(object sender, RoutedEventArgs e)
        {
            if(Directory.GetFiles(App.Temp).Length == 0)
            {
                MessageBox.Show("Не из чего создавать логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            } else
            {
                new Log();
            }
        }

        private void Button_StartSpionClick(object sender, RoutedEventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = Directory.GetCurrentDirectory() + "/Spion.exe";
            p.StartInfo.Arguments = "fsdhjfweohfgdnsvklhstsdfax";
            p.Start();
            Button_StartSpion.Visibility = Visibility.Collapsed;
            Button_CloseSpion.Visibility = Visibility.Visible;
        }

        private void Button_CloseSpionClick(object sender, RoutedEventArgs e)
        {
            var procces = Process.GetProcessesByName("Spion");
            foreach (var proc in procces)
            {
                proc.Kill();
            }
            Button_StartSpion.Visibility = Visibility.Visible;
            Button_CloseSpion.Visibility = Visibility.Collapsed;
        }
    }
}
