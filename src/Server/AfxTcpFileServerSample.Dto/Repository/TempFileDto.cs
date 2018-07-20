using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AfxTcpFileServerSample.Dto.Repository
{
    public class TempFileDto
    {
        public int Id { get; set; }

        public string Directory { get; set; }

        public string Name { get; set; }

        public long Length { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime LastWriteTime { get; set; }

        public long TempIndex { get; set; }

        public string TempName { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
