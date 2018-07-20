using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfxTcpFileServerSample.Common
{
    public class StatusException : Exception
    {
        public int Status { get; private set; }
        public StatusException(int status, string msg) : base(msg)
        {
            this.Status = status;
        }

        public StatusException(int status) : base()
        {
            this.Status = status;
        }
    }
}
