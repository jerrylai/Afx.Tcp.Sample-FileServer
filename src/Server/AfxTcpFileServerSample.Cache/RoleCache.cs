using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AfxTcpFileServerSample.Dto;
using AfxTcpFileServerSample.Dto.ProtoBuf;
using AfxTcpFileServerSample.ICache;

namespace AfxTcpFileServerSample.Cache
{
    public class RoleCache : ListDbCache, IRoleCache
    {
        public virtual List<RoleInfoDto> Get()
        {
            return this.GetData<List<RoleInfoDto>>();
        }

        public virtual List<RoleInfoDto> GetOrSet(Func<List<RoleInfoDto>> fun)
        {
            var list = this.Get();
            if(list == null)
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

        public virtual void Set(List<RoleInfoDto> list)
        {
            this.SetData(list);
        }
    }
}
