using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AfxTcpFileServerSample.Enums;

namespace AfxTcpFileServerSample.IRepository
{
    public interface IStatusLockRepository : IBaseRepository
    {
        bool Lock(StatusLockType type, string key, string owner, int timeoutSecond);

        bool Release(StatusLockType type, string key);

        bool IsLock(StatusLockType type, string key);
    }
}
