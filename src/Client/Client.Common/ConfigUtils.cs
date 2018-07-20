using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Afx.Configuration;

namespace Client.Common
{
    public static class ConfigUtils
    {
        private static string _appPath = null;
        public static string AppPath
        {
            get
            {
                if (_appPath == null)
                {
                    string s = AppDomain.CurrentDomain.BaseDirectory;

                    _appPath = s;
                }

                return _appPath;
            }
        }

        public static string ConfigFile
        {
            get
            {
                return Path.Combine(AppPath, "Config\\Config.xml");
            }
        }
        
        private static XmlConfig _config = null;
        public static XmlConfig Config
        {
            get
            {
                if (_config == null)
                {
                    _config = new XmlConfig(ConfigFile, false);
                }

                return _config;
            }
        }

        private static string _serverAddress = null;
        public static string ServerAddress
        {
            get
            {
                if(_serverAddress == null)
                {
                    _serverAddress = Config.GetItemAttribute("Root", "Server", "host");
                }

                return _serverAddress;
            }
            set
            {
                _serverAddress = value;
                Config.SetItemAttribute("Root", "Server", "host", value);
            }
        }

        private static string _desKey = null;
        public static string DesKey
        {
            get
            {
                if (_desKey == null)
                {
                    _desKey = Config.GetItemAttribute("Root", "DesKey", "value");
                }

                return _desKey;
            }
            set
            {
                _desKey = value;
                Config.SetItemAttribute("Root", "DesKey", "value", value);
            }
        }

        private static Nullable<bool> _isRememberPassword = null;
        public static bool IsRememberPassword
        {
            get
            {
                if (_isRememberPassword == null)
                {
                    string s = Config.GetItemAttribute("User", "RememberPassword", "value");
                    _isRememberPassword = (s ?? "").ToLower() == "true";
                }

                return _isRememberPassword.Value;
            }
            set
            {
                _isRememberPassword = value;
                Config.SetItemAttribute("User", "RememberPassword", "value", value.ToString());
            }
        }


        private static string _account = null;
        public static string Account
        {
            get
            {
                if (_account == null)
                {
                    _account = Config.GetItemAttribute("User", "Account", "value");
                }

                return _account;
            }
            set
            {
                _account = value;
                Config.SetItemAttribute("User", "Account", "value", value);
            }
        }

        private static string _password = null;
        public static string Password
        {
            get
            {
                if (_password == null)
                {
                    var s = Config.GetItemAttribute("User", "Password", "value");
                    _password = string.IsNullOrEmpty(s) ? s : Afx.Utils.DesUtils.Decrypt(s);
                }

                return _password;
            }
            set
            {
                _password = value;
                Config.SetItemAttribute("User", "Password", "value", string.IsNullOrEmpty(value) ? value : Afx.Utils.DesUtils.Encrypt(value));
            }
        }

        private static string _localPath = null;
        public static string LocalPath
        {
            get
            {
                if (_localPath == null)
                {
                    _localPath = Config.GetItemAttribute("User", "LocalPath", "value");
                }

                return _localPath;
            }
            set
            {
                _localPath = value;
                Config.SetItemAttribute("User", "LocalPath", "value", value);
            }
        }

        private static Nullable<int> _serverPathId = null;
        public static int ServerPathId
        {
            get
            {
                if (_serverPathId == null)
                {
                    string s = Config.GetItemAttribute("User", "ServerPathId", "value");
                    int temp = 0;
                    int.TryParse(s, out temp);
                    _serverPathId = temp;
                }

                return _serverPathId.Value;
            }
            set
            {
                _serverPathId = value;
                Config.SetItemAttribute("User", "ServerPathId", "value", _serverPathId.Value.ToString());
            }
        }

        public static int LogSaveDay { get { return 7; } }
    }
}
