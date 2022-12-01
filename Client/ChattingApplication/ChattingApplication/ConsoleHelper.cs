using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace ChattingApplication
{
    internal class ConsoleHelper
    {
        [DllImport("kernel32")]
        public static extern void AllocConsole();
        [DllImport("kernel32")]
        public static extern void FreeConsole();

        public static void ShowDebugConsole()
        {
            AllocConsole();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Title = "ChattingApp Debug Console";
            Console.WriteLine("[ChattingApp Console] Console successfully constructed!");
        }
    }
}
