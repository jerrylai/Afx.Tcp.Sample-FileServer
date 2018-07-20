using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using AfxTcpFileServerSample.Dto;
using AfxTcpFileServerSample.Models;
using AfxTcpFileServerSample.Enums;
using AfxTcpFileServerSample.Common;
using AfxTcpFileServerSample.IRepository;
using AfxTcpFileServerSample.Dto.ProtoBuf;

namespace AfxTcpFileServerSample.Repository
{
    public class FileInfoRepository : BaseRepository, IFileInfoRepository
    {
        internal readonly static FileInfoRepository Instance = new FileInfoRepository();

        public virtual int AddOrUpdate(FileInfoDto vm)
        {
            int count = 0;
            if (vm != null)
            {
                using (var db = this.CreateDbContext())
                {
                    using (db.BeginTransaction(IsolationLevel.Serializable))
                    {
                        count += this.AddOrUpdate(vm, db);
                        db.Commit();
                    }
                }
            }

            return count;
        }

        protected virtual int AddOrUpdate(FileInfoDto vm, FileContext db)
        {
            int count = 0;
            if(vm != null)
            {
                vm.Directory = Utils.FormatDirectory(vm.Directory);
                vm.Name = Utils.FormatName(vm.Name);

                db.Configuration.AutoDetectChangesEnabled = true;
                FileInfo m = db.FileInfo.Where(q => q.Directory == vm.Directory && q.Name == vm.Name && q.Type == vm.Type).FirstOrDefault();
                int i = 0;
                if (m == null)
                {
                    m = new FileInfo()
                    {
                        Type = vm.Type,
                        ParentId = vm.ParentId,
                        Directory = vm.Directory ?? "",
                        Name = vm.Name,
                        Key = Guid.NewGuid().ToString("n"),
                        IsDelete = false
                    };
                    i++;
                }
                
                if (m.Length != vm.Length)
                {
                    m.Length = vm.Length;
                    i++;
                }
                if (m.CreationTime != vm.CreationTime)
                {
                    m.CreationTime = vm.CreationTime;
                    i++;
                }
                if (m.LastWriteTime != vm.LastWriteTime)
                {
                    m.LastWriteTime = vm.LastWriteTime;
                    i++;
                }
                if (m.IsDelete != false)
                {
                    m.IsDelete = false;
                    i++;
                }

                if (i > 0)
                {
                    m.CheckStatus = (int)CheckStatusType.None;
                    m.UpdateTime = DateTime.Now;
                    if (m.Id == 0) db.FileInfo.Add(m);
                    count += db.SaveChanges();

                    vm.Id = m.Id;
                    vm.UpdateTime = m.UpdateTime;
                    vm.Key = m.Key;
                    vm.IsDelete = m.IsDelete;
                }
            }

            return count;
        }

        public virtual int AddOrUpdate(List<FileInfoDto> list)
        {
            int count = 0;
            if (list != null && list.Count > 0)
            {
                using (var db = this.CreateDbContext())
                {
                    using (db.BeginTransaction(IsolationLevel.Serializable))
                    {
                        foreach (var vm in list)
                        {
                            count += this.AddOrUpdate(vm, db);
                        }
                        db.Commit();
                    }
                }
            }

            return count;
        }

        public virtual FileInfoDto Get(int id)
        {
            FileInfoDto vm = null;
            if (id > 0)
            {
                using (var db = this.CreateDbContext())
                {
                    var query = from q in db.FileInfo
                                where q.Id == id && q.IsDelete == false
                                select new FileInfoDto
                                {
                                    Id = q.Id,
                                    ParentId = q.ParentId,
                                    Type = q.Type,
                                    Directory = q.Directory,
                                    Name = q.Name,
                                    Length = q.Length,
                                    CreationTime = q.CreationTime,
                                    LastWriteTime = q.LastWriteTime,
                                    Key = q.Key,
                                    UpdateTime = q.UpdateTime,
                                    IsDelete = q.IsDelete,
                                };

                    vm = query.FirstOrDefault();
                }
            }

            return vm;
        }

        public virtual FileInfoDto Get(string directory, string name, FileInfoType type)
        {
            FileInfoDto vm = null;
            if (!string.IsNullOrEmpty(name))
            {
                directory = Utils.FormatDirectory(directory);
                name = Utils.FormatName(name);

                using (var db = this.CreateDbContext())
                {
                    var query = from q in db.FileInfo
                                where q.Directory == directory && q.Name == name && q.Type == type && q.IsDelete == false
                                select new FileInfoDto
                                {
                                    Id = q.Id,
                                    ParentId = q.ParentId,
                                    Type = q.Type,
                                    Directory = q.Directory,
                                    Name = q.Name,
                                    Length = q.Length,
                                    CreationTime = q.CreationTime,
                                    LastWriteTime = q.LastWriteTime,
                                    Key = q.Key,
                                    UpdateTime = q.UpdateTime,
                                    IsDelete = q.IsDelete
                                };

                    vm = query.FirstOrDefault();
                }
            }

            return vm;
        }

        public virtual int Delete(int id)
        {
            int count = 0;
            if (id > 0)
            {
                using (var db = this.CreateDbContext())
                {
                    using (db.BeginTransaction())
                    {
                        db.Configuration.AutoDetectChangesEnabled = true;
                        var m = db.FileInfo.Where(q => q.Id == id && q.IsDelete == false).FirstOrDefault();
                        if(m != null)
                        {
                            m.IsDelete = true;
                            m.CheckStatus = (int)CheckStatusType.None;
                            m.UpdateTime = DateTime.Now;
                            count += db.SaveChanges();
                            db.Commit();
                        }
                    }
                }
            }

            return count;
        }

        public virtual int Delete(string directory, string name, FileInfoType type)
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
                        var m = db.FileInfo.Where(q => q.Directory == directory && q.Name == name && q.Type == type && q.IsDelete == false).FirstOrDefault();
                        if (m != null)
                        {
                            m.IsDelete = false;
                            m.CheckStatus = (int)CheckStatusType.None;
                            m.UpdateTime = DateTime.Now;
                            count += db.SaveChanges();
                            db.Commit();
                        }
                    }
                }
            }

            return count;
        }

        public virtual bool Exist(string directory, string name, FileInfoType type)
        {
            int count = 0;
            if (!string.IsNullOrEmpty(name))
            {
                directory = Utils.FormatDirectory(directory);
                name = Utils.FormatName(name);

                using (var db = this.CreateDbContext())
                {
                    count = db.FileInfo.Where(q => q.Directory == directory && q.Name == name && q.Type == type && q.IsDelete == false).Count();
                }
            }

            return count > 0;
        }

        public virtual PageListDto<FileInfoDto> GetPageList(FileInfoPageParamDto vm)
        {
            PageListDto<FileInfoDto> page = new PageListDto<FileInfoDto>();
            using (var db = this.CreateDbContext())
            {
                var query = from q in db.FileInfo
                            where q.IsDelete == false && q.ParentId == vm.ParentId
                            select new FileInfoDto
                            {
                                Id = q.Id,
                                ParentId = q.ParentId,
                                Type = q.Type,
                                Directory = q.Directory,
                                Name = q.Name,
                                Length = q.Length,
                                CreationTime = q.CreationTime,
                                LastWriteTime = q.LastWriteTime,
                                Key = q.Key,
                                UpdateTime = q.UpdateTime,
                                IsDelete = q.IsDelete,
                            };

                #region where
                if(vm.Type != FileInfoType.None)
                {
                    query = query.Where(q => q.Type == vm.Type);
                }
                if(vm.BeginCreateTime.HasValue)
                {
                    query = query.Where(q => q.CreationTime >= vm.BeginCreateTime.Value);
                }
                if (vm.EndCreateTime.HasValue)
                {
                    query = query.Where(q => q.CreationTime < vm.EndCreateTime.Value.AddSeconds(1));
                }
                if (vm.BeginLastWriteTime.HasValue)
                {
                    query = query.Where(q => q.LastWriteTime >= vm.BeginLastWriteTime.Value);
                }
                if (vm.EndLastWriteTime.HasValue)
                {
                    query = query.Where(q => q.LastWriteTime < vm.EndLastWriteTime.Value.AddSeconds(1));
                }
                if (!string.IsNullOrEmpty(vm.Keyword))
                {
                    query = query.Where(q => q.Name.Contains(vm.Keyword.ToLower()));
                }
                #endregion

                page.TotalCount = query.Count();

                #region Orderby
                switch (vm.Orderby)
                {
                    case "Type":
                        query = vm.IsDesc ? query.OrderByDescending(q => q.Type) : query.OrderBy(q => q.Type);
                        break;
                    case "Name":
                        query = vm.IsDesc ? query.OrderByDescending(q => q.Name) : query.OrderBy(q => q.Name);
                        break;
                    case "Length":
                        query = vm.IsDesc ? query.OrderByDescending(q => q.Length) : query.OrderBy(q => q.Length);
                        break;
                    case "CreationTime":
                        query = vm.IsDesc ? query.OrderByDescending(q => q.CreationTime) : query.OrderBy(q => q.CreationTime);
                        break;
                    case "LastWriteTime":
                        query = vm.IsDesc ? query.OrderByDescending(q => q.LastWriteTime) : query.OrderBy(q => q.LastWriteTime);
                        break;
                    default:
                        query = from q in query orderby q.Type, q.Name select q;
                        break;
                }
                #endregion

                this.GetPage(page, query, vm.Index, vm.Size);
            }

            return page;
        }

        public virtual List<FileInfoDto> GetSyncList(SyncParamDto vm)
        {
            List<FileInfoDto> list = null;
            using(var db = this.CreateDbContext())
            {
                DateTime? startTime = null;
                if(vm.SyncId.HasValue && !string.IsNullOrEmpty(vm.SyncKey))
                {
                    var m = db.FileInfo.Where(q => q.Id == vm.SyncId.Value).FirstOrDefault();
                    if(m != null && m.Key == vm.SyncKey)
                    {
                        startTime = vm.SyncUpdateTime;
                    }
                }

                var query = from q in db.FileInfo
                            select new FileInfoDto
                            {
                                Id = q.Id,
                                ParentId = q.ParentId,
                                Type = q.Type,
                                Directory = q.Directory,
                                Name = q.Name,
                                Length = q.Length,
                                CreationTime = q.CreationTime,
                                LastWriteTime = q.LastWriteTime,
                                Key = q.Key,
                                UpdateTime = q.UpdateTime,
                                IsDelete = q.IsDelete,
                            };

                if (startTime.HasValue)
                {
                    query = query.Where(q => q.UpdateTime > startTime.Value);
                }

                query = query.OrderBy(q => q.UpdateTime);

                list = query.Take(vm.Count).ToList();
            }

            return list;
        }

        public virtual int UpdateSync(FileInfoDto vm)
        {
            int count = 0;
            if (vm != null)
            {
                vm.Directory = Utils.FormatDirectory(vm.Directory);
                vm.Name = Utils.FormatName(vm.Name);
                
                using (var db = this.CreateDbContext())
                {
                    using (db.BeginTransaction(IsolationLevel.Serializable))
                    {
                        db.Configuration.AutoDetectChangesEnabled = true;
                        FileInfo m = db.FileInfo.Where(q => q.Directory == vm.Directory && q.Name == vm.Name && q.Type == vm.Type).FirstOrDefault();
                        if (m == null)
                        {
                            m = new FileInfo()
                            {
                                Type = vm.Type,
                                ParentId = vm.ParentId,
                                Directory = vm.Directory,
                                Name = vm.Name,
                                Key = Guid.NewGuid().ToString("n")
                            };
                        }

                        m.Length = vm.Length;
                        m.CreationTime = vm.CreationTime;
                        m.LastWriteTime = vm.LastWriteTime;
                        m.IsDelete = vm.IsDelete;
                        m.UpdateTime = vm.UpdateTime;
                        m.CheckStatus = (int)CheckStatusType.None;

                        if (m.Id == 0) db.FileInfo.Add(m);

                        count += db.SaveChanges();
                        db.Commit();
                    }
                }
            }

            return count;
        }

        public virtual int ResetCheckStatus()
        {
            int count = 0;
            using(var db = this.CreateDbContext())
            {
                using(db.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    string sql = string.Format("update {0} set {1}={{0}} where {2}={{1}}", db.GetColumn("FileInfo"), db.GetColumn("CheckStatus"), db.GetColumn("IsDelete"));
                    count = db.Database.ExecuteSqlCommand(sql, new object[] { (int)CheckStatusType.Check, 0 });
                    db.Commit();
                }
            }

            return count;
        }

        public virtual List<FileInfoDto> GetCheckStatusList(int count)
        {
            List<FileInfoDto> list = null;
            using (var db = this.CreateDbContext())
            {
                var query = from q in db.FileInfo
                            where q.CheckStatus == CheckStatusType.Check
                            orderby q.Id
                            select new FileInfoDto
                            {
                                Id = q.Id,
                                ParentId = q.ParentId,
                                Type = q.Type,
                                Directory = q.Directory,
                                Name = q.Name,
                                Length = q.Length,
                                CreationTime = q.CreationTime,
                                LastWriteTime = q.LastWriteTime,
                                Key = q.Key,
                                UpdateTime = q.UpdateTime,
                                IsDelete = q.IsDelete,
                            };
                
                list = query.Take(count).ToList();
            }

            return list;
        }

        public virtual int DeleteCheckStatus()
        {
            int count = 0;
            using (var db = this.CreateDbContext())
            {
                using (db.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    string sql = string.Format("update {0} set {1}={{0}},{2}={{1}},{3}={{2}} where {1}={{3}}", db.GetColumn("FileInfo"), db.GetColumn("CheckStatus"), db.GetColumn("IsDelete"), db.GetColumn("UpdateTime"));
                    count = db.Database.ExecuteSqlCommand(sql, new object[] { (int)CheckStatusType.None, 1, DateTime.Now, (int)CheckStatusType.Check });
                    db.Commit();
                }
            }

            return count;
        }

        public virtual int UpdateCheckStatus(int id)
        {
            int count = 0;
            using (var db = this.CreateDbContext())
            {
                using (db.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    string sql = string.Format("update {0} set {1}={{0}} where {2}={{1}} and {1}={{2}}", db.GetColumn("FileInfo"), db.GetColumn("CheckStatus"), db.GetColumn("Id"));
                    count = db.Database.ExecuteSqlCommand(sql, new object[] { (int)CheckStatusType.None, id, (int)CheckStatusType.Check });
                    db.Commit();
                }
            }

            return count;
        }
    }
}
