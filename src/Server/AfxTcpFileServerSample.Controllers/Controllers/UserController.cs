using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AfxTcpFileServerSample.IService;
using AfxTcpFileServerSample.Enums;
using Afx.Tcp.Host;
using AfxTcpFileServerSample.Dto.ProtoBuf;
using AfxTcpFileServerSample.Common;
using Afx.Tcp.Protocols;
using Afx.Utils;
using System.Threading.Tasks;

namespace AfxTcpFileServerSample.Controllers
{
    public class UserController : BaseController
    {
        public ActionResult Login(LoginParamDto vm)
        {
            if(vm != null && !string.IsNullOrEmpty(vm.Account) && !string.IsNullOrEmpty(vm.Password))
            {
                var userService = this.GetService<IUserService>();
                var result = userService.Login(vm);
                if(result != null)
                {
                    return Success(result);
                }
            }

            return Error();
        }

        public ActionResult Logout()
        {
            var userService = this.GetService<IUserService>();
            userService.Logout();

            return Success();
        }

        [Auth]
        public ActionResult UpdatePassword(UpdatePwdDto vm)
        {
            if (vm != null && !string.IsNullOrEmpty(vm.OldPassword) && !string.IsNullOrEmpty(vm.NewPassword))
            {
                var userService = this.GetService<IUserService>();
                bool result = userService.UpdatePassword(vm);
                return Success(result);
            }

            return Error();
        }

        [Auth(AuthType.System)]
        public ActionResult Get(int id)
        {
            if(id > 0)
            {
                var userService = this.GetService<IUserService>();
                var vm = userService.Get(id);
                if(vm != null)
                {
                    return Success(vm);
                }
            }

            return Error();
        }

        [Auth(AuthType.System)]
        public ActionResult GetPageList(UserInfoPageParamDto vm)
        {
            if (vm != null && vm.Index > 0 && vm.Size > 0)
            {
                var userService = this.GetService<IUserService>();
                var result = userService.GetPageList(vm);
                if (result != null)
                {
                    return Success(result);
                }
            }

            return Error();
        }

        [Auth(AuthType.System)]
        public ActionResult Exist(string account)
        {
            if (!string.IsNullOrEmpty(account))
            {
                var userService = this.GetService<IUserService>();
                var result = userService.Exist(account);
                return Success(result);
            }

            return Error();
        }

        [Auth(AuthType.System)]
        public ActionResult Delete(int id)
        {
            if (id > 0)
            {
                var userService = this.GetService<IUserService>();
                userService.Delete(id);
                return Success();
            }

            return Error();
        }

        [Auth(AuthType.System)]
        public ActionResult Add(UserInfoDto vm)
        {
            if (vm != null && !string.IsNullOrEmpty(vm.Account) && !string.IsNullOrEmpty(vm.Name))
            {
                var userService = this.GetService<IUserService>();
                if (userService.Add(vm) > 0)
                {
                    return Success(vm);
                }
            }

            return Error();
        }

        [Auth(AuthType.System)]
        public ActionResult Update(UserInfoDto vm)
        {
            if (vm != null && vm.Id > 0 && !string.IsNullOrEmpty(vm.Name))
            {
                var userService = this.GetService<IUserService>();
                if (userService.Update(vm) > 0)
                {
                    return Success(vm);
                }
            }

            return Error();
        }
    }
}
