using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AfxTcpFileServerSample.Dto;
using AfxTcpFileServerSample.Dto.Repository;
using AfxTcpFileServerSample.Enums;

namespace AfxTcpFileServerSample.ICache
{
    public interface ISysConfigCache : IBaseCache
    {
        List<SysConfigDto> Get(ConfigType type);
        List<SysConfigDto> GetOrSet(ConfigType type, Func<List<SysConfigDto>> fun);
        void Set(ConfigType type, List<SysConfigDto> list);
        void Remove(ConfigType type);
    }
}
