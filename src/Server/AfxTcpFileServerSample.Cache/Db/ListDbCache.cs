﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfxTcpFileServerSample.Cache
{
    public abstract class ListDbCache : BaseCache
    {
        public ListDbCache() : base("ListDb") { }
    }
}
