using AfxTcpFileServerSample.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AfxTcpFileServerSample.ICache
{
    public interface IServerSyncTypeCache : IBaseCache
    {
        List<SyncType> Get(int serverId);
        List<SyncType> GetOrSet(int serverId, Func<List<SyncType>> fun);
        void Set(int serverId, List<SyncType> list);
        void Remove(int serverId);
    }
}
