using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AfxTcpFileServerSample.Dto.ProtoBuf;
using AfxTcpFileServerSample.ICache;

namespace AfxTcpFileServerSample.Cache
{
    public class UpdateInfoCache : ListDbCache,IUpdateInfoCache
    {
        public virtual List<UpdateInfoDto> Get()
        {
            return this.GetData<List<UpdateInfoDto>>();
        }

        public virtual List<UpdateInfoDto> GetOrSet(Func<List<UpdateInfoDto>> fun)
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

        public virtual void Set(List<UpdateInfoDto> list)
        {
            this.SetData(list);
        }
    }
}
