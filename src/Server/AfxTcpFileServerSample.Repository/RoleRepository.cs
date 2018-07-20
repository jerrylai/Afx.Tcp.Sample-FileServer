using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AfxTcpFileServerSample.Models;
using AfxTcpFileServerSample.IRepository;
using AfxTcpFileServerSample.Enums;
using AfxTcpFileServerSample.ICache;
using AfxTcpFileServerSample.Dto.ProtoBuf;

namespace AfxTcpFileServerSample.Repository
{
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        internal readonly static RoleRepository Instance = new RoleRepository();
        
        public virtual List<RoleInfoDto> GetList()
        {
            var roleCache = this.GetCache<IRoleCache>();
            List<RoleInfoDto> list = roleCache.GetOrSet(() =>
            {
                using (var db = this.CreateDbContext())
                {
                    var query = from q in db.Role
                                where q.IsDelete == false
                                select new RoleInfoDto
                                {
                                    Id = q.Id,
                                    Name = q.Name,
                                    Type = q.Type,
                                    IsSystem = q.IsSystem,
                                    UpdateTime = q.UpdateTime,
                                    Key = q.Key,
                                    IsDelete = q.IsDelete
                                };

                    return query.ToList();
                }
            });

            return list;
        }

        public virtual RoleInfoDto Get(int id)
        {
            RoleInfoDto vm = null;
            var list = this.GetList();
            vm = list.Find(q => q.Id == id);

            return vm;
        }

        public virtual RoleInfoDto Get(RoleType roleType)
        {
            var list = this.GetList();
            var vm = list.Find(q => q.IsSystem == true && q.Type == roleType);

            return vm;
        }

        public virtual int Delete(int id)
        {
            int count = 0;
            using (var db = this.CreateDbContext())
            {
                db.Configuration.AutoDetectChangesEnabled = true;
                var m = db.Role.Where(q => q.Id == id && q.IsDelete == false && q.IsSystem == false).FirstOrDefault();
                if (m != null && db.User.Where(q => q.RoleId == id && q.IsDelete == false).Count() == 0)
                {
                    m.IsDelete =true;
                    m.UpdateTime = DateTime.Now;
                    db.AddCommitCallback(() => 
                    {
                        var roleCache = this.GetCache<IRoleCache>();
                        roleCache.Remove();
                    });
                    count += db.SaveChanges();
                }
            }

            return count;
        }

        public virtual int Update(RoleInfoDto vm)
        {
            int count = 0;
            using(var db = this.CreateDbContext())
            {
                db.Configuration.AutoDetectChangesEnabled = true;
                using (db.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    if (db.Role.Where(q => q.Id != vm.Id && q.Name == vm.Name && q.IsDelete == false).Count() == 0)
                    {
                        var m = db.Role.Where(q => q.Id == vm.Id && q.IsSystem == false && q.IsDelete == false).FirstOrDefault();
                        if (m != null)
                        {
                            int i = 0;
                            if (vm.AuthList != null)
                            {
                                i = RoleAuthRepository.Instance.AddOrUpdate(m.Id, vm.AuthList, db);
                                count += i;
                            }
                            if (m.Type != vm.Type)
                            {
                                m.Type = vm.Type;
                                i++;
                            }
                            if (m.Name != vm.Name)
                            {
                                m.Name = vm.Name;
                                i++;
                            }
                            if (i > 0)
                            {
                                m.UpdateTime = DateTime.Now;
                            }
                            db.AddCommitCallback(() =>
                            {
                                var roleCache = this.GetCache<IRoleCache>();
                                roleCache.Remove();
                            });
                            count += db.SaveChanges();
                            db.Commit();
                            vm.UpdateTime = m.UpdateTime;
                        }
                    }
                }
            }

            return count;
        }

        public virtual int Add(RoleInfoDto vm)
        {
            int count = 0;
            using (var db = this.CreateDbContext())
            {
                db.Configuration.AutoDetectChangesEnabled = true;
                using (db.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    if (db.Role.Where(q => q.Name == vm.Name && q.IsDelete == false).Count() == 0)
                    {
                        var m = new Role()
                        {
                            Name = vm.Name,
                            Type = vm.Type,
                            IsDelete = false,
                            IsSystem = false,
                            Key = Guid.NewGuid().ToString("n"),
                            UpdateTime = DateTime.Now
                        };
                        db.Role.Add(m);
                        db.AddCommitCallback(() =>
                        {
                            var roleCache = this.GetCache<IRoleCache>();
                            roleCache.Remove();
                        });
                        count += db.SaveChanges();
                        vm.Id = m.Id;
                        if (vm.AuthList != null)
                        {
                            count += RoleAuthRepository.Instance.AddOrUpdate(m.Id, vm.AuthList, db);
                        }
                        db.Commit();
                        vm.UpdateTime = m.UpdateTime;
                    }
                }
            }

            return count;
        }

        public virtual bool Exist(string name)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(name))
            {
                result = this.GetList().Find(q => q.Name == name && q.IsDelete == false) != null;
            }

            return result;
        }

        public virtual int GetUserCount(int id)
        {
            int count = 0;
            if (id > 0)
            {
                using (var db = this.CreateDbContext())
                {
                    count = db.User.Where(q => q.RoleId == id && q.IsDelete == false).Count();
                }
            }

            return count;
        }

        public virtual List<RoleInfoDto> GetSyncList(SyncParamDto vm)
        {
            List<RoleInfoDto> list = null;
            using (var db = this.CreateDbContext())
            {
                DateTime? startTime = null;
                if (vm.SyncId.HasValue && !string.IsNullOrEmpty(vm.SyncKey))
                {
                    var m = db.Role.Where(q => q.Id == vm.SyncId.Value).FirstOrDefault();
                    if (m != null && m.Key == vm.SyncKey)
                    {
                        startTime = vm.SyncUpdateTime;
                    }
                }

                var query = from q in db.Role
                            where q.IsSystem == false
                            select new RoleInfoDto
                            {
                                Id = q.Id,
                                Type = q.Type,
                                Name = q.Name,
                                IsSystem = q.IsSystem,
                                Key = q.Key,
                                UpdateTime = q.UpdateTime,
                                IsDelete = q.IsDelete
                            };

                if (startTime.HasValue)
                {
                    query = query.Where(q => q.UpdateTime > startTime.Value);
                }

                query = query.OrderBy(q => q.UpdateTime);

                list = query.Take(vm.Count).ToList();
                foreach(var m in list)
                {
                    m.AuthList = RoleAuthRepository.Instance.GetList(m.Id, db);
                }
            }

            return list;
        }

        public virtual int UpdateSync(List<RoleInfoDto> list)
        {
            int count = 0;
            if (list != null)
            {
                using (var db = this.CreateDbContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = true;
                    using (db.BeginTransaction(System.Data.IsolationLevel.Serializable))
                    {
                        foreach (var vm in list)
                        {
                            var m = db.Role.Where(q => q.Name== vm.Name && q.IsSystem == false && q.IsDelete == false).FirstOrDefault();
                            if (m == null && vm.IsDelete == false)
                            {
                                m = new Role()
                                {
                                    Name = vm.Name,
                                    IsSystem = false,
                                    Key = Guid.NewGuid().ToString("n")
                                };
                            }

                            if (m != null)
                            {
                                m.IsDelete = vm.IsDelete;
                                m.Type = vm.Type;
                                m.UpdateTime = vm.UpdateTime;
                                if (m.Id == 0) db.Role.Add(m);
                                count += db.SaveChanges();
                            }
                        }
                        db.AddCommitCallback(() =>
                        {
                            var roleCache = this.GetCache<IRoleCache>();
                            roleCache.Remove();
                        });
                        db.Commit();
                    }
                }
            }

            return count;
        }
    }
}
