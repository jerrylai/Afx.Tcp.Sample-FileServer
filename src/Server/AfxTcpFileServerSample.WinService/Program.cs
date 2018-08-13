using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace AfxTcpFileServerSample.WinService
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            System.IO.Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            IocRegister.Register();
            Common.ConfigUtils.SetThreads();
#if DEBUG
            using (var server = new Controllers.FileServer())
            {
                server.Start();
                using (System.Threading.ManualResetEvent me = new System.Threading.ManualResetEvent(true))
                {
                    me.Reset();
                    me.WaitOne();
                }
            }
#else
            Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new MainService()
            };
            ServiceBase.Run(ServicesToRun);
#endif
        }
    }
}
