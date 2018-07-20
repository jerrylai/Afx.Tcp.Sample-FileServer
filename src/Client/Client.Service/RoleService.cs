using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AfxTcpFileServerSample.Dto.ProtoBuf;
using AfxTcpFileServerSample.Enums;
using Client.IService;

namespace Client.Service
{
    public class RoleService : BaseService, IRoleService
    {
        public RoleService(IFileClient client)
            : base(client)
        {
        }

        public virtual List<RoleInfoDto> GetList(RoleInfoListParamDto vm)
        {
            List<RoleInfoDto> list = null;
            list = this.Client.Request<List<RoleInfoDto>, RoleInfoListParamDto>(MsgCmd.GetRoleList, vm);

            return list;
        }

    }
}
