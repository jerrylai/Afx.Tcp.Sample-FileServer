using AfxTcpFileServerSample.Dto.ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AfxTcpFileServerSample.ICache
{
    public interface IUpdateInfoCache : IBaseCache
    {
        List<UpdateInfoDto> Get();

        List<UpdateInfoDto> GetOrSet(Func<List<UpdateInfoDto>> fun);

        void Set(List<UpdateInfoDto> list);

        void Remove();
    }
}
