using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AfxTcpFileServerSample.ICache
{
    public interface IUserIdCache : IBaseCache
    {
        int? Get(string account);
        int? GetOrSet(string account, Func<int> fun);
        void Set(string account, int? value);
        void Remove(string account);
    }
}
