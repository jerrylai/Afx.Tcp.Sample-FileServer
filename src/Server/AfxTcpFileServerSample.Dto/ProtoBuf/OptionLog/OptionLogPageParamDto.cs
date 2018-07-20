using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ProtoBuf;

namespace AfxTcpFileServerSample.Dto.ProtoBuf
{
    [ProtoContract]
    public class OptionLogPageParamDto
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
        public int Type { get; set; }
        
        [ProtoMember(6)]
        public DateTime? BeginTime { get; set; }

        [ProtoMember(7)]
        public DateTime? EndTime { get; set; }

        [ProtoMember(8)]
        public string Keyword { get; set; }
    }
}
