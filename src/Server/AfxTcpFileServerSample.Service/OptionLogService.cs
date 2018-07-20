using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AfxTcpFileServerSample.IRepository;
using AfxTcpFileServerSample.Enums;
using AfxTcpFileServerSample.Common;
using AfxTcpFileServerSample.IService;
using AfxTcpFileServerSample.Dto.ProtoBuf;
using AfxTcpFileServerSample.Dto.Service;

namespace AfxTcpFileServerSample.Service
{
    public class OptionLogService : BaseService, IOptionLogService
    {
        internal readonly static OptionLogService Instance = new OptionLogService();
    
        public virtual void Add(OptionLogType type, string msg)
        {
            try
            {
                var userInfo = SessionUtils.UserInfo;
                if (userInfo != null && userInfo.Id > 0)
                {
                    OptionLogDto vm = new OptionLogDto()
                    {
                        Type = (int)type,
                        UserId = userInfo.Id,
                        UserAccount = userInfo.Account,
                        UserName = userInfo.Name,
                        Address = userInfo.ClientAddress,
                        CreateTime = DateTime.Now,
                        Msg = msg ?? ""
                    };
                    var optionLogRepository = this.GetRepository<IOptionLogRepository>();
                    optionLogRepository.Add(vm);
                }
            }
            catch(Exception ex)
            {
                LogUtils.Error("【OptionLogService.Add】", ex);
            }
        }

        public virtual void Add(OptionLogType type, List<string> list)
        {
            try
            {
                var userInfo = SessionUtils.UserInfo;
                if (userInfo != null && userInfo.Id > 0 && list!= null && list.Count > 0)
                {
                    List<OptionLogDto> addlist = new List<OptionLogDto>(list.Count);
                    foreach (var s in list)
                    {
                        OptionLogDto vm = new OptionLogDto()
                        {
                            Type = (int)type,
                            UserId = userInfo.Id,
                            UserAccount = userInfo.Account,
                            UserName = userInfo.Name,
                            Address = userInfo.ClientAddress,
                            CreateTime = DateTime.Now,
                            Msg = s ?? ""
                        };
                        addlist.Add(vm);
                    }

                    var optionLogRepository = this.GetRepository<IOptionLogRepository>();
                    optionLogRepository.Add(addlist);
                }
            }
            catch (Exception ex)
            {
                LogUtils.Error("【OptionLogService.Add】", ex);
            }
        }

        public virtual void Add(List<AddOptionLogDto> list)
        {
            try
            {
                var userInfo = SessionUtils.UserInfo;
                if (userInfo != null && userInfo.Id > 0 && list != null && list.Count > 0)
                {
                    List<OptionLogDto> addlist = new List<OptionLogDto>(list.Count);
                    foreach (var s in list)
                    {
                        OptionLogDto vm = new OptionLogDto()
                        {
                            Type = (int)s.Type,
                            UserId = userInfo.Id,
                            UserAccount = userInfo.Account,
                            UserName = userInfo.Name,
                            Address = userInfo.ClientAddress,
                            CreateTime = DateTime.Now,
                            Msg = s.Msg ?? ""
                        };
                        addlist.Add(vm);
                    }

                    var optionLogRepository = this.GetRepository<IOptionLogRepository>();
                    optionLogRepository.Add(addlist);
                }
            }
            catch (Exception ex)
            {
                LogUtils.Error("【OptionLogService.Add】", ex);
            }
        }

        public virtual PageListDto<OptionLogDto> GetPageList(OptionLogPageParamDto vm)
        {
            var optionLogRepository = this.GetRepository<IOptionLogRepository>();
            return optionLogRepository.GetPageList(vm);
        }
    }
}
