using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;

namespace SystemApps8_2
{
    class Program
    {
        static IntPtr hook = IntPtr.Zero;
        static IntPtr hookMs = IntPtr.Zero;
        static void Main(string[] args)
        {
            hook = SetHook(HookCallback, WH_KEYBOARD_LL);
            hookMs = SetHook(HookCallbackMS, WH_MOUSE_LL);
            Application.Run();
            UnhookWindowsHookEx(hook);
            UnhookWindowsHookEx(hookMs);
        }

        static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if ((nCode >= 0) && (wParam == (IntPtr)WM_KEYDOWN))
            {
                int vkCode = Marshal.ReadInt32(lParam);
                if (((Keys)vkCode == Keys.LWin) || ((Keys)vkCode == Keys.RWin))
                {
                    Console.WriteLine("{0} blocked!", (Keys)vkCode);
                    return (IntPtr)1;
                }
            }
            return CallNextHookEx(hook, nCode, wParam, lParam);
        }

        static IntPtr HookCallbackMS(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if ((nCode >= 0) && (wParam == (IntPtr)WM_MOUSEMOVE))
            {
                int X = Marshal.ReadInt32(lParam);
                if (X < 400) return (IntPtr)1;
            }
            return CallNextHookEx(hookMs, nCode, wParam, lParam);
        }

        private static IntPtr SetHook(HookProc proc, int whMessage)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(whMessage, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        const int WM_KEYDOWN = 0x0100;
        const int WH_KEYBOARD_LL = 13;
        const int WH_MOUSE_LL = 14;
        const int WM_MOUSEMOVE = 0x0200;

        public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern IntPtr SetWindowsHookEx(int idHook, HookProc hookProc, IntPtr hMod, uint threadId);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern IntPtr CallNextHookEx(IntPtr hHook, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool UnhookWindowsHookEx(IntPtr hHook);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
