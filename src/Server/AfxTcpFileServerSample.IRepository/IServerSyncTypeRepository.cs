using AfxTcpFileServerSample.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AfxTcpFileServerSample.IRepository
{
    public interface IServerSyncTypeRepository : IBaseRepository
    {
        List<SyncType> GetList(int serverId);
    }
}
