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
    public class RoleAuthRepository : BaseRepository, IRoleAuthRepository
    {
        internal readonly static RoleAuthRepository Instance = new RoleAuthRepository();
        
        public virtual List<AuthType> GetList(int roleId)
        {
            List<AuthType> list = null;
            using (var db = this.CreateDbContext())
            {
                list = this.GetList(roleId, db);
            }

            return list;
        }

        internal protected virtual List<AuthType> GetList(int roleId, FileContext db)
        {
            var roleAuthCache = this.GetCache<IRoleAuthCache>();
            List<AuthType> list = roleAuthCache.GetOrSet(roleId, () =>
            {
                var query = from q in db.RoleAuth
                            where q.RoleId == roleId
                            select q.Type;

                return query.ToList();
            });

            return list;
        }

        internal protected virtual int AddOrUpdate(int roleId, List<AuthType> list, FileContext db)
        {
            int count = 0;
            if (list != null && roleId > 0)
            {
                list = new List<AuthType>(list);
                db.Configuration.AutoDetectChangesEnabled = true;
                var old = db.RoleAuth.Where(q => q.RoleId == roleId).ToList();
                List<RoleAuth> del = new List<RoleAuth>();
                while(old.Count > 0)
                {
                    var m = old[0];
                    old.Remove(m);
                    if(list.Contains(m.Type))
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
                    this.GetCache<IRoleAuthCache>().Remove(roleId);
                });
                while(list.Count > 0)
                {
                    var auth = list[0];
                    list.Remove(auth);
                    RoleAuth m = null;
                    if(del.Count > 0)
                    {
                        m = del[0];
                        del.Remove(m);
                    }
                    else
                    {
                        m = new RoleAuth()
                        {
                            RoleId = roleId
                        };
                    }
                    m.Type = auth;
                    if (m.Id == 0) db.RoleAuth.Add(m);
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

        //public int Update(int roleId, List<AuthType> list)
        //{
        //    int count = 0;
        //    if (list != null && roleId > 0)
        //    {
        //        using(var db = this.CreateFileContext())
        //        {
        //            using(db.BeginTransaction())
        //            {
        //                count = this.Update(roleId, list, db);
        //            }
        //        }
        //    }

        //    return count;
        //}
    }
}
