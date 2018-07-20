using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AfxTcpFileServerSample.Dto;
using AfxTcpFileServerSample.Dto.Repository;
using AfxTcpFileServerSample.IRepository;
using AfxTcpFileServerSample.Models;

namespace AfxTcpFileServerSample.Repository
{
    public class FileInfoSyncRepository : BaseRepository, IFileInfoSyncRepository
    {
        internal readonly static FileInfoSyncRepository Instance = new FileInfoSyncRepository();

        public virtual FileInfoSyncDto Get(int serverId)
        {
            FileInfoSyncDto vm = null;
            if (serverId > 0)
            {
                using (var db = this.CreateDbContext())
                {
                    var query = from q in db.FileInfoSync
                                where q.ServerId == serverId
                                select new FileInfoSyncDto
                                {
                                    ServerId = q.ServerId,
                                    SyncId = q.SyncId,
                                    SyncKey = q.SyncKey,
                                    SyncUpdateTime = q.SyncUpdateTime
                                };

                    vm = query.FirstOrDefault();
                    if (vm == null)
                    {
                        vm = new FileInfoSyncDto()
                        {
                            ServerId = serverId
                        };
                    }
                }
            }

            return vm;
        }

        public virtual int Update(FileInfoSyncDto vm)
        {
            int count = 0;
            if (vm != null && vm.ServerId > 0)
            {
                using (var db = this.CreateDbContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = true;
                    using (db.BeginTransaction(System.Data.IsolationLevel.Serializable))
                    {
                        var m = db.FileInfoSync.Where(q => q.ServerId == vm.ServerId).FirstOrDefault();
                        if(m == null)
                        {
                            m = new SyncInfo()
                            {
                                ServerId = vm.ServerId
                            };
                            db.FileInfoSync.Add(m);
                        }
                        m.SyncId = vm.SyncId;
                        m.SyncKey = vm.SyncKey;
                        m.SyncUpdateTime = DateTime.Now;
                        count += db.SaveChanges();
                        db.Commit();
                        vm.SyncUpdateTime = m.SyncUpdateTime;
                    }
                }
            }

            return count;
        }
    }
}
