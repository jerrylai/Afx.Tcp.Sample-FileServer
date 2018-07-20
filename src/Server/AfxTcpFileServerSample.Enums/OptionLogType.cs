using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AfxTcpFileServerSample.Enums
{
    public enum OptionLogType : int
    {
        None = 0,

        Login = 1001,
        Logout = 1002,
        UpdatePassword = 1003,

        AddRole = 1101,
        UpdateRole = 1102,
        DeleteRole = 1103,

        AddUser = 1201,
        UpdateUser = 1202,
        DeleteUser = 1203,

        AddServerInfo = 1301,
        UpdateServerInfo = 1302,
        DeleteServerInfo = 1303,

        CreateFile = 1401,
        CreateFileCompleted = 1402,
        CloseCreateFile = 1403,
        OpenFile = 1404,
        CloseOpenFile = 1405,
        DeleteFile = 1406,
        DeleteOpenFile = 1407,

        CreateDirectory = 1501,
        DeleteDirectory = 1502,

        CreateClientUpdateFile = 1601,
        UpdateClientUpdateInfo = 1602,
        
        CreateServerUpdateFile = 1701,
        UpdateServerUpdateInfo = 1702,
        
        SyncFileBegin = 1801,
        SyncFileEnd = 1802,
        SyncCreateDirectory = 1803,
        SyncDeleteDirectory = 1804,

    }
}
