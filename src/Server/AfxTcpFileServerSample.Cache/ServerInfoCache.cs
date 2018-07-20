using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AfxTcpFileServerSample.Dto.ProtoBuf;
using AfxTcpFileServerSample.ICache;

namespace AfxTcpFileServerSample.Cache
{
    public class ServerInfoCache : ListDbCache, IServerInfoCache
    {
        public virtual List<ServerInfoDto> Get()
        {
            return this.GetData<List<ServerInfoDto>>();
        }

        public virtual List<ServerInfoDto> GetOrSet(Func<List<ServerInfoDto>> fun)
        {
            var list = this.Get();
            if (list == null)
            {
                list = fun();
                this.Set(list);
            }

            return list;
        }

        public virtual void Remove()
        {
            this.RemoveKey();
        }

        public virtual void Set(List<ServerInfoDto> list)
        {
            this.SetData(list);
        }
    }
}
