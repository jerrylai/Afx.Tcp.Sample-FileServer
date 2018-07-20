using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AfxTcpFileServerSample.Common
{
    public class UserInfo : IDisposable
    {
        public int Id { get; set; }

        public int RoleId { get; set; }

        public string Account { get; set; }

        public string Name { get; set; }

        public string SessionId { get; set; }

        public string ClientAddress { get; set; }

        public UserFile File { get; private set; }
                
        public UserInfo()
        {
            this.SessionId = Guid.NewGuid().ToString("n");
            this.Id = 0;
            this.RoleId = 0;
            this.Account = "";
            this.Name = "";
            this.ClientAddress = "";
            this.File = new UserFile();
        }

        public void Dispose()
        {
            this.File.Dispose();
            this.SessionId = Guid.NewGuid().ToString("n");
            this.Id = 0;
            this.RoleId = 0;
            this.Account = "";
            this.Name = "";
        }
    }
}
