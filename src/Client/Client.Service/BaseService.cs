using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Client.IService;

namespace Client.Service
{
    public abstract class BaseService : IBaseService
    {
        protected IFileClient Client { get; private set; }

        protected BaseService(IFileClient client)
        {
            this.Client = client;
            this.IsDisposed = false;
        }

        public virtual void Dispose()
        {
            this.Client = null;
            this.IsDisposed = true;
        }

        public virtual bool IsDisposed { get; private set; }
    }
}
