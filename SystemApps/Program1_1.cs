using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SystemApps
{
    class Program1_1
    {
        static void Main(string[] args)
        {
            int res = ExtFunctions.MessageBox(IntPtr.Zero, "Hello user!!!", "Test message", 0);
            Console.WriteLine("You press: " + res.ToString());
            res = ExtFunctions.MessageBox(IntPtr.Zero, "Hello user!!!", "Test question", ExtFunctions.MB_YESNO | ExtFunctions.MB_ICONQUESTION);
            Console.WriteLine("You press: " + res.ToString());

            Console.Write("\nTest hide window title = ");
            string title = Console.ReadLine();
            IntPtr hwnd = ExtFunctions.FindWindowByCaption(IntPtr.Zero, title);
            if ((hwnd == IntPtr.Zero) || (hwnd == null))
            {
                Console.WriteLine("Window not found");
            }
            else
            {
                ExtFunctions.ShowWindow(hwnd, ExtFunctions.SW_HIDE);
                Console.WriteLine("Press key for show window");
                Console.ReadKey();
                ExtFunctions.ShowWindow(hwnd, ExtFunctions.SW_SHOWMAXIMIZED);
                Console.WriteLine("Press key for show window");
                Console.ReadKey();
                ExtFunctions.ShowWindow(hwnd, ExtFunctions.SW_SHOWMINIMIZED);
                Console.WriteLine("Press key for show window");
                Console.ReadKey();
                ExtFunctions.ShowWindow(hwnd, ExtFunctions.SW_SHOWNORMAL);
                Console.WriteLine("Press key for show window");
                Console.ReadKey();
                ExtFunctions.ShowWindow(hwnd, ExtFunctions.SW_SHOW);
            }

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
