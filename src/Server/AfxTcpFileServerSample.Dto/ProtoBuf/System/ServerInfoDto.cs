using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AfxTcpFileServerSample.Enums;
using ProtoBuf;

namespace AfxTcpFileServerSample.Dto.ProtoBuf
{
    [ProtoContract]
    public class ServerInfoDto
    {
        [ProtoMember(1, IsRequired = true)]
        public int Id { get; set; }

        [ProtoMember(2)]
        public int Port { get; set; }

        [ProtoMember(3)]
        public string Address { get; set; }

        [ProtoMember(4)]
        public string Account { get; set; }

        [ProtoMember(5)]
        public string Password { get; set; }

        [ProtoMember(6)]
        public DateTime UpdateTime { get; set; }

        [ProtoMember(7, IsRequired=true)]
        public List<SyncType> SyncList { get; set; }
    }
}
