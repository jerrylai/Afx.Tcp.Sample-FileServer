using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AfxTcpFileServerSample.Enums
{
    public enum CheckFileInfoStatus
    {
       None = 0,
       RestCheckStatus = 100,
       ReadRootPath = 200,
       ReadPathInfo = 300,
       Completed = 1000
    }
}
