using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AfxTcpFileServerSample.Enums;
using ProtoBuf;

namespace AfxTcpFileServerSample.Dto.ProtoBuf
{
    [ProtoContract]
    public class AddFileInfoDto
    {
        [ProtoMember(1, IsRequired=true)]
        public FileInfoType Type { get; set; }

        [ProtoMember(2)]
        public string Directory { get; set; }

        [ProtoMember(3)]
        public string Name { get; set; }
    }
}
