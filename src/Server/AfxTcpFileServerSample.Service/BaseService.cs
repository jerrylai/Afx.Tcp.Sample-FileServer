using System;
using System.Collections.Generic;
using System.Text;

using Afx.Ioc;
using Afx.Threading;
using AfxTcpFileServerSample.Common;
using AfxTcpFileServerSample.IRepository;

namespace AfxTcpFileServerSample.Service
{
    public abstract class BaseService
    {
        protected virtual T GetRepository<T>() where T : IBaseRepository => IocUtils.Get<T>();

        protected virtual T GetRepository<T>(string name) where T : IBaseRepository => IocUtils.Get<T>(name);

        protected virtual T GetRepository<T>(object[] args) where T : IBaseRepository => IocUtils.Get<T>(args);

        protected virtual T GetRepository<T>(string name, object[] args) where T : IBaseRepository => IocUtils.Get<T>(name, args);
    }
}
