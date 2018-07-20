using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Client.IService;

namespace Client.Service
{
    public class ServerInfoService : BaseService, IServerInfoService
    {
        public ServerInfoService(IFileClient client)
            : base(client)
        {
        }
    }
}
