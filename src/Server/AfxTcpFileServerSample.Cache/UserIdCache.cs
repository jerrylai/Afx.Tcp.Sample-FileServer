using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AfxTcpFileServerSample.ICache;

namespace AfxTcpFileServerSample.Cache
{
    public class UserIdCache : DataDbCache, IUserIdCache
    {
        public virtual int? Get(string account)
        {
            return this.GetData<int?>(account);
        }

        public virtual int? GetOrSet(string account, Func<int> fun)
        {
            var value = this.Get(account);
            if (!value.HasValue)
            {
                value = fun();
                this.Set(account, value);
            }

            return value.Value;
        }

        public virtual void Remove(string account)
        {
            this.RemoveKey(account);
        }

        public virtual void Set(string account, int? value)
        {
            this.SetData(value, account);
        }
    }
}
