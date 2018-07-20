using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AfxTcpFileServerSample.Dto.ProtoBuf;

namespace AfxTcpFileServerSample.ICache
{
    public interface IUserCache : IBaseCache
    {
        UserInfoDto Get(int id);
        UserInfoDto GetOrSet(int id, Func<UserInfoDto> fun);
        void Set(int id, UserInfoDto value);
        void Remove(int id);
    }
}
