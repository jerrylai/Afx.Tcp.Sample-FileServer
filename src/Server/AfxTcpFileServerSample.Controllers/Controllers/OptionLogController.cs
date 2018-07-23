using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AfxTcpFileServerSample.IService;
using AfxTcpFileServerSample.Enums;
using Afx.Tcp.Host;
using AfxTcpFileServerSample.Dto.ProtoBuf;

namespace AfxTcpFileServerSample.Controllers
{
    public class OptionLogController : BaseController
    {
        [Auth(AuthType.System)]
        public ActionResult GetPageList(OptionLogPageParamDto vm)
        {
            if (vm != null && vm.Index > 0 && vm.Size > 0)
            {
                var optionLogService = this.GetService<IOptionLogService>();
                var result = optionLogService.GetPageList(vm);

                return Success(result);
            }

            return Error();
        }
    }
}
