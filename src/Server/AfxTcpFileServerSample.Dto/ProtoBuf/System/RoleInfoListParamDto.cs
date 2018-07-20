using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AfxTcpFileServerSample.Enums;
using ProtoBuf;

namespace AfxTcpFileServerSample.Dto.ProtoBuf
{
    [ProtoContract]
    public class RoleInfoListParamDto
    {
        [ProtoMember(1, IsRequired = true)]
        public RoleType Type { get; set; }

        [ProtoMember(2)]
        public string Keyword { get; set; }

        [ProtoMember(3)]
        public string Orderby { get; set; }

        [ProtoMember(4)]
        public bool IsDesc { get; set; }
    }
}
