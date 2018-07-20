using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AfxTcpFileServerSample.Models;
using AfxTcpFileServerSample.IRepository;
using AfxTcpFileServerSample.ICache;
using AfxTcpFileServerSample.Enums;

namespace AfxTcpFileServerSample.Repository
{
    public class ServerSyncTypeRepository : BaseRepository, IServerSyncTypeRepository
    {
        internal readonly static ServerSyncTypeRepository Instance = new ServerSyncTypeRepository();

        public virtual List<SyncType> GetList(int serverId)
        {
            var serverSyncTypeCache = this.GetCache<IServerSyncTypeCache>();
            List<SyncType> list = serverSyncTypeCache.GetOrSet(serverId, ()=>
            {
                using (var db = this.CreateDbContext())
                {
                    var query = from q in db.ServerSyncType
                                where q.ServerId == serverId
                                select q.Type;

                    return query.ToList();
                }
            });
            

            return list;
        }


        internal protected virtual int AddOrUpdate(int serverId, List<SyncType> list, FileContext db)
        {
            int count = 0;
            if (list != null && serverId > 0)
            {
                list = new List<SyncType>(list);
                db.Configuration.AutoDetectChangesEnabled = true;
                var old = db.ServerSyncType.Where(q => q.ServerId == serverId).ToList();
                List<ServerSyncType> del = new List<ServerSyncType>();
                while (old.Count > 0)
                {
                    var m = old[0];
                    old.Remove(m);
                    if (list.Contains(m.Type))
                    {
                        list.Remove(m.Type);
                    }
                    else
                    {
                        del.Add(m);
                    }
                }
                db.AddCommitCallback(() =>
                {
                    var serverSyncTypeCache = this.GetCache<IServerSyncTypeCache>();
                    serverSyncTypeCache.Remove(serverId);
                });
                while (list.Count > 0)
                {
                    var syncType = list[0];
                    list.Remove(syncType);
                    ServerSyncType m = null;
                    if (del.Count > 0)
                    {
                        m = del[0];
                        del.Remove(m);
                    }
                    else
                    {
                        m = new ServerSyncType()
                        {
                            ServerId = serverId
                        };
                    }
                    m.Type = syncType;
                    if (m.Id == 0) db.ServerSyncType.Add(m);
                    count += db.SaveChanges();
                }

                del.ForEach(m =>
                {
                    db.Entry(m).State = System.Data.Entity.EntityState.Deleted;
                    count += db.SaveChanges();
                });
            }

            return count;
        }
    }
}
