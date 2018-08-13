using System;
using System.Collections.Generic;
using System.Text;

using Afx.Tcp.Protocols;
using Afx.Tcp.Host;
using AfxTcpFileServerSample.Common;
using AfxTcpFileServerSample.Dto;
using AfxTcpFileServerSample.IService;
using Afx.Ioc;

namespace AfxTcpFileServerSample.Controllers
{
    public class FileServer : IDisposable
    {
        public const string USER_INFO_KEY = "SYS_USER_INFO";

        private TcpHost host;

        public FileServer()
        {
            this.host = new TcpHost();
            this.host.LoadCmdMethod("Config/Cmd.xml");
            this.host.Encrypt = EncryptUtils.Encrypt;
            this.host.Decrypt = EncryptUtils.Decrypt;
            this.host.ClientConnectedEvent += OnClientConnectedEvent;
            this.host.ClientClosedEvent += OnClientClosedEvent;
            this.host.MvcHostServerErrorEvent += OnMvcHostServerErrorEvent;
        }

        private void OnClientConnectedEvent(TcpHost server, Session session)
        {
            UserInfo user = new UserInfo()
            {
                SessionId = session.Sid,
                ClientAddress = session.Address
            };
            session[USER_INFO_KEY] = user;
        }

        private void OnMvcHostServerErrorEvent(TcpHost server, Exception ex)
        {
            LogUtils.Error("【MvcHostServerError】", ex);
        }

        private void OnClientClosedEvent(TcpHost server, Session session)
        {
            try
            {
                UserInfo user = session[USER_INFO_KEY] as UserInfo;
                SessionUtils.UserInfo = user;
                var userService = IocUtils.Get<IUserService>();
                userService.Logout();
            }
            catch (Exception ex)
            {
                LogUtils.Error("【OnClientClosedEvent】", ex);
            }
        }


        public void Start()
        {
            try
            {
                this.host.Start(ConfigUtils.Port);
                var fileInfoService = IocUtils.Get<IFileInfoService>();
                fileInfoService.CheckFileInfo();
            }
            catch(Exception ex)
            {
                LogUtils.Error("【Start】", ex);
                throw ex;
            }
        }

        public void Stop()
        {
            this.host.Stop();
        }


        public void Dispose()
        {
            this.host.Dispose();
        }
        
    }
}
