using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Spion
{
    class Program
    {
        static void Main(string[] args)
        {
            int sleep = 2000;
            Listener l1 = new Listener(0, 55, sleep);
            l1.StartListening();
            Listener l2 = new Listener(56, 110, sleep);
            l2.StartListening();
            Listener l3 = new Listener(111, 156, sleep);
            l3.StartListening();
            Listener l4 = new Listener(157, 200, sleep);
            l4.StartListening();
            Listener l5 = new Listener(201, 256, sleep);
            l5.StartListening();
            
            Thread.Sleep(100000);
        }
        
    }
}