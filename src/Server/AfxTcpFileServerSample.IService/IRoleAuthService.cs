using AfxTcpFileServerSample.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AfxTcpFileServerSample.IService
{
    public interface IRoleAuthService : IBaseService
    {
        List<AuthType> GetAuthList(int roleId);

        bool CheckRole(int roleId, List<AuthType> auths);
    }
}
