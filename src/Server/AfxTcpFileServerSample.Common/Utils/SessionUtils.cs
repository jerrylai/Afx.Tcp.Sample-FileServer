using System;
using System.Collections.Generic;
using System.Text;

namespace AfxTcpFileServerSample.Common
{
    public static class SessionUtils
    {
        [ThreadStatic]
        private static UserInfo _userInfo;
        public static UserInfo UserInfo
        {
            get
            {
                return _userInfo;
            }
            set
            {
                _userInfo = value;
            }
        }

    }
}
