using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace AfxTcpFileServerSample.Dto.ProtoBuf
{
    [ProtoContract]
    public class UserInfoPageParamDto
    {
        [ProtoMember(1, IsRequired = true)]
        public int Index { get; set; }

        [ProtoMember(2, IsRequired = true)]
        public int Size { get; set; }

        [ProtoMember(3)]
        public string Keyword { get; set; }

        [ProtoMember(4)]
        public string Orderby { get; set; }

        [ProtoMember(5)]
        public bool IsDesc { get; set; }
    }
}
