using Client.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.Service
{
    public class OptionLogService : BaseService, IOptionLogService
    {
        public OptionLogService(IFileClient client)
            : base(client)
        {
        }
    }
}
