using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ProtoBuf;

namespace AfxTcpFileServerSample.Dto.ProtoBuf
{
    [ProtoContract]
    public class PageListDto<T>
    {
        [ProtoMember(1, IsRequired = true)]
        public int Index { get; set; }

        [ProtoMember(2, IsRequired = true)]
        public int Size { get; set; }

        [ProtoMember(3, IsRequired = true)]
        public int TotalCount { get; set; }

        [ProtoMember(4, IsRequired = true)]
        public List<T> List { get; set; }
    }
}
