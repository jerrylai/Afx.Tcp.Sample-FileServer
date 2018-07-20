using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AfxTcpFileServerSample.Dto.ProtoBuf;

namespace Client.IService
{
    public interface IUserService : IBaseService
    {
        bool UpdatePwd(string oldPwd, string newPwd);

        PageListDto<UserInfoDto> GetPageList(UserInfoPageParamDto vm);
    }
}
