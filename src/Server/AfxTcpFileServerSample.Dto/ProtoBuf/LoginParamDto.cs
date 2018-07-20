using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace AfxTcpFileServerSample.Dto.ProtoBuf
{
    [ProtoContract]
    public class LoginParamDto
    {
        [ProtoMember(1, IsRequired = true)]
        public string Account { get; set; }

        [ProtoMember(2, IsRequired = true)]
        public string Password { get; set; }
    }
}
