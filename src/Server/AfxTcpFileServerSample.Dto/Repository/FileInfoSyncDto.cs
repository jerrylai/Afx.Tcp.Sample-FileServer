using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AfxTcpFileServerSample.Dto.Repository
{
    public class FileInfoSyncDto
    {
        public int ServerId { get; set; }

        public int? SyncId { get; set; }

        public string SyncKey { get; set; }

        public DateTime? SyncUpdateTime { get; set; }
    }
}
