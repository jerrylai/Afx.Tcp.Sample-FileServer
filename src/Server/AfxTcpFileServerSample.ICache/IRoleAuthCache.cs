using AfxTcpFileServerSample.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AfxTcpFileServerSample.ICache
{
    public interface IRoleAuthCache : IBaseCache
    {
        List<AuthType> Get(int roleId);
        List<AuthType> GetOrSet(int roleId, Func<List<AuthType>> fun);
        void Set(int roleId, List<AuthType> list);
        void Remove(int roleId);
    }
}
