using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AfxTcpFileServerSample.Enums;
using AfxTcpFileServerSample.ICache;

namespace AfxTcpFileServerSample.Cache
{
    public class ServerSyncTypeCache : DataDbCache, IServerSyncTypeCache
    {
        public virtual List<SyncType> Get(int serverId)
        {
            return this.GetData<List<SyncType>>(serverId);
        }

        public virtual List<SyncType> GetOrSet(int serverId, Func<List<SyncType>> fun)
        {
            var list = this.Get(serverId);
            if (list == null)
            {
                list = fun();
                this.Set(serverId, list);
            }

            return list;
        }

        public virtual void Remove(int serverId)
        {
            this.RemoveKey(serverId);
        }

        public virtual void Set(int serverId, List<SyncType> list)
        {
            this.SetData(list, serverId);
        }
    }
}
