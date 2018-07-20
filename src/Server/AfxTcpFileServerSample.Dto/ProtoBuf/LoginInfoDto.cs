using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AfxTcpFileServerSample.Enums;
using ProtoBuf;

namespace AfxTcpFileServerSample.Dto.ProtoBuf
{
    [ProtoContract]
    public class LoginInfoDto
    {
        [ProtoMember(1, IsRequired = true)]
        public int Id { get; set; }

        [ProtoMember(2)]
        public string Name { get; set; }

        [ProtoMember(3)]
        public string Account { get; set; }

        [ProtoMember(4)]
        public int RoleId { get; set; }

        [ProtoMember(5)]
        public string RoleName { get; set; }

        [ProtoMember(6)]
        public List<AuthType> RoleAuth { get; set; }

        [ProtoMember(7)]
        public string SessionId { get; set; }
    }
}
