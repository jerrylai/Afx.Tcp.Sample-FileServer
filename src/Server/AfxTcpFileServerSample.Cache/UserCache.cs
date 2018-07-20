using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AfxTcpFileServerSample.Dto.ProtoBuf;
using AfxTcpFileServerSample.ICache;

namespace AfxTcpFileServerSample.Cache
{
    public class UserCache : DataDbCache, IUserCache
    {
        public virtual UserInfoDto Get(int id)
        {
            return this.GetData<UserInfoDto>(id);
        }

        public virtual UserInfoDto GetOrSet(int id, Func<UserInfoDto> fun)
        {
            var list = this.Get(id);
            if (list == null)
            {
                list = fun();
                this.Set(id, list);
            }

            return list;
        }

        public virtual void Remove(int id)
        {
            this.RemoveKey(id);
        }

        public virtual void Set(int id, UserInfoDto value)
        {
            this.SetData(value, id);
        }
    }
}
