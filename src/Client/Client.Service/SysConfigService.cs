using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Client.IService;

namespace Client.Service
{
    public class SysConfigService : BaseService, ISysConfigService
    {
        public SysConfigService(IFileClient client)
            : base(client)
        {
        }
    }
}
