using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AfxTcpFileServerSample.Models;
using AfxTcpFileServerSample.Common;
using AfxTcpFileServerSample.IRepository;
using AfxTcpFileServerSample.Dto.Repository;

namespace AfxTcpFileServerSample.Repository
{
    public class ReadPathInfoRepository : BaseRepository, IReadPathInfoRepository
    {
        internal readonly static ReadPathInfoRepository Instance = new ReadPathInfoRepository();

        public virtual int Add(List<ReadPathDto> list)
        {
            int count = 0;
            if(list != null && list.Count > 0)
            {
                using(var db = this.CreateDbContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = false;
                    using (db.BeginTransaction(System.Data.IsolationLevel.Serializable))
                    {
                        foreach(var vm in list)
                        {
                            vm.Path = Utils.FormatDirectory(vm.Path);
                            
                            if (!string.IsNullOrEmpty(vm.Path) && vm.Path != "\\")
                            {
                                var m = db.ReadPathInfo.Where(q => q.Path == vm.Path).FirstOrDefault();
                                if (m == null)
                                {
                                    m = new ReadPathInfo()
                                    {
                                        Path = vm.Path
                                    };
                                    db.ReadPathInfo.Add(m);
                                    count += db.SaveChanges();
                                }
                            }
                        }

                        db.Commit();
                    }
                }
            }

            return count;
        }

        public virtual List<ReadPathDto> GetList(int count)
        {
            List<ReadPathDto> list = null;
            using(var db = this.CreateDbContext())
            {
                var query = from q in db.ReadPathInfo
                            select new ReadPathDto
                            {
                                Id = q.Id,
                                Path = q.Path
                            };

                list = query.Take(count).ToList();
            }

            return list;
        }

        public virtual int Delete(int id)
        {
            int count = 0;
            if(id > 0)
            {
                using (var db = this.CreateDbContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = true;
                    using (db.BeginTransaction())
                    {
                            var m = db.ReadPathInfo.Where(q => q.Id == id).FirstOrDefault();
                            if (m != null)
                            {
                                db.Entry(m).State = System.Data.Entity.EntityState.Deleted;
                                count += db.SaveChanges();
                                db.Commit();
                            }
                    }
                }
            }

            return count;
        }

        public virtual int Count()
        {
            int count = 0;
            using (var db = this.CreateDbContext())
            {
                count = db.ReadPathInfo.Count();
            }

            return count;
        }
    }
}
