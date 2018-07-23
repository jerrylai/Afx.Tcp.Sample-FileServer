using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AfxTcpFileServerSample.Dto;
using AfxTcpFileServerSample.IService;
using AfxTcpFileServerSample.Enums;
using Afx.Tcp.Host;

namespace AfxTcpFileServerSample.Controllers
{
    public class RoleAuthController : BaseController
    {
        [Auth(AuthType.System)]
        public ActionResult GetAuthList(int roleId)
        {
            if (roleId > 0)
            {
                var roleAuthService = this.GetService<IRoleAuthService>();
                var result = roleAuthService.GetAuthList(roleId);

                return Success(result);
            }

            return Error();
        }
    }
}
