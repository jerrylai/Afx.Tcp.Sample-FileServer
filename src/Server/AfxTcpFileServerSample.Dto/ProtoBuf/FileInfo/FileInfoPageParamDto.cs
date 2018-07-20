using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AfxTcpFileServerSample.Enums;
using ProtoBuf;

namespace AfxTcpFileServerSample.Dto.ProtoBuf
{
    [ProtoContract]
    public class FileInfoPageParamDto
    {
        [ProtoMember(1, IsRequired = true)]
        public int Index { get; set; }

        [ProtoMember(2)]
        public int Size { get; set; }

        [ProtoMember(3)]
        public string Orderby { get; set; }

        [ProtoMember(4)]
        public bool IsDesc { get; set; }

        [ProtoMember(5)]
        public int ParentId { get; set; }

        [ProtoMember(6)]
        public FileInfoType Type { get; set; }

        [ProtoMember(7)]
        public DateTime? BeginCreateTime { get; set; }

        [ProtoMember(8)]
        public DateTime? EndCreateTime { get; set; }

        [ProtoMember(9)]
        public DateTime? BeginLastWriteTime { get; set; }

        [ProtoMember(10)]
        public DateTime? EndLastWriteTime { get; set; }

        [ProtoMember(11)]
        public string Keyword { get; set; }
    }
}
