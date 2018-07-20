using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AfxTcpFileServerSample.Dto.ProtoBuf;

namespace Client.IService
{
    public interface IRoleService:IBaseService
    {
        List<RoleInfoDto> GetList(RoleInfoListParamDto vm);
    }
}
