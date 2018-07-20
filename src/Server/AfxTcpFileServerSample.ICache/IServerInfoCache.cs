using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AfxTcpFileServerSample.Dto;
using AfxTcpFileServerSample.Dto.ProtoBuf;

namespace AfxTcpFileServerSample.ICache
{
    public interface IServerInfoCache : IBaseCache
    {
        List<ServerInfoDto> Get();

        List<ServerInfoDto> GetOrSet(Func<List<ServerInfoDto>> fun);

        void Set(List<ServerInfoDto> list);

        void Remove();
    }
}
