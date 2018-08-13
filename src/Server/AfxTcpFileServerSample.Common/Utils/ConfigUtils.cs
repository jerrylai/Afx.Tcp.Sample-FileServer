using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Afx.Ioc;
using Afx.Utils;
using Afx.Configuration;
using AfxTcpFileServerSample.Enums;

namespace AfxTcpFileServerSample.Common
{
    public static class ConfigUtils
    {
        public const int MAX_FILE_DATA_SIZE = 7 * 1024;

        private static Lazy<XmlConfig> _configuration = new Lazy<XmlConfig>(() =>
          IocUtils.GetSingle<XmlConfig>(), false);

        public static XmlConfig Configuration => _configuration.Value;
        
        public static string TempDirectory
        {
            get
            {
                return Path.Combine(Directory.GetCurrentDirectory(), "temp");
            }
        }

        private static string _redisConfig;
        public static string RedisConfig
        {
            get
            {
                if (string.IsNullOrEmpty(_redisConfig))
                {
                    _redisConfig = Configuration.GetItemAttribute("Root", "Redis", "config");
                    if (string.IsNullOrEmpty(_redisConfig)) throw new ArgumentNullException("RedisConfig");
                }

                return _redisConfig;
            }
        }

        private static string _desKey;
        public static string DesKey
        {
            get
            {
                if (string.IsNullOrEmpty(_desKey))
                {
                    _desKey = Configuration.GetItemAttribute("Root", "Encrypt", "desKey");
                    if (string.IsNullOrEmpty(_desKey)) throw new ArgumentNullException("DesKey");
                }

                return _desKey;
            }
        }

        private static Nullable<int> _port = null;
        public static int Port
        {
            get
            {
                if (!_port.HasValue)
                {
                    int temp = 2100;
                    string s = Configuration.GetItemAttribute("Root", "Server", "port");
                    if (!string.IsNullOrEmpty(s) && int.TryParse(s, out temp))
                    {
                        _port = temp;
                    }
                    else
                    {
                        Port = temp;
                    }
                }

                return _port.Value;
            }
            set
            {
                _port = value;
                Configuration.SetItemAttribute("Root", "Server", "port", _port.Value.ToString());
            }
        }

        #region Database
        private static Nullable<bool> _initDatabase = null;
        public static bool InitDatabase
        {
            get
            {
                if (!_initDatabase.HasValue)
                {
                    bool temp = true;
                    string s = Configuration.GetItemAttribute("Root", "Database", "init");
                    if (!string.IsNullOrEmpty(s) && bool.TryParse(s, out temp))
                    {
                        _initDatabase = temp;
                    }
                    else
                    {
                        _initDatabase = false;
                    }
                }

                return _initDatabase.Value;
            }
        }

        private static Nullable<bool> _isWriteSqlLog = null;
        public static bool IsWriteSqlLog
        {
            get
            {
                if (!_isWriteSqlLog.HasValue)
                {
                    bool temp = false;
                    string s = Configuration.GetItemAttribute("Root", "Database", "isLog");
                    if (!string.IsNullOrEmpty(s) && bool.TryParse(s, out temp))
                    {
                        _isWriteSqlLog = temp;
                    }
                    else
                    {
                        _isWriteSqlLog = false;
                    }
                }

                return _isWriteSqlLog.Value;
            }
        }

        private static DatabaseType _databaseType = DatabaseType.None;
        public static DatabaseType DatabaseType
        {
            get
            {
                if (_databaseType == DatabaseType.None)
                {
                    string s = Configuration.GetItemAttribute("Root", "Database", "type");
                    if (string.IsNullOrEmpty(s) || !Enum.TryParse(s, true, out _databaseType) || _databaseType == DatabaseType.None)
                    {
                        throw new ArgumentNullException("DatabaseType");
                    }
                }

                return _databaseType;
            }
        }

        private static string _connectionString = null;
        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    string key = $"{DatabaseType}";
                    _connectionString = Configuration.GetItemAttribute("Root", key, "connectionString");
                    if (string.IsNullOrEmpty(_connectionString))
                    {
                        throw new ArgumentNullException(key);
                    }
                }

                return _connectionString;
            }
        }
        #endregion

        #region Cache

        private static Nullable<CacheType> _cacheType = null;
        public static CacheType CacheType
        {
            get
            {
                if (!_cacheType.HasValue)
                {
                    var val = CacheType.None;
                    string s = Configuration.GetItemAttribute("Root", "Cache", "type");
                    if (string.IsNullOrEmpty(s) ||  !Enum.TryParse(s, true, out val))
                    {
                        throw new ArgumentNullException("CacheType");
                    }
                    else
                    {
                        _cacheType = val;
                    }
                }

                return _cacheType.Value;
            }
        }

        private static string _cachePrefix;
        public static string CachePrefix
        {
            get
            {
                if (_cachePrefix == null)
                {
                    _cachePrefix = Configuration.GetItemAttribute("Root", "Cache", "prefix") ?? "";
                }

                return _cachePrefix;
            }
        }
        #endregion
        
        public static int LogSaveDay = 7;

        private static string _fileRootPath = null;
        public static string FileRootPath
        {
            get
            {
                if (_fileRootPath == null)
                {
                    string s = Configuration.GetItemAttribute("Root", "FileRoot", "path");
                    _fileRootPath = GetFullPath(s);
                }

                return _fileRootPath;
            }
            set
            {
                if (value != null)
                {
                    _fileRootPath = GetFullPath(value);
                    Configuration.SetItemAttribute("Root", "FileRoot", "path", value);
                }
            }
        }
        
        private static string GetFullPath(string path)
        {
            string s = path;
            if (!string.IsNullOrEmpty(path))
            {
                if (path.StartsWith("~\\"))
                {
                    s = Utils.Combine(Directory.GetCurrentDirectory(), path.Substring(2));
                }
                else if (path.StartsWith("\\"))
                {
                    s = Utils.Combine(Directory.GetCurrentDirectory(), path.Substring(1));
                }
            }

            return s;
        }

        public static void SetThreads()
        {
            int ProcessorCount = Environment.ProcessorCount;
            int minThreads = ProcessorCount * 2;
            int minIoThreads = ProcessorCount * 2;
            int maxThreads = ProcessorCount * 1000;
            int maxIoThreads = 1000;

            if (minThreads < 10) minThreads = 10;
            if (minIoThreads < 10) minIoThreads = 10;
            if (maxThreads < 1000) maxThreads = 1000;

            //var s = GetValue("Threads:Min");
            //if (!string.IsNullOrEmpty(s))
            //{
            //    int temp = 0;
            //    if (int.TryParse(s, out temp) && temp > minThreads)
            //    {
            //        minThreads = temp;
            //    }
            //}

            //s = GetValue("Threads:IO");
            //if (!string.IsNullOrEmpty(s))
            //{
            //    int temp = 0;
            //    if (int.TryParse(s, out temp) && temp > minIoThreads)
            //    {
            //        minIoThreads = temp;
            //    }
            //}

            int workerThreads = 0;
            int completionPortThreads = 0;
            //SetMaxThreads
            System.Threading.ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);
            if (workerThreads < maxThreads) workerThreads = maxThreads;
            if (completionPortThreads < maxIoThreads) completionPortThreads = maxIoThreads;
            System.Threading.ThreadPool.SetMaxThreads(workerThreads, completionPortThreads);
            //SetMinThreads
            System.Threading.ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
            if (workerThreads < minThreads) workerThreads = minThreads;
            if (completionPortThreads < minIoThreads) completionPortThreads = minIoThreads;
            System.Threading.ThreadPool.SetMinThreads(workerThreads, completionPortThreads);
        }
    }
}
