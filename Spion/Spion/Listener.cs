using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using static System.Console;

namespace Spion
{
    public class Listener
    {
        private string buffer = "";
        public Listener()
        {
            var hook = Hook.GlobalEvents();
            hook.KeyPress += HookOnKeyPress;

            
        }
        private void HookOnKeyPress(object sender, KeyPressEventArgs e)
        {
            char chr = e.KeyChar;
            WriteLine("Нажата кнопка: " + chr);
            if (chr == ' ') buffer = "";
            else
            {
                buffer += e.KeyChar;
                if (buffer == "секрет")
                {
                    MessageBox.Show("Хм.....");
                }
            }
        }
    }
}