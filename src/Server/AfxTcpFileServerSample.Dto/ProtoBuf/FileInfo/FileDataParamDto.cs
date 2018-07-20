using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace AfxTcpFileServerSample.Dto.ProtoBuf
{
    /// <summary>
    /// 获取文件数据Dto
    /// </summary>
    [ProtoContract]
    public class FileDataParamDto
    {
        /// <summary>
        /// 文件数据开始位置
        /// </summary>
        [ProtoMember(1, IsRequired = true)]
        public long Index { get; set; }

        /// <summary>
        /// 获取文件数据大小
        /// </summary>
        [ProtoMember(2, IsRequired = true)]
        public int Size { get; set; }
    }
}
