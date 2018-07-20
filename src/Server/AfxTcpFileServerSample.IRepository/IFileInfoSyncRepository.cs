using AfxTcpFileServerSample.Dto.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace AfxTcpFileServerSample.IRepository
{
    public interface IFileInfoSyncRepository : IBaseRepository
    {
        FileInfoSyncDto Get(int serverId);

        int Update(FileInfoSyncDto vm);
    }
}
