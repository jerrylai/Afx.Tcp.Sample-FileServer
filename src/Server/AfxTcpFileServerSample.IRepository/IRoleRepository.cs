using System;
using System.Collections.Generic;
using System.Text;

using AfxTcpFileServerSample.Dto.ProtoBuf;
using AfxTcpFileServerSample.Enums;

namespace AfxTcpFileServerSample.IRepository
{
    public interface IRoleRepository : IBaseRepository
    {
        List<RoleInfoDto> GetList();

        RoleInfoDto Get(int id);

        RoleInfoDto Get(RoleType roleType);

        int Delete(int id);

        int Update(RoleInfoDto vm);

        int Add(RoleInfoDto vm);

        bool Exist(string name);

        int GetUserCount(int id);

        List<RoleInfoDto> GetSyncList(SyncParamDto vm);

        int UpdateSync(List<RoleInfoDto> list);
    }
}
