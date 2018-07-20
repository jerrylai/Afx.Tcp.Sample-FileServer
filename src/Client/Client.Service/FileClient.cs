using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AfxTcpFileServerSample.Dto.ProtoBuf;
using Client.IService;
using Afx.Tcp.Protocols;
using AfxTcpFileServerSample.Enums;
using Afx.Utils;
using Client.Common;

namespace Client.Service
{
    public class FileClient : IFileClient
    {
        private MsgClient msgClient;

        public FileClient()
        {
            this.msgClient = new MsgClient();
            this.msgClient.Encrypt = EncryptUtils.Encrypt;
            this.msgClient.Decrypt = EncryptUtils.Decrypt;
        }

        public virtual bool IsLogin { get; private set; }

        public virtual LoginInfoDto UserInfo { get; private set; }

        private Action<IFileClient, Exception> closedCallback;
        public Action<IFileClient, Exception> ClosedCallback
        {
            get
            {
                return this.closedCallback;
            }
            set
            {
                this.closedCallback = value;
                this.msgClient.ClosedCall = (c, ex) => { this.closedCallback?.Invoke(this, ex); };
            }
        }

        public bool IsConnected => this.msgClient.IsConnected;

        public string Host => this.msgClient.Host;

        public bool IsDisposed => this.msgClient.IsDisposed;


        public bool Login(string account, string pwd)
        {
            bool result = false;
            if(this.IsConnected && !string.IsNullOrEmpty(account))
            {
                LoginParamDto vm = new LoginParamDto() { Account = account, Password = pwd };
                var m = this.Request<LoginInfoDto, LoginParamDto>(MsgCmd.Login, vm);
                if (m != null)
                {
                    this.UserInfo = m;
                    this.IsLogin = true;
                    result = true;
                }
            }

            return result;
        }

        public virtual TResult Request<TResult, TData>(MsgCmd cmd, TData data, bool isThrow = false)
        {
            TResult result = default(TResult);
            var msg = this.msgClient.Send(cmd.GetValue(), data);
            if(msg.Status == MsgStatus.Succeed.GetValue())
            {
                result = msg.GetData<TResult>();
            }
            else
            {
                string s = !string.IsNullOrEmpty(msg.Msg) ? msg.Msg : ((MsgStatus)msg.Status).GetDescription();
                LogUtils.Error($"【Request】cmd:{cmd}, Status: {msg.Status}, Msg: {s}");
                if (isThrow) throw new MsgStatusException(msg.Status, s);
            }
            return result;
        }

        public virtual TResult Request<TResult, TData>(MsgCmd cmd, TData data, out int status, out string error)
        {
            TResult result = default(TResult);
            status = MsgStatus.None.GetValue();
            error = null;
            var msg = this.msgClient.Send(cmd.GetValue(), data);
            if (msg.Status == MsgStatus.Succeed.GetValue())
            {
                result = msg.GetData<TResult>();
            }
            else
            {
                string s = !string.IsNullOrEmpty(msg.Msg) ? msg.Msg : ((MsgStatus)msg.Status).GetDescription();
                LogUtils.Error($"【Request】cmd:{cmd}, Status: {msg.Status}, Msg: {s}");
            }
            status = msg.Status;
            error = !string.IsNullOrEmpty(msg.Msg) ? msg.Msg : ((MsgStatus)msg.Status).GetDescription();

            return result;
        }

        public virtual bool Request(MsgCmd cmd)
        {
            bool result = false;
            var msg = this.msgClient.Send(cmd.GetValue());
            if (msg.Status == MsgStatus.Succeed.GetValue())
            {
                result = true;
            }
            else
            {
                string s = !string.IsNullOrEmpty(msg.Msg) ? msg.Msg : ((MsgStatus)msg.Status).GetDescription();
                LogUtils.Error($"【Request】cmd:{cmd}, Status: {msg.Status}, Msg: {s}");
            }
            return result;
        }

        public virtual bool Request<TData>(MsgCmd cmd, TData data)
        {
            bool result = false;
            var msg = this.msgClient.Send(cmd.GetValue(), data);
            if (msg.Status == MsgStatus.Succeed.GetValue())
            {
                result = true;
            }
            else
            {
                string s = !string.IsNullOrEmpty(msg.Msg) ? msg.Msg : ((MsgStatus)msg.Status).GetDescription();
                LogUtils.Error($"【Request】cmd:{cmd}, Status: {msg.Status}, Msg: {s}");
            }
            return result;
        }

        public virtual void Close()
        {
            this.msgClient.Close();
            this.IsLogin = false;
            this.UserInfo = null;
        }

        public virtual bool Connect(string hostAndPort, int millisecondsTimeout = 5000)
        {
            return this.msgClient.Connect(hostAndPort, millisecondsTimeout);
        }

        public virtual void Dispose()
        {
            this.Close();
            this.msgClient.Dispose();
        }
    }
}
