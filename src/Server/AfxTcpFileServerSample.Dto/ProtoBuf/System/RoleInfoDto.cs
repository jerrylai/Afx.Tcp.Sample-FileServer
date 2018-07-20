using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AfxTcpFileServerSample.Enums;
using ProtoBuf;

namespace AfxTcpFileServerSample.Dto.ProtoBuf
{
    [ProtoContract]
    public class RoleInfoDto
    {
        [ProtoMember(1, IsRequired = true)]
        public int Id { get; set; }

        [ProtoMember(2)]
        public RoleType Type { get; set; }

        [ProtoMember(3)]
        public string Name { get; set; }

        [ProtoMember(4)]
        public bool IsSystem { get; set; }

        [ProtoMember(5)]
        public DateTime UpdateTime { get; set; }
        
        [ProtoMember(6)]
        public string Key { get; set; }

        [ProtoMember(7)]
        public bool IsDelete { get; set; }
        
        [ProtoMember(8, IsRequired = true)]
        public List<AuthType> AuthList { get; set; }
    }
}
