using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AfxTcpFileServerSample.Enums;
using AfxTcpFileServerSample.IRepository;
using AfxTcpFileServerSample.IService;

namespace AfxTcpFileServerSample.Service
{
    public class RoleAuthService : BaseService, IRoleAuthService
    {
        internal readonly static RoleAuthService Instance = new RoleAuthService();

        public virtual List<AuthType> GetAuthList(int roleId)
        {
            var repository = this.GetRepository<IRoleAuthRepository>();
            return repository.GetList(roleId);
        }

        public virtual bool CheckRole(int roleId, List<AuthType> auths)
        {
            bool result = false;
            if (roleId > 0 && auths != null && auths.Count > 0)
            {
                var repository = this.GetRepository<IRoleAuthRepository>();
                var list = repository.GetList(roleId);
                result = list.FindIndex(q => auths.Contains(q)) >= 0;
            }

            return result;
        }
    }
}
