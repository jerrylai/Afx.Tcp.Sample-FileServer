using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using AfxTcpFileServerSample.Dto;
using AfxTcpFileServerSample.Models;
using AfxTcpFileServerSample.Enums;
using AfxTcpFileServerSample.IRepository;

namespace AfxTcpFileServerSample.Repository
{
    public class StatusLockRepository : BaseRepository, IStatusLockRepository
    {
        internal readonly static StatusLockRepository Instance = new StatusLockRepository();

        public virtual bool Lock(StatusLockType type, string key, string owner, int timeoutSecond)
        {
            bool result = false;
            key = key ?? "";
            owner = owner ?? "";
            using(var db = this.CreateDbContext())
            {
                db.Configuration.AutoDetectChangesEnabled = true;
                using(db.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    DateTime now = DateTime.Now;
                    var m = db.StatusLock.Where(q => q.Type == type && q.Key == key).FirstOrDefault();
                    if(m == null)
                    {
                        m = new StatusLock()
                        {
                            Type = type,
                            Key = key,
                            IsLock = false
                        };
                    }
                    if(m.IsLock == false || m.Owner == owner || m.Timeout < now)
                    {
                        m.IsLock = true;
                        m.Owner = owner;
                        m.Timeout = now.AddSeconds(timeoutSecond);
                        m.UpdateTime = now;
                        result = true;
                    }

                    if (m.Id == 0) db.StatusLock.Add(m);
                    db.SaveChanges();
                    db.Commit();
                }
            }

            return result;
        }

        public virtual bool Release(StatusLockType type, string key)
        {
            bool result = false;
            key = key ?? "";
            using (var db = this.CreateDbContext())
            {
                db.Configuration.AutoDetectChangesEnabled = true;
                using (db.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    DateTime now = DateTime.Now;
                    var m = db.StatusLock.Where(q => q.Type == type && q.Key == key).FirstOrDefault();
                    if (m == null)
                    {
                        m = new StatusLock()
                        {
                            Type = type,
                            Key = key
                        };
                    }

                    m.IsLock = false;
                    m.Owner = "";
                    m.Timeout = now;
                    m.UpdateTime = now;
                    result = true;

                    if (m.Id == 0) db.StatusLock.Add(m);
                    db.SaveChanges();
                    db.Commit();
                }
            }

            return result;
        }

        public virtual bool IsLock(StatusLockType type, string key)
        {
            bool result = false;
            key = key ?? "";
            using (var db = this.CreateDbContext())
            {
                DateTime now = DateTime.Now;
                var m = db.StatusLock.Where(q => q.Type == type && q.Key == key).FirstOrDefault();
                if(m != null && m.IsLock == true && m.Timeout > now)
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
