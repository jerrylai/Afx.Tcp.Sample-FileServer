using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Client.WinMain
{
    public static class WinAPIs
    {
        private const int WM_USER = 0x0400;
        public const int WM_OPEN_FORM = WM_USER + 1;
        public const int WM_CLOSE_SYSTEM = WM_USER + 2;

        public const string HandleFile = "hwnd.s";

        public static string GetHandleFile()
        {
            return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, HandleFile);
        }

        [DllImport("user32.dll")]
        public static extern int PostMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
    }
}
