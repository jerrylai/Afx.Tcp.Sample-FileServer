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
    public class TempFileRepository : BaseRepository, ITempFileRepository
    {
        internal readonly static TempFileRepository Instance = new TempFileRepository();

        public virtual TempFileDto Get(string directory, string name)
        {
            TempFileDto vm = null;
            if (!string.IsNullOrEmpty(name))
            {
                directory = Utils.FormatDirectory(directory);
                name = Utils.FormatName(name);

                using(var db = this.CreateDbContext())
                {
                    var query = from q in db.TempFile
                                where q.Directory == directory && q.Name == name
                                select new TempFileDto
                                {
                                    Id = q.Id,
                                    Directory = q.Directory,
                                    Name = q.Name,
                                    Length = q.Length,
                                    CreationTime = q.CreationTime,
                                    LastWriteTime = q.LastWriteTime,
                                    TempIndex = q.TempIndex,
                                    TempName = q.TempName,
                                    UpdateTime = q.UpdateTime
                                };
                    vm = query.FirstOrDefault();
                }
            }

            return vm;
        }

        public virtual int AddOrUpdate(TempFileDto vm)
        {
            int count = 0;
            if(vm != null && !string.IsNullOrEmpty(vm.Name))
            {
                vm.Directory = Utils.FormatDirectory(vm.Directory);
                vm.Name = Utils.FormatName(vm.Name);

                using (var db = this.CreateDbContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = true;
                    using(db.BeginTransaction(System.Data.IsolationLevel.Serializable))
                    {
                        var m = db.TempFile.Where(q => q.Directory == vm.Directory && q.Name == vm.Name).FirstOrDefault();
                        if (m == null) m = new TempFile();
                        m.Directory = vm.Directory;
                        m.Name = vm.Name;
                        m.Length = vm.Length;
                        m.CreationTime = vm.CreationTime;
                        m.LastWriteTime = vm.LastWriteTime;
                        m.TempIndex = vm.TempIndex;
                        m.TempName = vm.TempName;
                        m.UpdateTime = DateTime.Now;
                        if (m.Id == 0) db.TempFile.Add(m);
                        count += db.SaveChanges();
                        db.Commit();
                        vm.Id = m.Id;
                        vm.UpdateTime = m.UpdateTime;
                    }
                }
            }

            return count;
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
                        var m = db.TempFile.Where(q => q.Id == id).FirstOrDefault();
                        if(m != null)
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

        public virtual int Delete(string directory, string name)
        {
            int count = 0;
            if (!string.IsNullOrEmpty(name))
            {
                directory = Utils.FormatDirectory(directory);
                name = Utils.FormatName(name);
                using (var db = this.CreateDbContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = true;
                    using (db.BeginTransaction())
                    {
                        var m = db.TempFile.Where(q => q.Directory == directory && q.Name == name).FirstOrDefault();
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
    }
}
