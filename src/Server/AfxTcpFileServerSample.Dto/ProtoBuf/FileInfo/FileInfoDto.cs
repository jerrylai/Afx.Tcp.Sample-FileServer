using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AfxTcpFileServerSample.Enums;
using ProtoBuf;

namespace AfxTcpFileServerSample.Dto.ProtoBuf
{
    [ProtoContract]
    public class FileInfoDto
    {
        [ProtoMember(1, IsRequired = true)]
        public int Id { get; set; }

        [ProtoMember(2)]
        public int ParentId { get; set; }

        [ProtoMember(3)]
        public FileInfoType Type { get; set; }

        [ProtoMember(4)]
        public string Directory { get; set; }

        [ProtoMember(5)]
        public string Name { get; set; }

        [ProtoMember(6)]
        public long Length { get; set; }

        [ProtoMember(7)]
        public DateTime CreationTime { get; set; }

        [ProtoMember(8)]
        public DateTime LastWriteTime { get; set; }

        [ProtoMember(9)]
        public DateTime UpdateTime { get; set; }

        [ProtoMember(10)]
        public string Key { get; set; }

        [ProtoMember(11)]
        public bool IsDelete { get; set; }
    }
}
