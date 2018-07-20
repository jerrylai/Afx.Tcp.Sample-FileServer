using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace AfxTcpFileServerSample.Dto.ProtoBuf
{
    /// <summary>
    /// 文件内容
    /// </summary>
    [ProtoContract]
    public class FileDataDto
    {
        /// <summary>
        /// 内容数据开始位置
        /// </summary>
        [ProtoMember(1, IsRequired=true)]
        public long Index { get; set; }

        /// <summary>
        /// 内容数据长度
        /// </summary>
        [ProtoMember(2, IsRequired = true)]
        public int Length { get; set; }

        /// <summary>
        /// 内容数据
        /// </summary>
        [ProtoMember(3, IsRequired = true)]
        public byte[] Data { get; set; }
    }
}
