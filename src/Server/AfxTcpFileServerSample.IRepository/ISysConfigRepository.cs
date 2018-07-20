using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AfxTcpFileServerSample.Dto.Repository;
using AfxTcpFileServerSample.Enums;

namespace AfxTcpFileServerSample.IRepository
{
    public interface ISysConfigRepository : IBaseRepository
    {
        List<SysConfigDto> GetList(ConfigType type);

        int AddOrUpdate(List<SysConfigDto> list, ConfigType type);

        int Delete(ConfigType type);
    }
}
