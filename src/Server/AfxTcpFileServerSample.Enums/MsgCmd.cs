using System;
using System.Collections.Generic;
using System.Text;

namespace AfxTcpFileServerSample.Enums
{
    /// <summary>
    /// 消息Cmd
    /// </summary>
    public enum MsgCmd
    {
        None = 0,

        Login = 1001,
        Logout = 1002,
        UpdatePassword = 1003,

        // Role
        GetRole = 2001,
        GetRoleList = 2002,
        AddRole = 2003,
        UpdateRole = 2004,
        DeleteRole = 2005,
        GetRoleAuthList = 2006,
        ExistRole = 2007,
        GetRoleUserCount = 2008,
        GetSyncRoleList = 2009,

        // User
        GetUser = 3001,
        GetUserPageList = 3002,
        AddUser = 3003,
        UpdateUser = 3004,
        DeleteUser = 3005,
        ExistUser = 3006,
        GetSyncUserList = 3007,

        //Setting
        GetServerInfo = 4001,
        GetServerInfoList = 4002,
        AddServerInfo = 4003,
        UpdateServerInfo = 4004,
        DeleteServerInfo = 4005,
        GetServerSyncList = 4006,
        RestCheckFileInfo = 4007,

        //Option Log
        GetOptionLogPageList = 5001,
        DeleteDayOptionLog = 5002,

        // File Info
        GetFileInfo = 11001,
        GetFileInfoPageList = 11002,
        IsExistFileInfo = 11003,
        GetSyncFileInfoList = 11004,
        DeleteFileInfo = 11005,

        CreateFile = 11006,
        UploadFileData = 11007,
        CreateFileCompleted = 11008,
        OpenFile = 11009,
        GetFileData = 11010,
        CloseFile = 11011,

        CreateDirectory = 11012,

        DeleteOpenFile = 11013,
        GetPhysicaPath = 11014,
        AddFileInfo = 11015,

        //ClientUpdate
        GetClientUpdateInfo = 20101,
        CreateClientUpdateFile = 20102,
        UpdateClientUpdateInfo = 20103,
        OpenClientUpdateFile = 120104,

        //ServerUpdate
        GetServerUpdateInfo = 20201,
        CreateServerUpdateFile = 20202,
        UpdateServerUpdateInfo = 20203,
        OpenServerUpdateInfoFile = 20204,
    }

#if NET4_0

    public static class MsgCmdExtension
    {
        public static int GetValue(this MsgCmd cmd)
        {
            return (int)cmd;
        }

        public static string GetName(this MsgCmd cmd)
        {
            return cmd.ToString();
        }

        public static MsgCmd GetCmd(this int value)
        {
            MsgCmd cmd = MsgCmd.None;
            if (Enum.IsDefined(typeof(MsgCmd), value))
            {
                cmd = (MsgCmd)value;
            }

            return cmd;
        }
    }

#endif
}
