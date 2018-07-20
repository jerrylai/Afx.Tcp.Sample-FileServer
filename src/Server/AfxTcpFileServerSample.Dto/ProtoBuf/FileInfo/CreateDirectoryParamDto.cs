using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace AfxTcpFileServerSample.Dto.ProtoBuf
{
    [ProtoContract]
    public class CreateDirectoryParamDto
    {
        [ProtoMember(1, IsRequired = true)]
        public string Directory { get; set; }

        [ProtoMember(2)]
        public string Name { get; set; }
        
        [ProtoMember(3)]
        public DateTime CreationTime { get; set; }

        [ProtoMember(4)]
        public DateTime LastWriteTime { get; set; }
    }
}
