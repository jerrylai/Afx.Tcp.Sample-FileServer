using AfxTcpFileServerSample.Dto.ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AfxTcpFileServerSample.IService
{
    public interface IUserService : IBaseService
    {
        LoginInfoDto Login(LoginParamDto vm);

        void Logout();

        UserInfoDto Get(int id);

        PageListDto<UserInfoDto> GetPageList(UserInfoPageParamDto vm);

        bool UpdatePassword(UpdatePwdDto vm);

        bool Exist(string account);

        void Delete(int id);

        int Add(UserInfoDto vm);

        int Update(UserInfoDto vm);
    }
}
