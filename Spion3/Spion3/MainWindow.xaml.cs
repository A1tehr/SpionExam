using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        private bool SecretWordsClicked = false;
        private void Button_SecretWordsClick(object sender, RoutedEventArgs e)
        {
            if (!SecretWordsClicked)
            {
                Grid_SecretWords.Visibility = Visibility.Visible;
                var words = (string[])MainWindow.CurrentRegistry.GetValue("Words");
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
            new SecretWord();
        }
    }
}
