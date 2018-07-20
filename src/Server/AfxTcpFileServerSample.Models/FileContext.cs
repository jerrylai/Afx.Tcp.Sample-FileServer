using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

using Afx.Data.Entity;
using Afx.Data.SQLite.Entity;
using System.Data.SQLite;
using AfxTcpFileServerSample.Enums;
using AfxTcpFileServerSample.Common;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace AfxTcpFileServerSample.Models
{
    public class FileContext : EntityContext, IDisposable
    {
        static FileContext()
        {
            Database.SetInitializer<FileContext>(null);
        }

        public DatabaseType DatabaseType { get; protected set; }
        public string ConnectionString { get; protected set; }

        public FileContext()
            : base(InitDb.GetConnection(ConfigUtils.ConnectionString, ConfigUtils.DatabaseType))
        {
            this.DatabaseType = ConfigUtils.DatabaseType;
            this.ConnectionString = ConfigUtils.ConnectionString;

            this.Database.Log = this.WriteSQL;
            //this.Configuration.UseDatabaseNullSemantics = true;

            InitDb.Initialize(this, typeof(FileContext));
        }
        
        internal void WriteSQL(string s)
        {
            if (ConfigUtils.IsWriteSqlLog)
            {
                LogUtils.Debug("【SQL】" + s);
            }
        }

        /// <summary>
        /// 转义关键字列名、表名
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public virtual string GetColumn(string column)
        {
            switch (DatabaseType)
            {
                case DatabaseType.MsSQLServer:
                case DatabaseType.SQLite:
                    return string.Format("[{0}]", column);
                case DatabaseType.MySQL:
                    return string.Format("`{0}`", column);
                case DatabaseType.Oracle:
                    return string.Format("\"{0}\"", column);
                default:
                    return column;
            }
        }

        /// <summary>
        /// 获取参数名
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual string GetParamter(string name)
        {
            switch (DatabaseType)
            {
                case DatabaseType.MsSQLServer:
                case DatabaseType.SQLite:
                    return string.Format("@{0}", name);
                case DatabaseType.MySQL:
                    return string.Format("?{0}", name);
                case DatabaseType.Oracle:
                    return string.Format(":{0}", name);
                default:
                    return name;
            }
        }

        private void SetPropertyDefaultValue()
        {
            var now = DateTime.Now;
            foreach (var m in this.ChangeTracker.Entries())
            {
                if (m.State == EntityState.Added && m.Entity is ICreateTime)
                {
                    m.Property(nameof(ICreateTime.CreateTime)).CurrentValue = now;
                }
                else if (m.State == EntityState.Deleted && m.Entity is IIsDelete)
                {
                    //逻辑删除
                    m.State = EntityState.Unchanged;
                    var p = m.Property(nameof(IIsDelete.IsDelete));
                    p.CurrentValue = true;
                    p.IsModified = true;
                }

                if ((m.State == EntityState.Added || m.State == EntityState.Modified)
                    && m.Entity is IUpdateTime)
                {
                    m.Property(nameof(IUpdateTime.UpdateTime)).CurrentValue = now;
                }
            }
        }

        public override int SaveChanges()
        {
            this.SetPropertyDefaultValue();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            this.SetPropertyDefaultValue();
            return base.SaveChangesAsync(cancellationToken);
        }

        #region 表对应实体

        public virtual DbSet<StatusLock> StatusLock { get; set; }

        public virtual DbSet<SysConfig> SysConfig { get; set; }

        public virtual DbSet<Role> Role { get; set; }

        public virtual DbSet<RoleAuth> RoleAuth { get; set; }

        public virtual DbSet<User> User { get; set; }

        public virtual DbSet<FileInfo> FileInfo { get; set; }

        public virtual DbSet<SyncInfo> FileInfoSync { get; set; }

        public virtual DbSet<ReadPathInfo> ReadPathInfo { get; set; }

        public virtual DbSet<ServerInfo> ServerInfo { get; set; }

        public virtual DbSet<ServerSyncType> ServerSyncType { get; set; }

        public virtual DbSet<TempFile> TempFile { get; set; }

        public virtual DbSet<UpdateInfo> UpdateInfo { get; set; }

        public virtual DbSet<OptionLog> OptionLog { get; set; }

        #endregion
    }
}
