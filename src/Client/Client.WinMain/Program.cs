using Client.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Client.WinMain
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.IO.Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            IocRegister.Register();
            bool createdNew = false;
            using (Mutex mutex = new Mutex(true, "Client.WinMain", out createdNew))
            {
                if (createdNew)
                {
                    Application.ThreadException += Application_ThreadException;
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new MainForm());
                }
                else
                {
                    try
                    {
                        string path = WinAPIs.GetHandleFile();
                        if (System.IO.File.Exists(path))
                        {
                            string s = System.IO.File.ReadAllText(path, System.Text.Encoding.UTF8) ?? "";
                            int h = 0;
                            if (!string.IsNullOrEmpty(s) && int.TryParse(s, out h) && h > 0)
                            {
                                IntPtr hwnd = new IntPtr(h);
                                WinAPIs.PostMessage(hwnd, WinAPIs.WM_OPEN_FORM, 0, 0);
                            }
                        }
                    }
                    catch { }
                }
            }
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            while(ex != null)
            {
                LogUtils.Error("【ThreadException】", ex);
                ex = ex.InnerException;
            }
        }
    }
}
