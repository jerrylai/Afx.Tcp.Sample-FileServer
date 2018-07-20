using AfxTcpFileServerSample.Dto.ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AfxTcpFileServerSample.IRepository
{
    public interface IUserRepository : IBaseRepository
    {
        UserInfoDto Get(int id);

        int GetId(string account);

        int Add(UserInfoDto vm);

        int Update(UserInfoDto vm);

        int Delete(int id);

        int UpdatePassword(int id, string password);

        PageListDto<UserInfoDto> GetPageList(UserInfoPageParamDto vm);

        List<UserInfoDto> GetSyncList(SyncParamDto vm);

        int UpdateSync(List<UserInfoDto> list);
    }
}
