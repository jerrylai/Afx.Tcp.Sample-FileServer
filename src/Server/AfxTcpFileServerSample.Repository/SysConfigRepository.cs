using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using AfxTcpFileServerSample.Models;
using AfxTcpFileServerSample.Enums;
using AfxTcpFileServerSample.IRepository;
using AfxTcpFileServerSample.ICache;
using AfxTcpFileServerSample.Dto.Repository;

namespace AfxTcpFileServerSample.Repository
{
    public class SysConfigRepository : BaseRepository, ISysConfigRepository
    {
        internal readonly static SysConfigRepository Instance = new SysConfigRepository();

        public virtual List<SysConfigDto> GetList(ConfigType type)
        {
            var sysConfigCache = this.GetCache<ISysConfigCache>();
            List<SysConfigDto> list = sysConfigCache.GetOrSet(type, () =>
            {
                using (var db = this.CreateDbContext())
                {
                    var query = from q in db.SysConfig
                                where q.Type == type
                                select new SysConfigDto
                                {
                                    Id = q.Id,
                                    Type = q.Type,
                                    Key = q.Key,
                                    Value = q.Value
                                };

                    return query.ToList();
                }
            });

            return list;
        }

        public virtual int AddOrUpdate(List<SysConfigDto> list, ConfigType type)
        {
            int count = 0;
            if(list != null)
            {
                list = new List<SysConfigDto>(list);
                using(var db = this.CreateDbContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = true;
                    using(db.BeginTransaction(System.Data.IsolationLevel.Serializable))
                    {
                        var old = db.SysConfig.Where(q => q.Type == type).ToList();
                        List<SysConfig> del = new List<SysConfig>();
                        while(old.Count > 0)
                        {
                            var m = old[0];
                            old.Remove(m);
                            var vm = list.Find(q => q.Key == m.Key);
                            if(vm != null)
                            {
                                m.Value = vm.Value;
                                count += db.SaveChanges();
                                list.Remove(vm);
                            }
                            else
                            {
                                del.Add(m);
                            }
                        }

                        while(list.Count > 0)
                        {
                            var vm = list[0];
                            list.Remove(vm);
                            SysConfig m = null;
                            if(del.Count > 0)
                            {
                                m = del[0];
                                del.Remove(m);
                            }
                            else
                            {
                                m = new SysConfig() { Type = type };
                            }
                            m.Key = vm.Key ?? "";
                            m.Value = vm.Value;
                            if (m.Id == 0) db.SysConfig.Add(m);
                            count += db.SaveChanges();
                        }

                        del.ForEach(m => {
                            db.Entry(m).State = System.Data.Entity.EntityState.Deleted;
                            count += db.SaveChanges();
                        });
                        db.AddCommitCallback(() =>
                        {
                            var sysConfigCache = this.GetCache<ISysConfigCache>();
                            sysConfigCache.Remove(type);
                        });
                        db.Commit();
                    }
                }
            }

            return count;
        }

        public virtual int Delete(ConfigType type)
        {
            int count = 0;
            using (var db = this.CreateDbContext())
            {
                using (db.BeginTransaction())
                {
                    string sql = string.Format("delete from {0} where {1}={{0}}", db.GetColumn("SysConfig"), db.GetColumn("Type"));
                    db.AddCommitCallback(() =>
                    {
                        var sysConfigCache = this.GetCache<ISysConfigCache>();
                        sysConfigCache.Remove(type);
                    });
                    count = db.Database.ExecuteSqlCommand(sql, new object[] { type });
                    db.Commit();
                }
            }
            return count;
        }
    }
}
