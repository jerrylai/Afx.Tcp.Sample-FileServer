using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AfxTcpFileServerSample.Models;
using AfxTcpFileServerSample.Enums;
using AfxTcpFileServerSample.IRepository;
using AfxTcpFileServerSample.ICache;
using AfxTcpFileServerSample.Dto.ProtoBuf;

namespace AfxTcpFileServerSample.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        internal readonly static UserRepository Instance = new UserRepository();
        
        public virtual UserInfoDto Get(int id)
        {
            UserInfoDto vm = null;
            if (id > 0)
            {
                var userCache = this.GetCache<IUserCache>();
                vm = userCache.GetOrSet(id, () =>
                {
                    using (var db = this.CreateDbContext())
                    {
                        var query = from m in db.User
                                    where m.Id == id && m.IsDelete == false
                                    select new UserInfoDto()
                                    {
                                        Id = m.Id,
                                        Account = m.Account,
                                        Name = m.Name,
                                        Password = m.Password,
                                        RoleId = m.RoleId,
                                        UpdateTime = m.UpdateTime,
                                        IsSystem = m.IsSystem,
                                        IsDelete = m.IsDelete
                                    };

                        return query.FirstOrDefault();
                    }
                });
            }

            return vm;
        }
        
        public virtual int GetId(string account)
        {
            int id = 0;
            if (!string.IsNullOrEmpty(account))
            {
                account = account.ToLower();
                var userIdCache = this.GetCache<IUserIdCache>();
                id = userIdCache.GetOrSet(account, () =>
                {
                    using (var db = this.CreateDbContext())
                    {
                        return db.User.Where(q => q.Account == account && q.IsDelete == false).Select(q => q.Id).FirstOrDefault();
                    }
                }) ?? 0;
            }

            return id;
        }


        public virtual int Add(UserInfoDto vm)
        {
            int count = 0;
            if (vm != null && !string.IsNullOrEmpty(vm.Account))
            {
                vm.Account = vm.Account.ToLower();
                using (var db = this.CreateDbContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = true;
                    using (db.BeginTransaction(System.Data.IsolationLevel.Serializable))
                    {
                        if (db.User.Where(q => q.Account == vm.Account && q.IsDelete == false).Count() == 0)
                        {
                            var m = new User()
                            {
                                Account = vm.Account,
                                Name = vm.Name ?? "",
                                Password = vm.Password ?? "",
                                RoleId = vm.RoleId,
                                IsSystem = false,
                                IsDelete = false,
                                UpdateTime = DateTime.Now
                            };
                            db.User.Add(m);
                            db.AddCommitCallback(() => { this.GetCache<IUserIdCache>().Remove(vm.Account); });
                            count += db.SaveChanges();
                            db.Commit();
                            vm.Id = m.Id;
                            vm.UpdateTime = m.UpdateTime;
                            vm.IsDelete = m.IsDelete;
                        }
                    }
                }
            }

            return count;
        }

        public virtual int Update(UserInfoDto vm)
        {
            int count = 0;
            if (vm != null && vm.Id > 0)
            {
                using (var db = this.CreateDbContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = true;
                    using (db.BeginTransaction())
                    {
                        var m = db.User.Where(q => q.Id == vm.Id && q.IsSystem == false && q.IsDelete == false).FirstOrDefault();
                        if (m != null)
                        {
                            int i = 0;
                            if (m.Name != vm.Name)
                            {
                                m.Name = vm.Name;
                                i++;
                            }
                            if (m.IsSystem == false && m.RoleId != vm.RoleId)
                            {
                                m.RoleId = vm.RoleId;
                                i++;
                            }
                            if (!string.IsNullOrEmpty(vm.Password) && m.Password != vm.Password)
                            {
                                m.Password = vm.Password;
                                i++;
                            }
                            db.AddCommitCallback(() => 
                            {
                                var userCache = this.GetCache<IUserCache>();
                                userCache.Remove(m.Id);
                            });
                            if (i > 0)
                            {
                                m.UpdateTime = DateTime.Now;
                                count += db.SaveChanges();
                                db.Commit();
                            }

                            vm.UpdateTime = m.UpdateTime;
                            vm.IsDelete = m.IsDelete;
                        }
                    }
                }
            }

            return count;
        }

        public virtual int Delete(int id)
        {
            int count = 0;
            using (var db = this.CreateDbContext())
            {
                db.Configuration.AutoDetectChangesEnabled = true;
                using (db.BeginTransaction())
                {
                    var m = db.User.Where(q => q.Id == id && q.IsSystem == false && q.IsDelete == false).FirstOrDefault();
                    if (m != null)
                    {
                        m.IsDelete = true;
                        m.UpdateTime = DateTime.Now;
                        db.AddCommitCallback(() => 
                        {
                            var userCache = this.GetCache<IUserCache>();
                            userCache.Remove(m.Id);
                            var userIdCache = this.GetCache<IUserIdCache>();
                            userIdCache.Remove(m.Account);
                        });
                        count += db.SaveChanges();
                        db.Commit();
                    }
                }
            }

            return count;
        }

        public virtual int UpdatePassword(int id, string password)
        {
            int count = 0;
            if (id > 0 && !string.IsNullOrEmpty(password))
            {
                using (var db = this.CreateDbContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = true;
                    using (db.BeginTransaction())
                    {
                        var m = db.User.Where(q => q.Id == id && q.IsDelete == false).FirstOrDefault();
                        if (m != null)
                        {
                            m.Password = password;
                            m.UpdateTime = DateTime.Now;
                            db.AddCommitCallback(() =>
                            {
                                var userCache = this.GetCache<IUserCache>();
                                userCache.Remove(m.Id);
                            });
                            count += db.SaveChanges();
                            db.Commit();
                        }
                    }
                }
            }

            return count;
        }

        public virtual PageListDto<UserInfoDto> GetPageList(UserInfoPageParamDto vm)
        {
            PageListDto<UserInfoDto> pageList = new PageListDto<UserInfoDto>();
            using (var db = this.CreateDbContext())
            {
                var query = from q in db.User
                            join a in db.Role on q.RoleId equals a.Id into b
                            from c in b.DefaultIfEmpty()
                            where q.IsDelete == false
                            select new UserInfoDto
                            {
                                Id = q.Id,
                                Name = q.Name,
                                Account = q.Account,
                                RoleId = q.RoleId,
                                UpdateTime = q.UpdateTime,
                                IsSystem = q.IsSystem,
                                RoleName = c.Name
                            };
                if (!string.IsNullOrEmpty(vm.Keyword))
                {
                    query = query.Where(q => q.Account.Contains(vm.Keyword.ToLower()) || q.Name.Contains(vm.Keyword) || q.RoleName.Contains(vm.Keyword));
                }

                pageList.TotalCount = query.Count();

                switch (vm.Orderby)
                {
                    case "Name":
                        query = vm.IsDesc ? query.OrderByDescending(q => q.Name) :
                             query.OrderBy(q => q.Name);
                        break;
                    case "Account":
                        query = vm.IsDesc ? query.OrderByDescending(q => q.Account) :
                             query.OrderBy(q => q.Account);
                        break;
                    case "RoleName":
                        query = vm.IsDesc ? query.OrderByDescending(q => q.RoleName) :
                             query.OrderBy(q => q.RoleName);
                        break;
                    case "UpdateTime":
                        query = vm.IsDesc ? query.OrderByDescending(q => q.UpdateTime) :
                             query.OrderBy(q => q.UpdateTime);
                        break;
                    case "Id":
                    default:
                        query = vm.IsDesc ? query.OrderByDescending(q => q.Id) :
                             query.OrderBy(q => q.Id);
                        break;
                }

                this.GetPage(pageList, query, vm.Index, vm.Size);
            }

            return pageList;
        }

        public virtual List<UserInfoDto> GetSyncList(SyncParamDto vm)
        {
            List<UserInfoDto> list = null;
            using (var db = this.CreateDbContext())
            {
                DateTime? startTime = null;
                if (vm.SyncId.HasValue && !string.IsNullOrEmpty(vm.SyncKey))
                {
                    var m = db.User.Where(q => q.Id == vm.SyncId.Value).FirstOrDefault();
                    if (m != null && m.Account == vm.SyncKey)
                    {
                        startTime = vm.SyncUpdateTime;
                    }
                }

                var query = from q in db.User
                            join r in db.Role on q.RoleId equals r.Id
                            where q.IsSystem == false
                            select new UserInfoDto
                            {
                                Id = q.Id,
                                Account = q.Account,
                                Name = q.Name,
                                Password = q.Password,
                                RoleId = q.RoleId,
                                UpdateTime = q.UpdateTime,
                                IsDelete = q.IsDelete,
                                RoleName = r.Name
                            };

                if (startTime.HasValue)
                {
                    query = query.Where(q => q.UpdateTime > startTime.Value);
                }

                query = query.OrderBy(q => q.UpdateTime);

                list = query.Take(vm.Count).ToList();
            }

            return list;
        }

        public virtual int UpdateSync(List<UserInfoDto> list)
        {
            int count = 0;
            if (list != null)
            {
                var rolelist = RoleRepository.Instance.GetList();
                var userRole = rolelist.Find(q => q.IsSystem == true && q.Type == RoleType.User);
                using (var db = this.CreateDbContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = true;
                    using (db.BeginTransaction(System.Data.IsolationLevel.Serializable))
                    {
                        var userCache = this.GetCache<IUserCache>();
                        var userIdCache = this.GetCache<IUserIdCache>();
                        foreach (var vm in list)
                        {
                            var m = db.User.Where(q => q.Account == vm.Account && q.IsSystem == false && q.IsDelete == false).FirstOrDefault();
                            if (m == null && vm.IsDelete == false)
                            {
                                m = new User()
                                {
                                    Account = vm.Account,
                                    IsSystem = false,
                                    RoleId = userRole.Id
                                };
                            }

                            if(m != null)
                            {
                                m.IsDelete = vm.IsDelete;
                                m.Name = vm.Name;
                                m.Password = vm.Password;
                                m.UpdateTime = vm.UpdateTime;

                                var r = rolelist.Find(q => db.DatabaseType == DatabaseType.Oracle ? q.Name == vm.RoleName : q.Name.ToLower() == vm.RoleName.ToLower());
                                if (r != null) m.RoleId = r.Id;
                                if (m.Id == 0) db.User.Add(m);
                                count += db.SaveChanges();
                                userCache.Remove(m.Id);
                                if (m.IsDelete == true)
                                {
                                    db.AddCommitCallback(() => { userIdCache.Remove(m.Account); });
                                }
                            }
                        }

                        db.Commit();
                    }
                }
            }

            return count;
        }
    }
}
