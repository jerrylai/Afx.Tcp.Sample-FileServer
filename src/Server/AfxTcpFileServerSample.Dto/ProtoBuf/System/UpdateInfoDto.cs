using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AfxTcpFileServerSample.Enums;
using ProtoBuf;

namespace AfxTcpFileServerSample.Dto.ProtoBuf
{
    [ProtoContract]
    public class UpdateInfoDto
    {
        [ProtoMember(1, IsRequired = true)]
        public int Id { get; set; }

        [ProtoMember(2)]
        public UpdateInfoType Type { get; set; }

        [ProtoMember(3)]
        public string Version { get; set; }

        [ProtoMember(4)]
        public string FilePath { get; set; }

        [ProtoMember(5)]
        public DateTime UpdateTime { get; set; }
    }
}
