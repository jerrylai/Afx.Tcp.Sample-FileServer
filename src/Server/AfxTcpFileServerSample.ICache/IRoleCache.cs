using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AfxTcpFileServerSample.Dto;
using AfxTcpFileServerSample.Dto.ProtoBuf;

namespace AfxTcpFileServerSample.ICache
{
    public interface IRoleCache : IBaseCache
    {
        List<RoleInfoDto> Get();

        List<RoleInfoDto> GetOrSet(Func<List<RoleInfoDto>> fun);

        void Set(List<RoleInfoDto> list);

        void Remove();
    }
}
