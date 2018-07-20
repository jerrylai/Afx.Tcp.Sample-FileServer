using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AfxTcpFileServerSample.IService
{
    public interface ISyncInfoService : IBaseService
    {
        bool ReleaseLock();
    }
}
