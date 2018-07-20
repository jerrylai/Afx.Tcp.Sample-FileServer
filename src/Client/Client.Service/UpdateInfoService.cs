using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Client.IService;

namespace Client.Service
{
    public class UpdateInfoService : BaseService, IUpdateInfoService
    {
        public UpdateInfoService(IFileClient client)
            : base(client)
        {
        }
    }
}
