using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace AfxTcpFileServerSample.Dto.ProtoBuf
{
    [ProtoContract]
    public class UpdatePwdDto
    {
        [ProtoMember(1, IsRequired = true)]
        public string OldPassword { get; set; }

        [ProtoMember(2, IsRequired = true)]
        public string NewPassword { get; set; }
    }
}
