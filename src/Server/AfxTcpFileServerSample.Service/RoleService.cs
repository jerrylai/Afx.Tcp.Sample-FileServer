using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AfxTcpFileServerSample.IRepository;
using AfxTcpFileServerSample.Enums;
using AfxTcpFileServerSample.Common;
using AfxTcpFileServerSample.IService;
using AfxTcpFileServerSample.Dto.ProtoBuf;

namespace AfxTcpFileServerSample.Service
{
    public class RoleService : BaseService, IRoleService
    {
        internal readonly static RoleService Instance = new RoleService();

        public virtual RoleInfoDto Get(int id)
        {
            var roleRepository = this.GetRepository<IRoleRepository>();
            RoleInfoDto vm = roleRepository.Get(id);

            if (vm != null) vm.AuthList = this.GetRepository<IRoleAuthRepository>().GetList(id);

            return vm;
        }

        public virtual List<RoleInfoDto> GetList(RoleInfoListParamDto vm)
        {
            var roleRepository = this.GetRepository<IRoleRepository>();
            IEnumerable<RoleInfoDto> list = roleRepository.GetList();
            if (vm.Type != 0) list = list.Where(q => q.Type == vm.Type);
            if (!string.IsNullOrEmpty(vm.Keyword)) list = list.Where(q => q.Name.Contains(vm.Keyword));
            switch(vm.Orderby)
            {
                case "Type":
                    list = vm.IsDesc ? list.OrderByDescending(q => q.Type)
                        : list.OrderBy(q => q.Type);
                    break;
                case "Name":
                    list = vm.IsDesc ? list.OrderByDescending(q => q.Name)
                        : list.OrderBy(q => q.Name);
                    break;
                case "UpdateTime":
                    list = vm.IsDesc ? list.OrderByDescending(q => q.UpdateTime)
                        : list.OrderBy(q => q.UpdateTime);
                    break;
            }

            return list.ToList();
        }

        public virtual int Update(RoleInfoDto vm)
        {
            int count = 0;
            if (vm != null && !string.IsNullOrEmpty(vm.Name))
            {
                var roleRepository = this.GetRepository<IRoleRepository>();
                var old = roleRepository.Get(vm.Id);
                if ((count = roleRepository.Update(vm)) > 0)
                {
                   OptionLogService.Instance.Add(OptionLogType.UpdateRole, old.Name != vm.Name ? old.Name + " -> " + vm.Name : old.Name);
                }
            }

            return count;
        }

        public virtual int Add(RoleInfoDto vm)
        {
            int count = 0;
            if (vm != null && !string.IsNullOrEmpty(vm.Name))
            {
                var roleRepository = this.GetRepository<IRoleRepository>();
                if ((count = roleRepository.Update(vm)) > 0)
                {
                    OptionLogService.Instance.Add(OptionLogType.AddRole, vm.Name);
                }
            }

            return count;
        }

        public virtual int GetUserCount(int id)
        {
            var roleRepository = this.GetRepository<IRoleRepository>();
            return roleRepository.GetUserCount(id);
        }

        public virtual bool Exist(string name)
        {
            var roleRepository = this.GetRepository<IRoleRepository>();
            return roleRepository.Exist(name);
        }

        public virtual int Delete(int id)
        {
            int count = 0;
            if(id > 0)
            {
                var roleRepository = this.GetRepository<IRoleRepository>();
                var role = roleRepository.Get(id);
                if (roleRepository.Delete(id) > 0)
                {
                    OptionLogService.Instance.Add(OptionLogType.DeleteRole, role.Name);
                }
            }

            return count;
        }
    }
}
