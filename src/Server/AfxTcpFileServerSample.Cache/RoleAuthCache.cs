using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AfxTcpFileServerSample.Enums;
using AfxTcpFileServerSample.ICache;

namespace AfxTcpFileServerSample.Cache
{
    public class RoleAuthCache : DataDbCache, IRoleAuthCache
    {
        public virtual List<AuthType> Get(int roleId)
        {
            return this.GetData<List<AuthType>>(roleId);
        }

        public virtual List<AuthType> GetOrSet(int roleId, Func<List<AuthType>> fun)
        {
            var list = this.Get(roleId);
            if(list == null)
            {
                list = fun();
                this.Set(roleId, list);
            }

            return list;
        }

        public virtual void Remove(int roleId)
        {
            this.RemoveKey(roleId);
        }

        public virtual void Set(int roleId, List<AuthType> list)
        {
            this.SetData(list, roleId);
        }
    }
}
