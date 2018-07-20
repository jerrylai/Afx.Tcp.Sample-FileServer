using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AfxTcpFileServerSample.Dto;
using AfxTcpFileServerSample.Dto.Repository;
using AfxTcpFileServerSample.Enums;
using AfxTcpFileServerSample.ICache;

namespace AfxTcpFileServerSample.Cache
{
    public class SysConfigCache : DataDbCache, ISysConfigCache
    {
        public virtual List<SysConfigDto> Get(ConfigType type)
        {
            return this.GetData<List<SysConfigDto>>(type);
        }

        public virtual List<SysConfigDto> GetOrSet(ConfigType type, Func<List<SysConfigDto>> fun)
        {
            var list = this.Get(type);
            if (list == null)
            {
                list = fun();
                this.Set(type, list);
            }

            return list;
        }

        public virtual void Remove(ConfigType type)
        {
            this.RemoveKey(type);
        }

        public virtual void Set(ConfigType type, List<SysConfigDto> list)
        {
            this.SetData(list, type);
        }
    }
}
