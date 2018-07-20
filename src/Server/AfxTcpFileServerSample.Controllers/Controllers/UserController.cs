using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AfxTcpFileServerSample.IService;
using AfxTcpFileServerSample.Enums;
using Afx.Tcp.Host;
using AfxTcpFileServerSample.Dto.ProtoBuf;

namespace AfxTcpFileServerSample.Controllers
{
    public class UserController : BaseController
    {
        public ActionResult Login()
        {
            var vm = this.GetData<LoginParamDto>();
            if(vm != null && !string.IsNullOrEmpty(vm.Account) && !string.IsNullOrEmpty(vm.Password))
            {
                var userService = this.GetService<IUserService>();
                var result = userService.Login(vm);
                if(result != null)
                {
                    return Success(result);
                }
            }

            return ParamError();
        }

        public ActionResult Logout()
        {
            var userService = this.GetService<IUserService>();
            userService.Logout();

            return Success();
        }

        [Auth]
        public ActionResult UpdatePassword()
        {
            UpdatePwdDto vm = this.GetData<UpdatePwdDto>();
            if (vm != null && !string.IsNullOrEmpty(vm.OldPassword) && !string.IsNullOrEmpty(vm.NewPassword))
            {
                var userService = this.GetService<IUserService>();
                bool result = userService.UpdatePassword(vm);
                return Success(result);
            }

            return ParamError();
        }

        [Auth(AuthType.System)]
        public ActionResult Get()
        {
            int id = this.GetData<int>();
            if(id > 0)
            {
                var userService = this.GetService<IUserService>();
                var vm = userService.Get(id);
                if(vm != null)
                {
                    return Success(vm);
                }
            }

            return ParamError();
        }

        [Auth(AuthType.System)]
        public ActionResult GetPageList()
        {
            var vm = this.GetData<UserInfoPageParamDto>();
            if (vm != null && vm.Index > 0 && vm.Size > 0)
            {
                var userService = this.GetService<IUserService>();
                var result = userService.GetPageList(vm);
                if (result != null)
                {
                    return Success(result);
                }
            }

            return ParamError();
        }

        [Auth(AuthType.System)]
        public ActionResult Exist()
        {
            string account = this.GetData<string>();
            if (!string.IsNullOrEmpty(account))
            {
                var userService = this.GetService<IUserService>();
                var result = userService.Exist(account);
                return Success(result);
            }

            return ParamError();
        }

        [Auth(AuthType.System)]
        public ActionResult Delete()
        {
            int id = this.GetData<int>();
            if (id > 0)
            {
                var userService = this.GetService<IUserService>();
                userService.Delete(id);
                return Success();
            }

            return ParamError();
        }

        [Auth(AuthType.System)]
        public ActionResult Add()
        {
            var vm = this.GetData<UserInfoDto>();
            if (vm != null && !string.IsNullOrEmpty(vm.Account) && !string.IsNullOrEmpty(vm.Name))
            {
                var userService = this.GetService<IUserService>();
                if (userService.Add(vm) > 0)
                {
                    return Success(vm);
                }
            }

            return ParamError();
        }

        [Auth(AuthType.System)]
        public ActionResult Update()
        {
            var vm = this.GetData<UserInfoDto>();
            if (vm != null && vm.Id > 0 && !string.IsNullOrEmpty(vm.Name))
            {
                var userService = this.GetService<IUserService>();
                if (userService.Update(vm) > 0)
                {
                    return Success(vm);
                }
            }

            return ParamError();
        }
    }
}
