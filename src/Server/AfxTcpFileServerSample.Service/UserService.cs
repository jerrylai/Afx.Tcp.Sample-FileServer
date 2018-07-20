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
    public class UserService : BaseService, IUserService
    {
        internal readonly static UserService Instance = new UserService();

        public virtual LoginInfoDto Login(LoginParamDto vm)
        {
            LoginInfoDto result = null;
            if(vm != null && !string.IsNullOrEmpty(vm.Account) && !string.IsNullOrEmpty(vm.Password))
            {
                var repository = this.GetRepository<IUserRepository>();
                var id = repository.GetId(vm.Account);
                if(id > 0)
                {
                    var m = repository.Get(id);
                    if(m != null)
                    {
                        if(string.IsNullOrEmpty(m.Password))
                        {
                            if (string.Compare(m.Account, "admin", true) == 0)
                            {
                                m.Password = EncryptUtils.Encrypt("admin");
                                repository.UpdatePassword(m.Id, m.Password);
                            }
                            else if (string.Compare(m.Account, "sync", true) == 0)
                            {
                                m.Password = EncryptUtils.Encrypt("sync");
                                repository.UpdatePassword(m.Id, m.Password);
                            }
                        }
                        string pwd = EncryptUtils.Decrypt(m.Password);
                        if(pwd == vm.Password)
                        {
                            result = new LoginInfoDto()
                            {
                                Id = m.Id,
                                Account = m.Account,
                                Name = m.Name,
                                RoleId = m.RoleId,
                                RoleName = ""
                            };
                            var roleRepository = this.GetRepository<IRoleRepository>();
                            var role = roleRepository.Get(m.RoleId);
                            if (role != null) result.RoleName = role.Name;
                            var roleAuthRepository = this.GetRepository<IRoleAuthRepository>();
                            result.RoleAuth = roleAuthRepository.GetList(m.RoleId);

                            var user = SessionUtils.UserInfo;
                            user.Id = result.Id;
                            user.Account = result.Account;
                            user.Name = result.Name;
                            user.RoleId = result.RoleId;
                            result.SessionId = user.SessionId;
                            OptionLogService.Instance.Add(OptionLogType.Login, "登录！");
                        }
                    }
                }
            }

            return result;
        }

        public virtual void Logout()
        {
            var u = SessionUtils.UserInfo;
            if(u != null)
            {
                string s = "";
                FileService.Instance.CloseFile();
                OptionLogService.Instance.Add(OptionLogType.Logout, "退出！");

                u.Dispose();
            }
        }

        public virtual UserInfoDto Get(int id)
        {
            UserInfoDto m = null;
            if (id > 0)
            {
                var repository = this.GetRepository<IUserRepository>();
                m = repository.Get(id);
            }

            return m;
        }

        public virtual PageListDto<UserInfoDto> GetPageList(UserInfoPageParamDto vm)
        {
            PageListDto<UserInfoDto> result = null;
            if (vm != null && vm.Index > 0 && vm.Size > 0)
            {
                var repository = this.GetRepository<IUserRepository>();
                result = repository.GetPageList(vm);
            }

            return result;
        }

        public virtual bool UpdatePassword(UpdatePwdDto vm)
        {
            bool result = false;
            int id = SessionUtils.UserInfo.Id;
            if (id > 0 && vm != null && !string.IsNullOrEmpty(vm.OldPassword) && !string.IsNullOrEmpty(vm.NewPassword))
            {
                var repository = this.GetRepository<IUserRepository>();
                var m = repository.Get(id);
                string old = EncryptUtils.Decrypt(m.Password);
                if (old == vm.OldPassword)
                {
                    if (repository.UpdatePassword(id, EncryptUtils.Encrypt(vm.NewPassword)) > 0)
                    {
                        OptionLogService.Instance.Add(OptionLogType.UpdatePassword, string.Empty);
                        result = true;
                    }
                }
            }

            return result;
        }

        public virtual bool Exist(string account)
        {
            bool result = false;
            if(!string.IsNullOrEmpty(account))
            {
                var repository = this.GetRepository<IUserRepository>();
                result = repository.GetId(account) > 0;
            }

            return result;
        }

        public virtual void Delete(int id)
        {
            if (id > 0)
            {
                var repository = this.GetRepository<IUserRepository>();
                var user = repository.Get(id);
                if (repository.Delete(id) > 0)
                {
                    OptionLogService.Instance.Add(OptionLogType.DeleteUser, string.Format("Account: {0}, Name: {1}", user.Account, user.Name));
                }
            }
        }

        public virtual int Add(UserInfoDto vm)
        {
            int count = 0;
            if(vm.RoleId > 0 && !string.IsNullOrEmpty(vm.Account) && !string.IsNullOrEmpty(vm.Name))
            {
                var repository = this.GetRepository<IUserRepository>();
                if ((count = repository.Add(vm)) > 0)
                {
                    OptionLogService.Instance.Add(OptionLogType.AddUser, string.Format("Account: {0}, Name: {1}", vm.Account, vm.Name));
                }
            }
            return count;
        }

        public virtual int Update(UserInfoDto vm)
        {
            int count = 0;
            if (vm.Id > 0 && vm.RoleId > 0 && !string.IsNullOrEmpty(vm.Account) && !string.IsNullOrEmpty(vm.Name))
            {
                var repository = this.GetRepository<IUserRepository>();
                var old = repository.Get(vm.Id);
                if ((count = repository.Update(vm)) > 0)
                {
                    string log = string.Format("Account: {0}, Name: {1}", old.Account, old.Name);
                    if (old.Name != vm.Name)
                    {
                        log = string.Format("Account: {0}, Name: ({1} ->  {2})", old.Account, old.Name, vm.Name);
                    }
                    OptionLogService.Instance.Add(OptionLogType.AddUser, log);
                }
            }
            return count;
        }

    }
}
