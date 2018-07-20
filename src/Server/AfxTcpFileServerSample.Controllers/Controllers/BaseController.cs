﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AfxTcpFileServerSample.IService;
using AfxTcpFileServerSample.Common;
using Afx.Tcp.Host;

namespace AfxTcpFileServerSample.Controllers
{
    public abstract class BaseController : Controller
    {
        protected UserInfo UserInfo;

        public override void Init(Session session, Afx.Tcp.Protocols.MsgData msg)
        {
            base.Init(session, msg);
            this.UserInfo = session[FileServer.USER_INFO_KEY] as UserInfo;
            SessionUtils.UserInfo = this.UserInfo;
        }

        protected virtual T GetService<T>() where T : IBaseService => IocUtils.Get<T>();

        protected virtual T GetService<T>(string name) where T : IBaseService => IocUtils.Get<T>(name);

        protected virtual T GetService<T>(object[] args) where T : IBaseService => IocUtils.Get<T>(args);

        protected virtual T GetService<T>(string name, object[] args) where T : IBaseService => IocUtils.Get<T>(name, args);

        public override void Dispose()
        {
            this.UserInfo = null;
            SessionUtils.UserInfo = null;

            base.Dispose();
        }
    }
}
