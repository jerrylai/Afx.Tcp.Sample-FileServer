using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;

using AfxTcpFileServerSample.Enums;
using AfxTcpFileServerSample.Common;

namespace AfxTcpFileServerSample.Models
{
    public class InitDb
    {
        internal static DbConnection GetConnection(string connectionString, DatabaseType dbType)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("connectionString is null!");
            }
            DbConnection dbConnection = null;
            switch (dbType)
            {
                case DatabaseType.MsSQLServer:
                    dbConnection = new System.Data.SqlClient.SqlConnection(connectionString);
                    break;
                case DatabaseType.MySQL:
                    dbConnection = new global::MySql.Data.MySqlClient.MySqlConnection(connectionString);
                    break;
                case DatabaseType.SQLite:
                    dbConnection = new System.Data.SQLite.SQLiteConnection(connectionString);
                    break;
                default:
                    throw new Exception("DatabaseType is error!");
            }

            return dbConnection;
        }

        public static bool Exists(string connectionString, DatabaseType dbType)
        {
            Afx.Data.Entity.Schema.DatabaseSchema databaseSchema = null;
            switch (dbType)
            {
                case DatabaseType.MsSQLServer:
                    databaseSchema = new Afx.Data.MSSQLServer.Entity.Schema.MsSqlDatabaseSchema(connectionString);
                    break;
                case DatabaseType.MySQL:
                    databaseSchema = new Afx.Data.MySql.Entity.Schema.MySqlDatabaseSchema(connectionString);
                    break;
                case DatabaseType.SQLite:
                    databaseSchema = new Afx.Data.SQLite.Entity.Schema.SQLiteDatabaseSchema(connectionString);
                    break;
                case DatabaseType.Oracle:
                    databaseSchema = new Afx.Data.Oracle.Entity.Schema.OracleDatabaseSchema(connectionString);
                    break;
                default:
                    throw new Exception("DatabaseType is error!");
            }
            using(databaseSchema)
            {
                return databaseSchema.Exist();
            }
        }

        private static object lockObj = new object();
        internal static void Initialize(FileContext context, Type dbContextType)
        {
            if (!ConfigUtils.InitDatabase)
            {
                return;
            }

            lock (lockObj)
            {
                if (!AfxTcpFileServerSample.Common.ConfigUtils.InitDatabase)
                {
                    return;
                }
                context.WriteSQL("-- Initialize Database begin");
                try
                {
                    Afx.Data.Entity.Schema.DatabaseSchema databaseSchema = null;
                    Afx.Data.Entity.Schema.TableSchema tableSchema = null;
                    switch (ConfigUtils.DatabaseType)
                    {
                        case DatabaseType.MsSQLServer:
                            databaseSchema = new Afx.Data.MSSQLServer.Entity.Schema.MsSqlDatabaseSchema(ConfigUtils.ConnectionString);
                            tableSchema = new Afx.Data.MSSQLServer.Entity.Schema.MsSqlTableSchema(ConfigUtils.ConnectionString);
                            break;
                        case DatabaseType.MySQL:
                            databaseSchema = new Afx.Data.MySql.Entity.Schema.MySqlDatabaseSchema(ConfigUtils.ConnectionString);
                            tableSchema = new Afx.Data.MySql.Entity.Schema.MySqlTableSchema(ConfigUtils.ConnectionString);
                            break;
                        case DatabaseType.SQLite:
                            databaseSchema = new Afx.Data.SQLite.Entity.Schema.SQLiteDatabaseSchema(ConfigUtils.ConnectionString);
                            tableSchema = new Afx.Data.SQLite.Entity.Schema.SQLiteTableSchema(ConfigUtils.ConnectionString);
                            break;
                        case DatabaseType.Oracle:
                            databaseSchema = new Afx.Data.Oracle.Entity.Schema.OracleDatabaseSchema(ConfigUtils.ConnectionString);
                            tableSchema = new Afx.Data.Oracle.Entity.Schema.OracleTableSchema(ConfigUtils.ConnectionString);
                            break;
                        default:
                            throw new Exception("DatabaseType is error!");
                    }

                    if (databaseSchema != null && tableSchema != null)
                    {
                        databaseSchema.Log = context.WriteSQL;
                        tableSchema.Log = context.WriteSQL;
                        using (var builder = new Afx.Data.Entity.Schema.BuildDatabase(databaseSchema, tableSchema))
                        {
                            builder.Build<FileContext>();
                        }
                    }

                    context.WriteSQL("-- InitBaseData begin");
                    new InitDb().InitBaseData(context);
                    context.WriteSQL("-- InitBaseData end");
                    context.WriteSQL("-- Initialize Database end");
                }
                catch (Exception ex)
                {
                    LogUtils.Error("【InitDataBase】", ex);
                }
            }
        }

        protected virtual void InitBaseData(FileContext context)
        {
            using (context.BeginTransaction(System.Data.IsolationLevel.Serializable))
            {
                if (context.Role.Count(q => q.Type == RoleType.Admin && q.IsSystem == true) == 0)
                {
                    Role m = new Role()
                    {
                        Type = RoleType.Admin,
                        Name = "系统管理",
                        IsSystem = true,
                        Key = Guid.NewGuid().ToString("n"),
                        IsDelete = false,
                        UpdateTime = DateTime.Now
                    };
                    context.Role.Add(m);
                    context.SaveChanges();
                }

                var admin_role = context.Role.Where(q => q.Type == RoleType.Admin && q.IsSystem == true).FirstOrDefault();
                if(admin_role != null)
                {
                    var list = context.RoleAuth.Where(q => q.RoleId == admin_role.Id).Select(q => q.Type).ToList();
                    if(list.Count(q=>q == AuthType.ReadFile) == 0)
                    {
                        RoleAuth m = new RoleAuth()
                        {
                            RoleId = admin_role.Id,
                            Type = AuthType.ReadFile
                        };
                        context.RoleAuth.Add(m);
                        context.SaveChanges();
                    }
                    if (list.Count(q => q == AuthType.WriteFile) == 0)
                    {
                        RoleAuth m = new RoleAuth()
                        {
                            RoleId = admin_role.Id,
                            Type = AuthType.WriteFile
                        };
                        context.RoleAuth.Add(m);
                        context.SaveChanges();
                    }
                    if (list.Count(q => q == AuthType.System) == 0)
                    {
                        RoleAuth m = new RoleAuth()
                        {
                            RoleId = admin_role.Id,
                            Type = AuthType.System
                        };
                        context.RoleAuth.Add(m);
                        context.SaveChanges();
                    }
                }

                if (context.Role.Count(q => q.Type == RoleType.User && q.IsSystem == true) == 0)
                {
                    Role m = new Role()
                    {
                        Type = RoleType.User,
                        Name = "文件管理",
                        IsSystem = true,
                        Key = Guid.NewGuid().ToString("n"),
                        IsDelete = false,
                        UpdateTime = DateTime.Now
                    };
                    context.Role.Add(m);
                    context.SaveChanges();
                }

                var file_role = context.Role.Where(q => q.Type == RoleType.User && q.IsSystem == true).FirstOrDefault();
                if (file_role != null)
                {
                    var list = context.RoleAuth.Where(q => q.RoleId == file_role.Id).Select(q => q.Type).ToList();
                    if (list.Count(q => q == AuthType.ReadFile) == 0)
                    {
                        RoleAuth m = new RoleAuth()
                        {
                            RoleId = file_role.Id,
                            Type = AuthType.ReadFile
                        };
                        context.RoleAuth.Add(m);
                        context.SaveChanges();
                    }
                    if (list.Count(q => q == AuthType.WriteFile) == 0)
                    {
                        RoleAuth m = new RoleAuth()
                        {
                            RoleId = file_role.Id,
                            Type = AuthType.WriteFile
                        };
                        context.RoleAuth.Add(m);
                        context.SaveChanges();
                    }
                }

                if (admin_role != null && context.User.Count(q => q.IsSystem == true && q.Account == "admin") == 0)
                {
                    User m = new User()
                    {
                        Account = "admin",
                        Name = "管理员",
                        Password = "",
                        RoleId = admin_role.Id,
                        IsSystem = true,
                        IsDelete = false,
                        UpdateTime = DateTime.Now
                    };
                    context.User.Add(m);
                    context.SaveChanges();
                }

                if (admin_role != null && context.User.Where(q => q.IsSystem == true && q.Account == "sync").Count() == 0)
                {
                    User m = new User()
                    {
                        Account = "sync",
                        Name = "数据同步",
                        Password = "",
                        RoleId = admin_role.Id,
                        IsSystem = true,
                        IsDelete = false,
                        UpdateTime = DateTime.Now
                    };
                    context.User.Add(m);
                    context.SaveChanges();
                }

                context.Commit();
            }
        }
    }
}
