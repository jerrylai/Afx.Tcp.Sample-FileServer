using AfxTcpFileServerSample.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AfxTcpFileServerSample.IRepository
{
    public interface IRoleAuthRepository : IBaseRepository
    {
        List<AuthType> GetList(int roleId);
    }
}
