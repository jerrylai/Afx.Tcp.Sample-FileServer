using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AfxTcpFileServerSample.Dto.ProtoBuf;
using AfxTcpFileServerSample.Enums;

namespace AfxTcpFileServerSample.IRepository
{
    public interface IUpdateInfoRepository : IBaseRepository
    {
        UpdateInfoDto Get(UpdateInfoType type);

        int AddOrUpdate(UpdateInfoDto vm);

        int Delete(UpdateInfoType type);
    }
}
