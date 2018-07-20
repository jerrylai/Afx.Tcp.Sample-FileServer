using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace AfxTcpFileServerSample.Dto.ProtoBuf
{
    [ProtoContract]
    public class UserInfoDto
    {
        [ProtoMember(1, IsRequired = true)]
        public int Id { get; set; }

        [ProtoMember(2)]
        public string Name { get; set; }

        [ProtoMember(3)]
        public string Account { get; set; }

        [ProtoMember(4)]
        public string Password { get; set; }

        [ProtoMember(5)]
        public int RoleId { get; set; }

        [ProtoMember(6)]
        public DateTime UpdateTime { get; set; }

        [ProtoMember(7)]
        public bool IsSystem { get; set; }

        [ProtoMember(8)]
        public bool IsDelete { get; set; }

        [ProtoMember(9)]
        public string RoleName { get; set; }
    }
}
