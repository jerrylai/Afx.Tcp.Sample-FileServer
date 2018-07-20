using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AfxTcpFileServerSample.Dto.ProtoBuf;
using Client.IService;
using AfxTcpFileServerSample.Enums;

namespace Client.Service
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IFileClient client)
            : base(client)
        {
        }

        public virtual bool UpdatePwd(string oldPwd, string newPwd)
        {
            bool result = false;
            if(this.Client.IsLogin)
            {
                UpdatePwdDto vm = new UpdatePwdDto()
                {
                    OldPassword = oldPwd,
                    NewPassword = newPwd
                };
                result = this.Client.Request<bool, UpdatePwdDto>(MsgCmd.UpdatePassword, vm);
            }

            return result;
        }

        public virtual PageListDto<UserInfoDto> GetPageList(UserInfoPageParamDto vm)
        {
            PageListDto<UserInfoDto> page = null;
            if(this.Client.IsLogin)
            {
                page = this.Client.Request<PageListDto<UserInfoDto>, UserInfoPageParamDto>(MsgCmd.GetUserPageList, vm);
            }

            return page;
        }

    }
}
