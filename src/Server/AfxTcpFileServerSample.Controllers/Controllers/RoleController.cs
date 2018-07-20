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
    public class RoleController : BaseController
    {
        [Auth(AuthType.System)]
        public ActionResult Get()
        {
            int id = this.GetData<int>();
            if (id > 0)
            {
                var roleService = this.GetService<IRoleService>();
                var vm = roleService.Get(id);
                if (vm != null)
                {
                    return Success(vm);
                }
            }

            return ParamError();
        }

        [Auth(AuthType.System)]
        public ActionResult GetList()
        {
            RoleInfoListParamDto vm = this.GetData<RoleInfoListParamDto>();
            if (vm != null)
            {
                var roleService = this.GetService<IRoleService>();
                var list = roleService.GetList(vm);
                return Success(list);
            }

            return ParamError();
        }

        [Auth(AuthType.System)]
        public ActionResult Add()
        {
            var vm = this.GetData<RoleInfoDto>();
            if(vm != null && !string.IsNullOrEmpty(vm.Name))
            {
                var roleService = this.GetService<IRoleService>();
                if (roleService.Add(vm) > 0)
                {
                    return Success(vm);
                }
            }

            return ParamError();
        }

        [Auth(AuthType.System)]
        public ActionResult Update()
        {
            var vm = this.GetData<RoleInfoDto>();
            if (vm != null && vm.Id > 0 && !string.IsNullOrEmpty(vm.Name))
            {
                var roleService = this.GetService<IRoleService>();
                if (roleService.Update(vm) > 0)
                {
                    return Success(vm);
                }
            }

            return ParamError();
        }

        [Auth(AuthType.System)]
        public ActionResult Delete()
        {
            var id = this.GetData<int>();
            if (id > 0 )
            {
                var roleService = this.GetService<IRoleService>();
                if (roleService.Delete(id) > 0)
                {
                    return Success();
                }
            }

            return ParamError();
        }

        [Auth(AuthType.System)]
        public ActionResult Exist()
        {
            var name = this.GetData<string>();
            if (!string.IsNullOrEmpty(name))
            {
                var roleService = this.GetService<IRoleService>();
                var result = roleService.Exist(name);
                return Success(result);
            }

            return ParamError();
        }

        [Auth(AuthType.System)]
        public ActionResult GetUserCount()
        {
            int id = this.GetData<int>();
            if (id > 0)
            {
                var roleService = this.GetService<IRoleService>();
                var count = roleService.GetUserCount(id);
                return Success(count);
            }

            return ParamError();
        }
    }
}
