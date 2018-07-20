using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AfxTcpFileServerSample.Models;
using AfxTcpFileServerSample.IRepository;
using AfxTcpFileServerSample.ICache;
using AfxTcpFileServerSample.Dto.ProtoBuf;
using AfxTcpFileServerSample.Enums;

namespace AfxTcpFileServerSample.Repository
{
    public class UpdateInfoRepository : BaseRepository, IUpdateInfoRepository
    {
        internal readonly static UpdateInfoRepository Instance = new UpdateInfoRepository();

        protected virtual List<UpdateInfoDto> GetList()
        {
            var updateInfoCache = this.GetCache<IUpdateInfoCache>();
            List<UpdateInfoDto> list = updateInfoCache.GetOrSet(() =>
            {
                using (var db = this.CreateDbContext())
                {
                    var query = from q in db.UpdateInfo
                                select new UpdateInfoDto
                                {
                                    Id = q.Id,
                                    Type = q.Type,
                                    Version = q.Version,
                                    FilePath = q.FilePath,
                                    UpdateTime = q.UpdateTime
                                };

                    return query.ToList();
                }
            });

            return list;
        }

        public virtual UpdateInfoDto Get(UpdateInfoType type)
        {
            UpdateInfoDto vm = null;
            var list = GetList();
            var query = from q in list
                        where q.Type == type
                        select q;

            vm = query.FirstOrDefault();

            return vm;
        }

        public virtual int AddOrUpdate(UpdateInfoDto vm)
        {
            int count = 0;
            if(vm != null)
            {
                using (var db = this.CreateDbContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = true;
                    using (db.BeginTransaction(System.Data.IsolationLevel.Serializable))
                    {
                        var m = db.UpdateInfo.Where(q => q.Type == vm.Type).FirstOrDefault();
                        if (m == null) m = new UpdateInfo();
                        m.Type = vm.Type;
                        m.Version = vm.Version;
                        m.FilePath = vm.FilePath;
                        m.UpdateTime = DateTime.Now;
                        if (m.Id == 0) db.UpdateInfo.Add(m);
                        db.AddCommitCallback(() =>
                        {
                            var updateInfoCache = this.GetCache<IUpdateInfoCache>();
                            updateInfoCache.Remove();
                        });
                        count += db.SaveChanges();
                        db.Commit();
                        vm.Id = m.Id;
                        vm.UpdateTime = m.UpdateTime;
                    }
                }
            }

            return count;
        }

        public virtual int Delete(UpdateInfoType type)
        {
            int count = 0;
            using (var db = this.CreateDbContext())
            {
                db.Configuration.AutoDetectChangesEnabled = true;
                using (db.BeginTransaction())
                {
                    var m = db.UpdateInfo.Where(q => q.Type == type).FirstOrDefault();
                    if (m != null)
                    {
                        db.Entry(m).State = System.Data.Entity.EntityState.Deleted;
                        db.AddCommitCallback(()=>
                        {
                            var updateInfoCache = this.GetCache<IUpdateInfoCache>();
                            updateInfoCache.Remove();
                        });
                        count += db.SaveChanges();
                        db.Commit();
                    }
                }
            }

            return count;
        }
    }
}
