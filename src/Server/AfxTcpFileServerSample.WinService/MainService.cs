using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

using AfxTcpFileServerSample.Controllers;

namespace AfxTcpFileServerSample.WinService
{
    public partial class MainService : ServiceBase
    {
        private FileServer server = null;
        public MainService()
        {
            InitializeComponent();
            this.server = new FileServer();
        }

        protected override void OnStart(string[] args)
        {
            this.server.Start();
        }

        protected override void OnStop()
        {
            this.server.Stop();
        }
    }
}
