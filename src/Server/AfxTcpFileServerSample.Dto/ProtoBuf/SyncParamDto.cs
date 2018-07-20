using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ProtoBuf;

namespace AfxTcpFileServerSample.Dto.ProtoBuf
{
    [ProtoContract]
    public class SyncParamDto
    {
        [ProtoMember(1, IsRequired = true)]
        public int Count { get; set; }

        [ProtoMember(2)]
        public int? SyncId { get; set; }

        [ProtoMember(3)]
        public string SyncKey { get; set; }

        [ProtoMember(4)]
        public DateTime? SyncUpdateTime { get; set; }
    }
}
