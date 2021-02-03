using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using static System.Console;

namespace Spion
{
    class Program
    {
        
        static void Main(string[] args)
        {
            new Listener();
            Application.Run();
        }
        
    }
}