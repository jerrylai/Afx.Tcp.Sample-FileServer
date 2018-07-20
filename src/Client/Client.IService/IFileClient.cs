using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AfxTcpFileServerSample.Dto.ProtoBuf;
using AfxTcpFileServerSample.Enums;

namespace Client.IService
{
    public interface IFileClient : IBaseService
    {
        bool IsLogin { get; }
        LoginInfoDto UserInfo { get; }

        bool IsConnected { get; }

        string Host { get; }

        bool Connect(string hostAndPort, int millisecondsTimeout = 5000);

        Action<IFileClient, Exception> ClosedCallback { get; set; }

        bool Login(string account, string pwd);

        TResult Request<TResult, TData>(MsgCmd cmd, TData data, bool isThrow = false);

        TResult Request<TResult, TData>(MsgCmd cmd, TData data, out int status, out string error);

        bool Request(MsgCmd cmd);

        bool Request<TData>(MsgCmd cmd, TData data);

        void Close();
    }
}
