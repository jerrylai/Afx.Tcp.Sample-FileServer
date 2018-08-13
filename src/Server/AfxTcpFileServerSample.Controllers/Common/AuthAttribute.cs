using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AfxTcpFileServerSample.Enums;
using AfxTcpFileServerSample.IService;
using Afx.Tcp.Host;
using Afx.Tcp.Protocols;
using AfxTcpFileServerSample.Common;
using Afx.Ioc;

namespace AfxTcpFileServerSample.Controllers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthAttribute : AuthorizeAttribute
    {
        private List<AuthType> authList = new List<AuthType>();
        public List<AuthType> AuthList { get { return this.authList; } }

        public AuthAttribute()
        {
            
        }

        public AuthAttribute(AuthType auth)
        {
            if (auth != AuthType.None)
            {
                this.AuthList.Add(auth);
            }
        }

        public AuthAttribute(AuthType[] auths)
        {
            if (auths != null && auths.Length > 0)
            {
                foreach (var auth in auths)
                {
                    if (auth != AuthType.None)
                    {
                        this.AuthList.Add(auth);
                    }
                }
            }
        }

        public override void OnAuthorization(AuthorizationContext authContext)
        {
            var user = SessionUtils.UserInfo;
            if (user != null && user.Id > 0)
            {
                authContext.IsAuth = true;
                var roleAuthService = IocUtils.Get<IRoleAuthService>();
                if (this.AuthList.Count > 0
                    && !roleAuthService.CheckRole(user.RoleId, this.AuthList))
                {
                    authContext.IsAuth = false;
                    authContext.Result = new ActionResult();
                    authContext.Result.SetMsg(MsgStatus.NeedLogin, "无权限！");
                }
            }
            else
            {
                authContext.IsAuth = false;
                authContext.Result = new ActionResult();
                authContext.Result.SetMsg(MsgStatus.NeedLogin, "未登录！");
            }
        }
    }
}
