using AfxTcpFileServerSample.Dto.ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AfxTcpFileServerSample.IService
{
    public interface IRoleService : IBaseService
    {
        RoleInfoDto Get(int id);

        List<RoleInfoDto> GetList(RoleInfoListParamDto vm);

        int Update(RoleInfoDto vm);

        int Add(RoleInfoDto vm);

        int GetUserCount(int id);

        bool Exist(string name);

        int Delete(int id);
    }
}
