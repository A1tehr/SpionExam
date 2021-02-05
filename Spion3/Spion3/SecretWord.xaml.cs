using Microsoft.Win32;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Spion3
{
    /// <summary>
    /// Логика взаимодействия для SecretWord.xaml
    /// </summary>
    public partial class SecretWord : UserControl
    {
        public static bool CanAddWord { get
            {
                foreach(var word in MainWindow.Instance.StackPanel_WordList.Children)
                {
                    if ((word as SecretWord).Edit) return false;
                }
                return true;
            }
        }
        public int Index { private set; get; }
        private string _word;
        public string Word {
            set
            {
                TextBox_Word.Text = value;
                _word = value;
            }
            get
            {
                return _word;
            }
        }

        private bool Edit { set; get; }

        public SecretWord()
        {
            InitializeComponent();
            EditText();
            Index = MainWindow.Instance.StackPanel_WordList.Children.Count;
            MainWindow.Instance.StackPanel_WordList.Children.Add(this);
        }

        public SecretWord(int index, string word)
        {
            InitializeComponent();
            Index = index;
            Word = word;
            Edit = false;
            MainWindow.Instance.StackPanel_WordList.Children.Add(this);
        }

        private void Button_DeleteClick(object sender, RoutedEventArgs e)
        {
            var words = (string[])MainWindow.CurrentRegistry.GetValue("Words");
            var newWords = new List<string>();
            for(int i = 0; i < words.Length; i++)
            {
                if (Index != i) newWords.Add(words[i]);
            }
            MainWindow.CurrentRegistry.SetValue("Words", newWords.ToArray());
            for(int i = Index + 1; i < MainWindow.Instance.StackPanel_WordList.Children.Count; i++)
            {
                (MainWindow.Instance.StackPanel_WordList.Children[i] as SecretWord).Index--;
            }

            MainWindow.Instance.StackPanel_WordList.Children.Remove(this);
        }

        private void Button_EditClick(object sender, RoutedEventArgs e)
        {
            if (Edit)
            {
                Edit = false;
                TextBox_Word.IsEnabled = false;
                Word = TextBox_Word.Text.Trim();
                var words = (string[])MainWindow.CurrentRegistry.GetValue("Words");
                if(words != null)
                {
                    if(Index <= words.Length - 1)
                    {
                        words[Index] = Word;
                    } else
                    {
                        var temp = new string[words.Length + 1];
                        for(int i = 0; i < words.Length; i++)
                        {
                            temp[i] = words[i];
                        }
                        temp[words.Length] = Word;
                        words = new string[temp.Length];
                        words = temp;
                    }
                    
                    MainWindow.CurrentRegistry.SetValue("Words", words, RegistryValueKind.MultiString);
                } else
                {
                    MainWindow.CurrentRegistry.SetValue("Words", new string[] { Word }, RegistryValueKind.MultiString);
                }
                
                Grid_Edit.Background = Brushes.Transparent;
            } else
            {
                EditText();   
            }
        }
        private void EditText()
        {
            Edit = true;
            TextBox_Word.IsEnabled = true;
            TextBox_Word.Focus();
            Grid_Edit.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF05D350");
        }
    }
}
