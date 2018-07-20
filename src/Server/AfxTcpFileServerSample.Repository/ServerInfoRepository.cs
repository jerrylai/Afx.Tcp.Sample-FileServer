using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AfxTcpFileServerSample.Models;
using AfxTcpFileServerSample.IRepository;
using AfxTcpFileServerSample.ICache;
using AfxTcpFileServerSample.Dto.ProtoBuf;

namespace AfxTcpFileServerSample.Repository
{
    public class ServerInfoRepository : BaseRepository, IServerInfoRepository
    {
        internal readonly static ServerInfoRepository Instance = new ServerInfoRepository();
        
        public virtual List<ServerInfoDto> GetList()
        {
            var serverInfoCache = this.GetCache<IServerInfoCache>();
            List<ServerInfoDto> list = serverInfoCache.GetOrSet(() =>
            {
                using (var db = this.CreateDbContext())
                {
                    var query = from q in db.ServerInfo
                                where q.IsDelete == false
                                select new ServerInfoDto
                                {
                                    Id = q.Id,
                                    Port = q.Port,
                                    Address = q.Address,
                                    Account = q.Account,
                                    Password = q.Password,
                                    UpdateTime = q.UpdateTime,
                                };

                    return query.ToList();
                }
            });

            return list;
        }

        public virtual ServerInfoDto Get(int id)
        {
            var m = this.GetList().Find(q => q.Id == id);

            return m;
        }

        public virtual int Add(ServerInfoDto vm)
        {
            int count = 0;
            if(vm != null)
            {
                using(var db = this.CreateDbContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = true;
                    using (db.BeginTransaction(System.Data.IsolationLevel.Serializable))
                    {
                        var m = db.ServerInfo.Where(q => q.Address == vm.Address).FirstOrDefault();
                        if(m == null || m.IsDelete == true)
                        {
                            if(m == null)
                            {
                                m = new ServerInfo();
                            }
                            m.Address = vm.Address;
                            m.Port = vm.Port;
                            m.Account = vm.Account;
                            m.Password = vm.Password;
                            m.IsDelete = false;
                            m.UpdateTime = DateTime.Now;
                            if (m.Id == 0) db.ServerInfo.Add(m);
                            count += db.SaveChanges();
                            vm.Id = m.Id;
                            if (vm.SyncList != null)
                            {
                               ServerSyncTypeRepository.Instance.AddOrUpdate(m.Id, vm.SyncList, db);
                            }
                            db.AddCommitCallback(() =>
                            {
                                var serverInfoCache = this.GetCache<IServerInfoCache>();
                                serverInfoCache.Remove();
                            });
                            db.Commit();
                            vm.UpdateTime = m.UpdateTime;
                        }
                    }
                }
            }

            return count;
        }

        public virtual int Update(ServerInfoDto vm)
        {
            int count = 0;
            if (vm != null && vm.Id > 0)
            {
                using (var db = this.CreateDbContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = true;
                    using (db.BeginTransaction(System.Data.IsolationLevel.Serializable))
                    {
                        var m = db.ServerInfo.Where(q => q.Id == vm.Id && q.IsDelete == false).FirstOrDefault();
                        if (m == null)
                        {
                            m.Address = vm.Address;
                            m.Port = vm.Port;
                            m.Account = vm.Account;
                            if (!string.IsNullOrEmpty(vm.Password))
                            {
                                m.Password = vm.Password;
                            }
                            m.UpdateTime = DateTime.Now;
                            db.AddCommitCallback(() =>
                            {
                                var serverInfoCache = this.GetCache<IServerInfoCache>();
                                serverInfoCache.Remove();
                            });
                            count += db.SaveChanges();
                            if (vm.SyncList != null)
                            {
                                ServerSyncTypeRepository.Instance.AddOrUpdate(m.Id, vm.SyncList, db);
                            }
                            db.Commit();
                            vm.UpdateTime = m.UpdateTime;
                        }
                    }
                }
            }

            return count;
        }

        public virtual int Delete(int id)
        {
            int count = 0;
            if (id > 0)
            {
                using (var db = this.CreateDbContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = true;
                    using (db.BeginTransaction())
                    {
                        var m = db.ServerInfo.Where(q => q.Id == id && q.IsDelete == false).FirstOrDefault();
                        if (m == null)
                        {
                            m.IsDelete = true;
                            m.UpdateTime = DateTime.Now;
                            db.AddCommitCallback(() =>
                            {
                                var serverInfoCache = this.GetCache<IServerInfoCache>();
                                serverInfoCache.Remove();
                            });
                            count += db.SaveChanges();
                            db.Commit();
                        }
                    }
                }
            }

            return count;
        }

        public virtual bool Exist(string address)
        {
            bool result = this.GetList().Find(q => q.Address == address) != null;

            return result;
        }

    }
}
