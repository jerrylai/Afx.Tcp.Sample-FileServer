using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AfxTcpFileServerSample.IRepository;
using AfxTcpFileServerSample.Enums;
using AfxTcpFileServerSample.Common;
using AfxTcpFileServerSample.IService;

namespace AfxTcpFileServerSample.Service
{
    public class UpdateInfoService : BaseService, IUpdateInfoService
    {
        internal readonly static UpdateInfoService Instance = new UpdateInfoService();
    }
}
