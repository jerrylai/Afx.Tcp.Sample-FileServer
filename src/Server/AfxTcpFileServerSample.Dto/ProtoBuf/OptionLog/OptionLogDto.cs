using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace AfxTcpFileServerSample.Dto.ProtoBuf
{
    [ProtoContract]
    public class OptionLogDto
    {
        [ProtoMember(1, IsRequired = true)]
        public int Id { get; set; }

        [ProtoMember(2)]
        public int Type { get; set; }

        [ProtoMember(3)]
        public int UserId { get; set; }

        [ProtoMember(4)]
        public string UserAccount { get; set; }

        [ProtoMember(5)]
        public string UserName { get; set; }

        [ProtoMember(6)]
        public DateTime CreateTime { get; set; }

        [ProtoMember(7)]
        public string Address { get; set; }

        [ProtoMember(8)]
        public string Msg { get; set; }
    }
}
