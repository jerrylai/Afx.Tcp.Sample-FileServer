using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AfxTcpFileServerSample.Models;
using AfxTcpFileServerSample.IRepository;
using AfxTcpFileServerSample.Dto.ProtoBuf;
using AfxTcpFileServerSample.Enums;

namespace AfxTcpFileServerSample.Repository
{
    public class OptionLogRepository : BaseRepository, IOptionLogRepository
    {
        internal readonly static OptionLogRepository Instance = new OptionLogRepository();

        public virtual int Add(List<OptionLogDto> list)
        {
            int count = 0;
            if (list != null && list.Count > 0)
            {
                using(var db = this.CreateDbContext())
                {
                    using(db.BeginTransaction())
                    {
                        foreach(var vm in list)
                        {
                            count += this.Add(vm, db);
                        }
                        db.Commit();
                    }
                }
            }

            return count;
        }

        public virtual int Add(OptionLogDto vm)
        {
            int count = 0;
            if (vm != null)
            {
                using (var db = this.CreateDbContext())
                {
                    using (db.BeginTransaction())
                    {
                        count += this.Add(vm, db);
                        db.Commit();
                    }
                }
            }

            return count;
        }

        protected virtual int Add(OptionLogDto vm, FileContext db)
        {
            int count = 0;
            if(vm != null)
            {
                OptionLog m = new OptionLog()
                {
                    Type = vm.Type,
                    UserId = vm.UserId,
                    UserAccount = vm.UserAccount,
                    UserName = vm.UserName,
                    Address = vm.Address,
                    CreateTime = vm.CreateTime,
                    Msg = vm.Msg ?? ""
                };
                db.OptionLog.Add(m);
                count += db.SaveChanges();
                vm.Id = m.Id;
            }

            return count;
        }

        public virtual PageListDto<OptionLogDto> GetPageList(OptionLogPageParamDto vm)
        {
            PageListDto<OptionLogDto> pageList = new PageListDto<OptionLogDto>();
            using(var db = this.CreateDbContext())
            {
                var query = from q in db.OptionLog
                            select new OptionLogDto
                            {
                                Id = q.Id,
                                Type = q.Type,
                                UserId = q.UserId,
                                UserAccount = q.UserAccount,
                                UserName = q.UserName,
                                Address = q.Address,
                                CreateTime = q.CreateTime,
                                Msg = q.Msg
                            };

                if (vm.Type != (int)MsgCmd.None)
                {
                    query = query.Where(q => q.Type == vm.Type);
                }
                if(vm.BeginTime.HasValue)
                {
                    query = query.Where(q => q.CreateTime >= vm.BeginTime.Value);
                }
                if (vm.EndTime.HasValue)
                {
                    query = query.Where(q => q.CreateTime <= vm.EndTime.Value);
                }
                if(!string.IsNullOrEmpty(vm.Keyword))
                {
                    query = query.Where(q => q.UserAccount.Contains(vm.Keyword) || q.UserName.Contains(vm.Keyword)
                        || q.Address.Contains(vm.Keyword) || q.Msg.Contains(vm.Keyword));
                }

                pageList.TotalCount = query.Count();

                switch(vm.Orderby)
                {
                    case "Type":
                        query = vm.IsDesc ? query.OrderByDescending(q => q.Type)
                            : query.OrderBy(q => q.Type);
                        break;
                    case "UserAccount":
                        query = vm.IsDesc ? query.OrderByDescending(q => q.UserAccount)
                            : query.OrderBy(q => q.UserAccount);
                        break;
                    case "UserName":
                        query = vm.IsDesc ? query.OrderByDescending(q => q.UserName)
                            : query.OrderBy(q => q.UserName);
                        break;
                    case "CreateTime":
                        query = vm.IsDesc ? query.OrderByDescending(q => q.CreateTime)
                            : query.OrderBy(q => q.CreateTime);
                        break;
                    case "Address":
                        query = vm.IsDesc ? query.OrderByDescending(q => q.Address)
                            : query.OrderBy(q => q.Address);
                        break;
                    default:
                        query = query.OrderByDescending(q => q.Id);
                        break;
                }

                this.GetPage(pageList, query, vm.Index, vm.Size);
            }

            return pageList;
        }
    }
}
